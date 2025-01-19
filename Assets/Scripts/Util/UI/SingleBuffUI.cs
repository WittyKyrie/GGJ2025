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
            img.enabled = instance != null;
            if (instance == null) return;
            img.sprite = instance.buffData.sprite;
            if (instance.used)
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