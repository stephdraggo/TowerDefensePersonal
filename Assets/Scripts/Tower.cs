using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower
{
    [AddComponentMenu("Mechanics/Tower")]
    public class Tower : MonoBehaviour
    {
        #region Variables
        [Header("Reference Variables")]
        [Header("General Stats")]
        [SerializeField] private string towerName = "";
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
        [SerializeField, Min(0.1f)] private float fireRate = 0.1f;

        private int level = 1;
        private float xp;
        private Enemy target;

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
        private float RequiredXp
        {
            get
            {
                if (level == 1)
                {
                    return baseRequiredXp;
                }
                return baseRequiredXp * level * xpScaler;
            }
        }
        private float MinRange
        {
            get => minRange;
        }
        private float MaxRange
        {
            get => maxRange * (level * 0.5f + 0.5f); //multiply is faster than divide for computers
        }
        private float Damage
        {
            get => damage * (level * 0.5f + 0.5f);
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
            Gizmos.DrawSphere(transform.position, minRange); //min range sphere

            Gizmos.color = new Color(0, 0, 1, 0.25f); //transluscent blue
            Gizmos.DrawSphere(transform.position, maxRange); //max range sphere
        }
#endif
        #endregion
        void Start()
        {

        }

        void Update()
        {

        }


    }
}