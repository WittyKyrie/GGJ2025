using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Buff
{
    [Serializable]
    public class BuffData
    {
        public string key;
        public string name;
        public string describe;
        [PreviewField] public Sprite sprite;
    }

    [CreateAssetMenu(fileName = "BuffDataInfo", menuName = "Data/BuffDataInfo", order = 1)]
    public class BuffDataInfo : ScriptableObject
    {
        public List<BuffData> buffDataList;

        public static string GetName(string key)
        {
            var buffList = GameManager.Instance.buffDataInfo.buffDataList;
            foreach (var info in buffList.Where(info => info.key == key))
            {
                return info.name;
            }

            return "没找到";
        }

        public static string GetDescribe(string key)
        {
            var buffList = GameManager.Instance.buffDataInfo.buffDataList;
            foreach (var info in buffList.Where(info => info.key == key))
            {
                return info.describe;
            }

            return "没找到";
        }

        public static Sprite GetSprite(string key)
        {
            var buffList = GameManager.Instance.buffDataInfo.buffDataList;
            foreach (var info in buffList.Where(info => info.key == key))
            {
                return info.sprite;
            }

            return null;
        }

        public static List<BuffData> GetRandomBuffData(int count)
        {
            var buffList = GameManager.Instance.buffDataInfo.buffDataList;

            if (buffList.Count < count)
            {
                throw new ArgumentException($"Requested count ({count}) exceeds the available BuffData count ({buffList.Count}).");
            }

            return buffList.OrderBy(_ => Guid.NewGuid()).Take(count).ToList();
        }

        public static BuffInstance GetBuffInstance(string buffName)
        {
            switch (buffName)
            {
                case "PropHand": return new PropHand();
                case "PropSnacks": return new PropSnacks();
                case "PropPower": return new PropPower();
                case "PropSnakes": return new PropSnakes();
                case "PropChopsticks": return new PropChopsticks();
                case "PropMTS": return new PropMTS();
                case "PropLiftTable": return new PropLiftTable();
                case "PropTransform": return new PropTransform();
                default:
                    return null;
            }
        }
    }
}