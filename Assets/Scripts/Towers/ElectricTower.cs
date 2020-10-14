using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefence.Utilities;

namespace TowerDefence.Towers
{
    [AddComponentMenu("Mechanics/Towers/Electric")]
    public class ElectricTower : Tower
    {
        #region Variables
        [Header("Electric Specs")]
        [SerializeField]
        private Transform ball;

        [SerializeField]
        private LineRenderer lightningLine;

        [SerializeField, Range(1, 10), Tooltip("Number of segments in one line towards enemy.")]
        private int segments;

        [SerializeField, Range(0.01f, 0.25f), Tooltip("Maximum offset a segment can have.")]
        private float maxVariance = 0.25f;


        private int recursionIndex;
        #endregion



        #region Functions
        protected override void RenderAttackVisuals()
        {
            recursionIndex = 0; //reset recursion index

            MathUte.DistanceAndDirection(out float distance, out Vector3 direction, ball, Target.transform);

            //generate list of segment points between tower and enemy
            List<Vector3> segmentPoints = new List<Vector3>();
            for (int i = 0; i < segments; i++)
            {
                segmentPoints.Add(ball.position + direction * i * (distance / segments));
            }


            RenderLightning(segmentPoints, direction);
        }

        protected override void RenderLevelUpVisuals()
        {

        }


        /// <summary>
        /// Renders lightning attack.
        /// </summary>
        /// <param name="_positions">all segment points</param>
        /// <param name="_direction">direction from tower to target</param>
        private void RenderLightning(List<Vector3> _positions, Vector3 _direction)
        {
            lightningLine.positionCount = segments;
            Vector3 newDirection = Vector3.Cross(_direction, _positions[recursionIndex]);

            _positions[recursionIndex] += newDirection * Random.Range(-maxVariance, maxVariance);
            lightningLine.SetPosition(recursionIndex, _positions[recursionIndex]);

            recursionIndex++;

            if (recursionIndex < segments) //if that wasn't the last segment
            {
                RenderLightning(_positions, _direction); //do it again
            }
        }
        #endregion
    }
}