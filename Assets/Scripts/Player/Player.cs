using System;
using System.Collections.Generic;
using System.Linq;
using Buff;
using DG.Tweening;
using MoreMountains.Tools;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Util;
using Util.EventHandleSystem;
using Util.UI;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public bool isMainPlayer;
        public float maxHealth = 10000; //最大酒量
        public float currentHealth = 10000; //当前酒量
        public BuffInstance[] BuffInstance = new BuffInstance[3];
        public MMProgressBar bar;
        public PlayerBuffList playerBuffList;
        public BeerGlass beerGlass;
        public BeerCan beerCan;
        public GameObject SnackLabel;

        public Func<float> GetSnacksMultiplier;
        public Func<float> GetPropPowerMultiplier;
        
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
            var beerDamage = beerGlass.GetBeerVolumeResult();
            beerDamage *= beerGlass.GetSnakeMultiplier();
            if (GetSnacksMultiplier != null) beerDamage *= GetSnacksMultiplier.Invoke();

            var preHealAmount = 0f;
            if(GetPropPowerMultiplier != null) preHealAmount = beerDamage * GetPropPowerMultiplier.Invoke();
            
            beerGlass.Reset();
            beerGlass.gameObject.SetActive(false);
            beerCan.gameObject.SetActive(false);
            bar.gameObject.SetActive(true);

            DOVirtual.DelayedCall(1f, () =>
            {
                if (preHealAmount > 0)
                {
                    Debug.Log($"timtest Trigger heal");
                    ChangeHealth(preHealAmount);
                }
            });
            AkSoundEngine.PostEvent(SoundEffects.DrinkingRound, GameManager.Instance.gameObject);
            
            DOVirtual.DelayedCall(2f, () =>
            {
                HandleDrinking(-beerDamage);
            });
        }

        public void InstantiateBuffInstance(List<BuffData> buffDatas)
        {
            //create buff instance
            for (int i = 0; i < buffDatas.Count; i++)
            {
                var instance = BuffDataInfo.GetBuffInstance(buffDatas[i].key);
                instance.buffData = buffDatas[i];
                instance.Init(this);
                BuffInstance[i] = instance;
            }
            playerBuffList.UpdateBuffUI(BuffInstance);
        }

        public void UseItem(int itemIndex)
        {
            if (BuffInstance[itemIndex] != null && !BuffInstance[itemIndex].used)
            {
                BuffInstance[itemIndex].OnUseItem();
                BuffInstance[itemIndex].used = true;
                playerBuffList.UpdateBuffUI(BuffInstance);
            }
        }

        public void EndItemOnPour()
        {
            foreach (var instance in BuffInstance)
            {
                if(instance.used) instance.OnPourEnd();
            }
        }
        public void EndItemOnRound()
        {
            foreach (var instance in BuffInstance)
            {
                if(instance.used) instance.OnRoundEnd();
            }
        }

        public void RemoveItem(BuffInstance item)
        {
            for (int i = 0; i < 3; i++)
            {
                if (BuffInstance[i] == item)
                {
                    BuffInstance[i] = null;
                    break;
                }
            }
            playerBuffList.UpdateBuffUI(BuffInstance);
        }

        private void MainPlayerTurn(MainPlayerTurn e)
        {
            playerBuffList.gameObject.SetActive(true);
            bar.gameObject.SetActive(false);

            if (isMainPlayer)
            {
                beerGlass.gameObject.SetActive(false);
                beerCan.gameObject.SetActive(true);
                DOVirtual.DelayedCall(1, () =>
                {
                    beerCan.BindP1();
                });
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
                DOVirtual.DelayedCall(1, () =>
                {
                    beerCan.BindP2();
                });
            }
            else
            {
                beerGlass.gameObject.SetActive(true);
                beerCan.gameObject.SetActive(false);
                beerCan.UnBindP2();
            }
        }

        public void ChangeHealth(float value)
        {
            bar.Initialization();
            currentHealth += value;
            bar.UpdateBar01(currentHealth / maxHealth);
        }
        [Button]
        public void HandleDrinking(float num)
        {
            ChangeHealth(num);
            if (currentHealth <= 0)
            {
                DOVirtual.DelayedCall(1f, () =>
                {
                    GameManager.Instance.ChangeState(GameState.Settlement);
                });
            }
            else
            {
                DOVirtual.DelayedCall(1f, () =>
                {
                    if(isMainPlayer) QuickEvent.DispatchMessage(new ShowPlayerTurnText(true));
                });
            }
        }

        public void SwitchSnackLabel(bool active)
        {
            SnackLabel.SetActive(active);
            var srs = SnackLabel.GetComponentsInChildren<SpriteRenderer>();
            if (active)
            {
                SnackLabel.transform.localScale = Vector3.zero;
                SnackLabel.transform.DOScale(Vector3.one, 1);
                srs[0].transform.DORotate(Vector3.forward, 20);
            }
        }
    }
}