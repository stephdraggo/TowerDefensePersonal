using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower
{
    [AddComponentMenu("Mechanics/Enemy")]
    public class Enemy : MonoBehaviour
    {
        #region Variables
        [Header("Reference Variables")]

        [Header("General Stats")]
        [SerializeField] private float health = 1;
        [SerializeField] private float speed = 1;
        [SerializeField] private float size = 1;

        [Header("Attack Stats")]
        [SerializeField] private float damage = 1;
        //[SerializeField] private string[] resistances;

        [Header("On Death")]
        [SerializeField] private float xpGain = 1;
        [SerializeField] private int moneyGain = 1;

        #endregion
        void Start()
        {

        }

        void Update()
        {

        }
    }
}