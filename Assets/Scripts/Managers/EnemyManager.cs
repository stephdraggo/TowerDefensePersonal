using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefence.Enemies;

namespace TowerDefence.Managers
{
    [AddComponentMenu("Mechanics/Enemy/Manager")]
    public class EnemyManager : MonoBehaviour
    {
        //we making a singleton
        public static EnemyManager instance = null;

        #region Variables
        [SerializeField] private GameObject enemyPrefab;

        private List<Enemy> livingEnemies = new List<Enemy>();



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
        void Start()
        {

        }

        void Update()
        {

        }

        public void SpawnEnemy(Transform _spawner)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, _spawner.position, enemyPrefab.transform.rotation); //new enemy at spawn location 
            livingEnemies.Add(newEnemy.GetComponent<Enemy>()); //add to living enemies list
        }

        /// <summary>
        /// Loops through all enemies, finding closest enemies within a certain range.
        /// </summary>
        /// <param name="_target">The centre of the range.</param>
        /// <param name="_maxRange">The maximum range.</param>
        /// <param name="_minRange">The enemy must be at least this far from the target, defaults to zero.</param>
        /// <returns>Enemy Array</returns>
        public Enemy[] GetClosestEnemy(Transform _target, float _maxRange, float _minRange = 0)
        {
            List<Enemy> closeEnemies = new List<Enemy>(); //new list to fill with enemies
            foreach (Enemy enemy in livingEnemies) //for each enemy that is living
            {
                float distance = Vector3.Distance(enemy.transform.position, _target.position); //distance between that enemy and the target location
                if (_minRange < distance && distance < _maxRange) //cannot combine bc must be true or false
                {
                    closeEnemies.Add(enemy); //add enemy to list of enemies withing range
                }
            }
            return closeEnemies.ToArray(); //convert list to array to make it static
        }
    }
}