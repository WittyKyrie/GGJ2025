﻿using DG.Tweening;
using Febucci.UI;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;
using Util.EventHandleSystem;

namespace Util.UI
{
    public class MainCanvas : MonoBehaviour
    {
        public TypewriterByCharacter red;
        public TypewriterByCharacter blue;

        public SkeletonAnimation spine;

        private void OnEnable()
        {
            QuickEvent.SubscribeListener<ShowPlayerTurnText>(ShowPlayerTurnText);
            QuickEvent.SubscribeListener<DrinkingEvent>(HandleDrinking);
            AkSoundEngine.PostEvent(SoundEffects.InGameBgm, GameManager.Instance.gameObject);
            AkSoundEngine.PostEvent(SoundEffects.InGameAmbBar, GameManager.Instance.gameObject);
        }

        private void OnDisable()
        {
            QuickEvent.UnsubscribeListener<ShowPlayerTurnText>(ShowPlayerTurnText);
            QuickEvent.UnsubscribeListener<DrinkingEvent>(HandleDrinking);
        }

        private void HandleDrinking(DrinkingEvent e)
        {
            DOVirtual.DelayedCall(0.5f, () =>
            {
                spine.gameObject.SetActive(true);
                spine.AnimationState.SetAnimation(0, "animation", false);
            });
            
            DOVirtual.DelayedCall(2.5f, () =>
            {
                spine.gameObject.SetActive(false);
            });
        }

        [Button]
        public void ShowPlayerTurnText(ShowPlayerTurnText e)
        {
            if (e.IsMainPlayer)
            {
                red.gameObject.SetActive(true);
                red.StartShowingText();
                DOVirtual.DelayedCall(1, () =>
                {
                    red.onTextDisappeared.RemoveAllListeners();
                    red.onTextDisappeared.AddListener(() =>
                    {
                        GameManager.Instance.ChangeState(GameState.MainPlayerTurn);
                        red.gameObject.SetActive(false);
                    });
                    red.StartDisappearingText();
                });
            }
            else
            {
                blue.gameObject.SetActive(true);
                blue.StartShowingText();
                DOVirtual.DelayedCall(1, () =>
                {
                    blue.onTextDisappeared.RemoveAllListeners();
                    blue.onTextDisappeared.AddListener(() =>
                    {
                        GameManager.Instance.ChangeState(GameState.SubPlayerTurn);
                        blue.gameObject.SetActive(false);
                    });
                    blue.StartDisappearingText();
                });
            }
        }
    }
}