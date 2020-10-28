using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TowerDefence.Towers;
using TowerDefence.Managers;

namespace TowerDefence.Enemies
{
    [AddComponentMenu("Mechanics/Enemy")]
    public class Enemy : MonoBehaviour
    {
        [System.Serializable]
        public class DeathEvent : UnityEvent<Enemy>
        {

        }

        #region Variables
        [Header("Reference Variables")]

        [Header("General Stats")]
        [SerializeField] private float health = 1;
        [SerializeField] private float speed = 1;
        [SerializeField] private float size = 1;

        [Header("Attack Stats")]
        [SerializeField] private float damage = 1;
        //[SerializeField] private string[] resistances;

        [Header("On Death")]
        [SerializeField] private float xpGain = 0.2f;
        [SerializeField] private int moneyGain = 1;

        [Space]

        [SerializeField]
        private DeathEvent onDeath = new DeathEvent();

        private Player player;

        #endregion
        #region Properties
        public float XP { get => xpGain; }
        public int Money { get => moneyGain; }
        #endregion
        void Start()
        {
            player = Player.instance; //find player instance
            onDeath.AddListener(player.AddMoney);
        }

        void Update()
        {

        }
        
        #region Functions
        /// <summary>Handles damage of enemy and checks if it should die.</summary>
        /// <param name="_tower">The tower doing the damage to the enemy.</param>
        public void Damage(float _damage)
        {
            health -= _damage;
            if (health <= 0)
            {
                Death();
            }
        }
        /// <summary>Handles destroying the enemy.</summary>
        private void Death()
        {
            onDeath.Invoke(this);
        }
        #endregion
    }
}