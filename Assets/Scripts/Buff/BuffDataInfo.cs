using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Buff
{
    [Serializable]
    public class BuffData
    {
        public string key;
        public string name;
        public string describe;
    }

    [CreateAssetMenu(fileName = "BuffDataInfo", menuName = "Data/BuffDataInfo", order = 1)]
    public class BuffDataInfo : ScriptableObject
    {
        public List<BuffData> buffDataList;

        public string GetName(string key)
        {
            foreach (var info in buffDataList.Where(info => info.key == key))
            {
                return info.name;
            }

            return "没找到";
        }

        public string GetDescribe(string key)
        {
            foreach (var info in buffDataList.Where(info => info.key == key))
            {
                return info.describe;
            }

            return "没找到";
        }
    }
}