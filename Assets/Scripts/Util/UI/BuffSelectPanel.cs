using System.Collections.Generic;
using Buff;
using DG.Tweening;
using Febucci.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Util.EventHandleSystem;

namespace Util.UI
{
    public class BuffSelectPanel : MonoBehaviour
    {
        public Sprite btnRed;
        public Sprite btnBlue;
        
        public GameObject redBck;
        public GameObject blueBck;

        public GameObject selectObj;
        public Button btn;

        public TypewriterByCharacter red;
        public TypewriterByCharacter blue;

        public SimpleUIAnimation simpleUIAnimation;
        public List<SingleBuffSelect> singleBuffSelects = new();

        private void OnEnable()
        {
            QuickEvent.SubscribeListener<ShowSelectBuff>(ShowRedBck);
            GameManager.Instance.ChangeState(GameState.PreAnimation);
        }

        private void OnDisable()
        {
            QuickEvent.UnsubscribeListener<ShowSelectBuff>(ShowRedBck);
        }

        [Button]
        public void ShowMainPlayerSelect()
        {
            var buffDataList = BuffDataInfo.GetRandomBuffData(3);
            GameManager.Instance.mainPlayer.InstantiateBuffInstance(buffDataList);
            var operateList = new List<string>()
            {
                "Q",
                "W",
                "E"
            };

            for (int i = 0; i < 3; i++)
            {
                singleBuffSelects[i].Init(buffDataList[i], operateList[i]);
            }
            
            selectObj.gameObject.SetActive(true);
            simpleUIAnimation.DoBornAnimation().OnComplete(() =>
            {
                btn.gameObject.SetActive(true);
                btn.GetComponent<Image>().sprite = btnRed;
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(ShowBlueBck);
            });
        }

        [Button]
        public void ShowSubPlayerSelect()
        {
            var buffDataList = BuffDataInfo.GetRandomBuffData(3);
            GameManager.Instance.subPlayer.InstantiateBuffInstance(buffDataList);
            var operateList = new List<string>()
            {
                "1",
                "2",
                "3"
            };

            for (int i = 0; i < 3; i++)
            {
                singleBuffSelects[i].Init(buffDataList[i], operateList[i]);
            }
            
            selectObj.gameObject.SetActive(true);
            simpleUIAnimation.DoBornAnimation().OnComplete(() =>
            {
                btn.gameObject.SetActive(true);
                btn.GetComponent<Image>().sprite = btnBlue;
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() =>
                {
                    AkSoundEngine.PostEvent(SoundEffects.NormalClick, GameManager.Instance.gameObject);
                    simpleUIAnimation.DoDieAnimation().OnComplete(() =>
                    {
                        QuickEvent.DispatchMessage(new ShowPlayerTurnText(true));
                        gameObject.SetActive(false);
                    });
                });
            });
        }

        [Button]
        public void ShowRedBck(ShowSelectBuff e)
        {
            gameObject.SetActive(true);
            btn.gameObject.SetActive(false);
            selectObj.gameObject.SetActive(false);
            redBck.SetActive(false);
            blueBck.SetActive(true);

            red.gameObject.SetActive(true);
            red.StartShowingText();

            DOVirtual.DelayedCall(1, () =>
            {
                red.onTextDisappeared.AddListener(() =>
                {
                    red.gameObject.SetActive(false);
                    ShowMainPlayerSelect();
                });
                red.StartDisappearingText();
            });
        }

        [Button]
        public void ShowBlueBck()
        {
            selectObj.gameObject.SetActive(false);
            btn.gameObject.SetActive(false);
            redBck.SetActive(true);
            blueBck.SetActive(false);
            AkSoundEngine.PostEvent(SoundEffects.NormalClick, GameManager.Instance.gameObject);

            blue.gameObject.SetActive(true);
            blue.StartShowingText();

            DOVirtual.DelayedCall(1, () =>
            {
                blue.onTextDisappeared.AddListener(() =>
                {
                    blue.gameObject.SetActive(false);
                    ShowSubPlayerSelect();
                });
                blue.StartDisappearingText();
            });
        }
    }
}