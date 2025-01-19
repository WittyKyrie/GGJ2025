using DG.Tweening;
using UnityEngine;

namespace Util.UI
{
    public class Move : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<RectTransform>().localPosition = new Vector3(0, -1700, 0);
            GetComponent<RectTransform>().DOAnchorPosY(0, 1f);
        }
    }
}