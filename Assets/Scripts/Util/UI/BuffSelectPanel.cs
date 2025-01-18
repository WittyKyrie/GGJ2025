using DG.Tweening;
using Febucci.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Util.UI
{
    public class BuffSelectPanel : MonoBehaviour
    {
        public GameObject redBck;
        public GameObject blueBck;

        public GameObject selectObj;
        public Button btn;

        public TypewriterByCharacter red;
        public TypewriterByCharacter blue;

        public SimpleUIAnimation simpleUIAnimation;

        [Button]
        public void ShowMainPlayerSelect()
        {
            selectObj.gameObject.SetActive(true);
            simpleUIAnimation.DoBornAnimation().OnComplete(() =>
            {
                btn.gameObject.SetActive(true);
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(ShowBlueBck);
            });
        }

        [Button]
        public void ShowSubPlayerSelect()
        {
            selectObj.gameObject.SetActive(true);
            simpleUIAnimation.DoBornAnimation().OnComplete(() =>
            {
                btn.gameObject.SetActive(true);
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() =>
                {
                    simpleUIAnimation.DoDieAnimation().OnComplete(() =>
                    {
                        GameManager.Instance.ChangeState(GameState.MainPlayerTurn);
                        gameObject.SetActive(false);
                    });
                });
            });
        }

        [Button]
        public void ShowRedBck()
        {
            gameObject.SetActive(true);
            btn.gameObject.SetActive(false);
            selectObj.gameObject.SetActive(false);
            redBck.SetActive(true);
            blueBck.SetActive(false);

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
            redBck.SetActive(false);
            blueBck.SetActive(true);

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