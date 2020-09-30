using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefence.Managers;

namespace TowerDefence.Towers
{
    [AddComponentMenu("Mechanics/Towers/Tower Platform")]
    public class TowerPlatform : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private Transform towerHolder;

        [Tooltip("Tower being held on this platform.")]
        private Tower heldTower;
        #endregion
        #region Functions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tower">Tower type being purchased for this platform.</param>
        public void AddTower(Tower _tower)
        {
            heldTower = _tower;

            _tower.transform.SetParent(towerHolder);
            _tower.transform.localPosition = Vector3.zero;
        }
        /// <summary>
        /// If this platform does not hold a tower, click to purchase tower.
        /// </summary>
        private void OnMouseUpAsButton()
        {
            if (heldTower == null)
            {
                TowerManager.instance.PurchaseTower(this);
            }
        }
        #endregion
    }
}