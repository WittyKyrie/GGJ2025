using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public int maxHealth = 100; //最大酒量
        public int currentHealth = 100; //当前酒量
        public List<Buff> Buffs = new(); //道具列表
        
        
    }
}