using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefence.Towers;

namespace TowerDefence.Managers
{
    public class TowerManager : MonoBehaviour
    {
        #region Variables
        public static TowerManager instance = null;

        [SerializeField,Tooltip("List of tower types that can be spawned.")]
        private List<Tower> spawnableTowers = new List<Tower>();

        [Tooltip("List of towers currently in game.")]
        private List<Tower> livingTowers = new List<Tower>();

        [Tooltip("The tower being purchased.")]
        private Tower towerToPurchase;
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
        #region Start
        void Start()
        {
            towerToPurchase = spawnableTowers[0]; //set tower to purchase to the first tower in the spawnable list
        }
        #endregion

        void Update()
        {

        }
        #region Functions (PurchaseTower)
        /// <summary>
        /// Takes money from the player, adds the tower to the platform and adds the tower to living towers list.
        /// </summary>
        /// <param name="_platform">The platform to add a tower to.</param>
        public void PurchaseTower(TowerPlatform _platform)
        {
            Player.instance.PurchaseTower(towerToPurchase);

            Tower newTower = Instantiate(towerToPurchase);

            _platform.AddTower(newTower);

            livingTowers.Add(newTower);
        }
        #endregion
    }
}