using Buff;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Util.UI
{
    public class SingleBuffUI : MonoBehaviour
    {
        public TMP_Text name;
        public Image img;

        public void UpdateDisplay(BuffInstance instance)
        {
            name.text = instance.buffData.name;
            img.sprite = instance.buffData.sprite;
            if (instance.used)
            {
                name.color = Color.gray;
                img.color = Color.gray;
            }
            else
            {
                name.color = Color.white;
                img.color = Color.white;
            }
        }
    }
}