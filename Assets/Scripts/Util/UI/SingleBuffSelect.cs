using Buff;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Util.UI
{
    public class SingleBuffSelect : MonoBehaviour
    {
        public TMP_Text operateText;
        public TMP_Text name;
        public TMP_Text des;
        public Image img;

        public void Init(BuffData data, string operate)
        {
            operateText.text = operate;
            name.text = data.name;
            des.text = data.describe;
            img.sprite = data.sprite;
        }
    }
}