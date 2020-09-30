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
        [SerializeField, Tooltip("Rotates on Y axis.")]
        private Transform turret;

        [SerializeField, Tooltip("Rotates up and down.")]
        private Transform gunHolder;

        [SerializeField, Tooltip("Visual of bullet trajectory.")]
        private LineRenderer bulletLine;

        [SerializeField, Tooltip("Which gun to fire from.")]
        private Transform leftFirePoint, rightFirePoint;

        private bool fireLeft = false;

        private float resetTime = 0;
        private bool visualsAreReset = false;
        #endregion
        #region Update
        /// <summary> Overrides Update function with itself plus reset visuals.</summary>
        protected override void Update()
        {
            base.Update(); //keep the base update function and add the following as well

            if (Target == null && !visualsAreReset) //detect if no enemy AND visuals are not reset
            {
                if (resetTime < fireRate) //check if current time < fire rate
                {
                    resetTime += Time.deltaTime; //add to current time
                }
                else
                {
                    bulletLine.positionCount = 0; //disable line renderer
                    resetTime = 0; //reset timer
                    visualsAreReset = true; //visuals have been reset
                }
            }
        }
        #endregion
        #region Functions (RenderAttackVisuals, RenderLevelUpVisuals, RenderBulletLine)
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
            visualsAreReset = false;
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void RenderLevelUpVisuals()
        {
            Debug.Log("leveling up");
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