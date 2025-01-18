using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public bool isMainPlayer;
        public int maxHealth = 100; //最大酒量
        public int currentHealth; //当前酒量
        public List<Buff.Buff> Buffs = new(); //道具列表

        private void Awake()
        {
            if (isMainPlayer)
            {
                GameManager.Instance.mainPlayer = this;
            }
            else
            {
                GameManager.Instance.subPlayer = this;
            }
        }

        public void Drinking()
        {
            
        }
    }
}