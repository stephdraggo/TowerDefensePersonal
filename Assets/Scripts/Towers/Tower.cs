using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefence.Enemies;
using TowerDefence.Managers;

namespace TowerDefence.Towers
{
    [AddComponentMenu("Mechanics/Tower")]
    public abstract class Tower : MonoBehaviour
    {
        #region Variables
        [Header("General Stats")]
        [SerializeField] private string towerName = "";
        [SerializeField] private int level = 1;
        [SerializeField, TextArea] private string description = "";
        [SerializeField, Range(1, 10)] private int cost = 1;

        [Header("Experience Stats")]
        [SerializeField, Range(2, 5)] private int maxLevel = 3;
        [SerializeField, Min(1)] private float baseRequiredXp = 5;
        [SerializeField, Min(1)] private float xpScaler = 1;

        [Header("Attack Stats")]
        [SerializeField, Min(0.1f)] private float damage = 1;
        [SerializeField, Min(0.1f)] private float minRange = 1;
        [SerializeField] private float maxRange = 5;
        [SerializeField, Min(0.01f)] protected float fireRate = 0.1f;

        private float xp;
        private Enemy target;
        private float currentTime = 0;

        #endregion
        #region Properties
        //get can access these variables but not change them
        public string TowerName
        {
            get => towerName;
        }
        public string Description
        {
            get => description;
        }
        public int Cost
        {
            get => cost;
        }
        public float MinRange
        {
            get => minRange;
        }

        /// <summary>Maximum range of the tower based on its level</summary>
        public float MaxRange
        {
            get => maxRange * (level * 0.5f + 0.5f); //multiply is faster than divide for computers
        }

        /// <summary>Amount of damage the tower does multiplied by level.</summary>
        public float Damage
        {
            get => damage * (level * 0.5f + 0.5f);
        }

        /// <summary>Gets formatted string containing all the information about a tower to be displayed.</summary>
        public string UiDisplayText
        {
            get
            {
                string display = string.Format("Name: {0} Cost: {1}\n", towerName, cost); //changes numbers to strings automatically
                display += description + "\n";
                display += string.Format("Min Range: {0}, Max Range: {1}, Damage: {2}", minRange, maxRange, damage);

                return display;
            }
        }

        /// <summary>Calculates the required xp based on current level and xpScaler</summary>
        private float RequiredXp
        {
            get
            {
                if (level == 1) //if level 1
                {
                    return baseRequiredXp; //return base required xp and exit
                }
                return baseRequiredXp * level * xpScaler; //multiply base xp by level and xpScaler and return value
            }
        }
        /// <summary>Enemy currently being targeted by this tower. Null if no target found.</summary>
        protected Enemy Target
        {
            get => target;
        }
        #endregion
        #region Unity Editor Functions
#if UNITY_EDITOR
        void OnValidate() //updates when inspector is changed
        {
            maxRange = Mathf.Clamp(maxRange, minRange + 1, float.MaxValue); //maxRange will always be at least 1 more than minRange
        }

        void OnDrawGizmosSelected() //draws only when object is selected
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f); //transluscent red
            Gizmos.DrawSphere(transform.position, MinRange); //min range sphere

            Gizmos.color = new Color(0, 0, 1, 0.25f); //transluscent blue
            Gizmos.DrawSphere(transform.position, MaxRange); //max range sphere
        }
#endif
        #endregion
        protected virtual void Update()
        {
            FindTarget(); //call targetting function
            FireWhenReady(); //call fire when ready function
        }
        #region Functions
        #region public
        /// <summary>Handles adding xp and checks if should level up.</summary>
        /// <param name="_xp">xp value to be added.</param>
        public void AddExperience(float _xp)
        {
            xp += _xp; //add xp amount sent from Enemy
            if (level < maxLevel && xp >= RequiredXp) //if level is not max and xp has reached required xp
            {
                LevelUp(); //call level up function
            }
        }
        #endregion
        #region protected
        protected abstract void RenderAttackVisuals();
        protected abstract void RenderLevelUpVisuals();
        #endregion
        #region private
        /// <summary>Finds and assigns a target</summary>
        private void FindTarget()
        {
            Enemy[] _enemies = EnemyManager.instance.GetClosestEnemy(transform, MaxRange, minRange); //get the enemy manager to find the enemies within range

            target = GetClosestEnemy(_enemies); //get the closest enemy from the array
        }

        /// <summary>Finds closest enemy based on array/list given.</summary>
        /// <param name="_enemies">Array/list of enemies.</param>
        /// <returns>Closest enemy.</returns>
        private Enemy GetClosestEnemy(Enemy[] _enemies)
        {
            float closestDistance = float.MaxValue; //new float of highest value
            Enemy closest = null; //closest enemy is null
            foreach (Enemy enemy in _enemies) //for each enemy in the given array/list
            {
                float distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position); //distance between tower and enemy
                if (distanceToEnemy < closestDistance) //if the distance is less than the saved closest distance
                {
                    closestDistance = distanceToEnemy; //change closest distance to distance from enemy
                    closest = enemy; //set closest enemy to current enemy
                }
            }
            return closest; //return the closest enemy
        }

        /// <summary>If there is a target, handles timer for fire rate.</summary>
        private void FireWhenReady()
        {
            if (target != null) //if there is a target
            {
                if (currentTime < fireRate) //if we haven't reached the fireRate timer
                {
                    currentTime += Time.deltaTime; //add to the timer
                }
                else //if we have reached the fireRate timer
                {
                    currentTime = 0; //reset the time
                    Fire(); //call the fire function
                }
            }
        }

        /// <summary>If there is a target, sends damage amount to enemy script.</summary>
        private void Fire()
        {
            if (target != null) //if there is a target
            {
                target.Damage(this); //tell the target to take damage according this tower's damage rate

                RenderAttackVisuals();
            }
        }

        /// <summary>Resets xp and handles levelling up.</summary>
        private void LevelUp()
        {
            xp -= RequiredXp; //reset xp while keeping any overflow
            level++; //increase level

            RenderLevelUpVisuals();
        }
        #endregion
        #endregion
    }
}