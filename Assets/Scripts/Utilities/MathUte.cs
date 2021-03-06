﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Utilities
{
    public class MathUte
    {
        /// <summary>
        /// Calculates the distance and direction between two points separately.
        /// </summary>
        /// <param name="_distance">Float distance returned.</param>
        /// <param name="_direction">Vector3 direction returned.</param>
        /// <param name="_from">Origin Transform.</param>
        /// <param name="_to">Target Transform.</param>
        public static void DistanceAndDirection(out float _distance, out Vector3 _direction, Transform _from, Transform _to)
        {
            Vector3 heading = _to.position - _from.position;
            _distance = heading.magnitude;
            _direction = heading.normalized;
        }

        /// <summary>
        /// Calculates the distance and direction between two vector3 separately.
        /// </summary>
        /// <param name="_distance">Float distance returned.</param>
        /// <param name="_direction">Vector3 direction returned.</param>
        /// <param name="_from">Origin Vector3.</param>
        /// <param name="_to">Target Vector3.</param>
        public static void DistanceAndDirection(out float _distance,out Vector3 _direction,Vector3 _from, Vector3 _to)
        {
            Vector3 heading = _to - _from;
            _distance = heading.magnitude;
            _direction = heading.normalized;
        }
    }
}