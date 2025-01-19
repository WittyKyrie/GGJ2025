using System.Collections.Generic;
using Buff;
using UnityEngine;

namespace Util.UI
{
    public class PlayerBuffList : MonoBehaviour
    {
        public List<SingleBuffUI> buffs = new();

        public void Init(List<BuffData> datas)
        {
            for (int i = 0; i < 3; i++)
            {
                buffs[i].Init(datas[i]);
            }
        }
    }
}