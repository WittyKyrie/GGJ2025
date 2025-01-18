﻿using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.Tools;
using Sirenix.OdinInspector;
using UnityEngine;
using Util.EventHandleSystem;
using Util.UI;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public bool isMainPlayer;
        public float maxHealth = 100; //最大酒量
        public float currentHealth = 100; //当前酒量
        public List<Buff.Buff> Buffs = new(); //道具列表
        public MMProgressBar bar;
        public PlayerBuffList playerBuffList;
        public BeerGlass beerGlass;
        public BeerCan beerCan;

        private void Awake()
        {
            bar.SetBar01(1);
            if (isMainPlayer)
            {
                GameManager.Instance.mainPlayer = this;
            }
            else
            {
                GameManager.Instance.subPlayer = this;
            }
        }

        private void OnEnable()
        {
            QuickEvent.SubscribeListener<MainPlayerTurn>(MainPlayerTurn);
            QuickEvent.SubscribeListener<SubPlayerTurn>(SubPlayerTurn);
        }

        private void OnDisable()
        {
            QuickEvent.UnsubscribeListener<MainPlayerTurn>(MainPlayerTurn);
            QuickEvent.UnsubscribeListener<SubPlayerTurn>(SubPlayerTurn);
        }

        public void Drinking()
        {
            playerBuffList.gameObject.SetActive(false);
            bar.gameObject.SetActive(true);
            
            DOVirtual.DelayedCall(1f, () =>
            {
                HandleDrinking(-50);
            });
        }

        private void MainPlayerTurn(MainPlayerTurn e)
        {
            playerBuffList.gameObject.SetActive(true);
            bar.gameObject.SetActive(false);

            if (isMainPlayer)
            {
                beerGlass.gameObject.SetActive(false);
                beerCan.gameObject.SetActive(true);
                beerCan.BindP1();
            }
            else
            {
                beerGlass.gameObject.SetActive(true);
                beerCan.gameObject.SetActive(false);
                beerCan.UnBindP1();
            }
        }

        private void SubPlayerTurn(SubPlayerTurn e)
        {
            playerBuffList.gameObject.SetActive(true);
            bar.gameObject.SetActive(false);

            if (!isMainPlayer)
            {
                beerGlass.gameObject.SetActive(false);
                beerCan.gameObject.SetActive(true);
                beerCan.BindP2();
            }
            else
            {
                beerGlass.gameObject.SetActive(true);
                beerCan.gameObject.SetActive(false);
                beerCan.UnBindP2();
            }
        }

        [Button]
        public void HandleDrinking(int num)
        {
            bar.Initialization();
            currentHealth += num;
            bar.UpdateBar01(currentHealth / maxHealth);
            if (currentHealth <= 0)
            {
                GameManager.Instance.ChangeState(GameState.Settlement);
            }
            else
            {
                if(isMainPlayer) QuickEvent.DispatchMessage(new ShowPlayerTurnText(true));
            }
        }
    }
}