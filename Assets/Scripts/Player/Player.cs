using System.Collections.Generic;
using Buff;
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
        public float maxHealth = 10000; //最大酒量
        public float currentHealth = 10000; //当前酒量
        public List<BuffData> Buffs = new(); //道具列表
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
            var result = beerGlass.GetBeerVolumeResult();
            beerGlass.Reset();
            beerGlass.gameObject.SetActive(false);
            beerCan.gameObject.SetActive(false);
            bar.gameObject.SetActive(true);
            
            DOVirtual.DelayedCall(2f, () =>
            {
                HandleDrinking(-result);
            });
        }

        public void InitBuffList()
        {
            playerBuffList.Init(Buffs);
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
        public void HandleDrinking(float num)
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
                DOVirtual.DelayedCall(0.5f, () =>
                {
                    if(isMainPlayer) QuickEvent.DispatchMessage(new ShowPlayerTurnText(true));
                });
            }
        }
    }
}