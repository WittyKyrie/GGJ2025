using Buff;
using UnityEngine;
using UnityEngine.UI;

namespace Util.UI
{
    public class SingleBuffUI : MonoBehaviour
    {
        // public TMP_Text name;
        public Image img;

        public void Init(BuffData data)
        {
            // name.text = data.name;
            img.sprite = data.sprite;
        }
    }
}