using Buff;
using UnityEngine;
using UnityEngine.UI;

namespace Util.UI
{
    public class SingleBuffUI : MonoBehaviour
    {
        // public TMP_Text name;
        public Image img;

        public void UpdateDisplay(BuffInstance instance)
        {
            img.sprite = instance != null? instance.buffData.sprite : null;
            if (instance != null && instance.used)
            {
                img.color = Color.gray;
            }
            else
            {
                img.color = Color.white;
            }
        }
    }
}