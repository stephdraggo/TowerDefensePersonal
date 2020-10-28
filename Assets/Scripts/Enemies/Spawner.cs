using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TowerDefence.Managers;

namespace TowerDefence.Enemies
{
    [AddComponentMenu("Mechanics/Enemy/Spawner")]
    public class Spawner : MonoBehaviour
    {
        [System.Serializable]
        public class AttackEvent : UnityEvent<float> { }
        public AttackEvent onAttackEvent;

        public delegate float AttackDelegate();
        public AttackDelegate onAttack;

        public float MeleeAttack()
        {
            print("Melee attack");
            return 100f;
        }

        public float ShootAttack()
        {
            print("Shoot attack");
            return 50f;
        }

        #region Variables
        [SerializeField] private float spawnRate = 1;

        private float currentTime = 0;

        private EnemyManager manager;
        #endregion
        #region Properties
        public float SpawnRate
        {
            get => spawnRate;
        }
        #endregion
        void Start()
        {
            manager = EnemyManager.instance;
            onAttack += MeleeAttack;
            onAttack += MeleeAttack;
            onAttack += ShootAttack;
            onAttack -= MeleeAttack;
            onAttack();
        }

        void Update()
        {
            if (currentTime < SpawnRate) //if current time is less than spawn rate
            {
                currentTime += Time.deltaTime; //increase time
            }
            else //if current time has reached spawn rate
            {
                if (manager != null) //if enemy manager is connected
                {
                    manager.SpawnEnemy(transform); //spawn enemy
                }
                currentTime = 0; //reset time
            }
        }
    }
}