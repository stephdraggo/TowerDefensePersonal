using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefence.Utilities;

namespace TowerDefence.Towers
{
    [AddComponentMenu("Mechanics/Towers/Machine Gun")]
    public class MachineGunTower : Tower
    {
        #region Variables
        [Header("Machine Gun Specs")]
        [SerializeField,Tooltip("Rotates on Y axis.")]
        private Transform turret;

        [SerializeField,Tooltip("Rotates up and down.")]
        private Transform gunHolder;

        [SerializeField,Tooltip("Visual of bullet trajectory.")]
        private LineRenderer bulletLine;

        [SerializeField,Tooltip("Which gun to fire from.")]
        private Transform leftFirePoint, rightFirePoint;

        private bool fireLeft = false;
        #endregion
        #region Functions
        /// <summary>
        /// Rotates gun holder towards target enemy and renders bullet line, alternating which gun fires.
        /// </summary>
        protected override void RenderAttackVisuals()
        {
            MathUte.DistanceAndDirection(out float distance, out Vector3 direction, gunHolder, Target.transform);
            gunHolder.rotation = Quaternion.LookRotation(direction);

            if (fireLeft)
            {
                RenderBulletLine(leftFirePoint);
            }
            else
            {
                RenderBulletLine(rightFirePoint);
            }
            fireLeft = !fireLeft;
        }
        /// <summary>
        /// Spawns line from origin to target.
        /// </summary>
        /// <param name="_start">Gun's end point.</param>
        private void RenderBulletLine(Transform _start)
        {
            bulletLine.positionCount = 2;
            bulletLine.SetPosition(0, _start.position);
            bulletLine.SetPosition(1, Target.transform.position);
        }
        #endregion
    }
}