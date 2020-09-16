using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefence.Towers;

namespace TowerDefence.Enemies
{
    [AddComponentMenu("Mechanics/Enemy")]
    public class Enemy : MonoBehaviour
    {
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

        private Player player;

        #endregion
        void Start()
        {
            player = Player.instance; //find player instance
        }

        void Update()
        {

        }
        
        #region Functions
        /// <summary>Handles damage of enemy and checks if it should die.</summary>
        /// <param name="_tower">The tower doing the damage to the enemy.</param>
        public void Damage(Tower _tower)
        {
            health -= _tower.Damage;
            _tower.AddExperience(xpGain);
            if (health <= 0)
            {
                Death(_tower);
            }
        }
        /// <summary>Handles destroying the enemy.</summary>
        /// <param name="_tower">The tower that receives xp from the kill.</param>
        private void Death(Tower _tower)
        {
            _tower.AddExperience(xpGain * 5);
            player.AddMoney(moneyGain);
            Destroy(gameObject);
        }
        #endregion
    }
}