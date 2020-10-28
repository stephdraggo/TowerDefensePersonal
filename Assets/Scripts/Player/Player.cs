using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefence.Towers;
using TowerDefence.Enemies;

namespace TowerDefence
{
    [AddComponentMenu("Mechanics/Player")]
    public class Player : MonoBehaviour
    {
        #region Variables
        public static Player instance = null;
        [Header("Player Variables")]
        private int money = 100;
        #endregion
        #region Awake, singleton set up
        void Awake()
        {
            if (instance == null) //if the instance doesn't exist
            {
                instance = this; //set this as instance
            }
            else if (instance != this) //if there is an instance but it isn't this object
            {
                Destroy(gameObject); //delete this
                return; //exit code early
            }
            DontDestroyOnLoad(gameObject); //always be able to access the original instance
        }
        #endregion
        void Update()
        {

        }

        #region Functions
        /// <summary>Gives player the money sent from the enemy on its death.</summary>
        public void AddMoney(int _money)
        {
            money += _money;
        }
        /// <summary>Gives player the money sent from the enemy on its death.</summary>
        public void AddMoney(Enemy _enemy)
        {
            money += _enemy.Money;
        }

        /// <summary>Takes money from the player when purchasing a tower.</summary>
        /// <param name="_tower">The Tower being bought.</param>
        public void PurchaseTower(Tower _tower)
        {
            money -= _tower.Cost;
        }
        #endregion
    }
}