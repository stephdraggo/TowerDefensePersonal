using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower
{
    [AddComponentMenu("Mehanics/Tower")]
    public class Tower : MonoBehaviour
    {
        #region Variables
        [Header("Reference Variables")]

        public string towerName;

        [Multiline]
        public string description;

        [Space]

        [SerializeField]
        [Range(-3, 3)]
        [Tooltip("This variable controls how fast and in what direction the object moves up and down.")]
        private float _speed;

        [SerializeField]
        [Min(0)]
        private int _cost;


        #endregion
        void Start()
        {
            towerName = "Kevin";
            _speed = Random.Range(0f, 3f);
            _cost = 5;
        }

        void Update()
        {
            transform.position += transform.up * Time.deltaTime * _speed * Input.GetAxis("Vertical");
        }

        void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(transform.position + transform.up * 2, Vector3.one);

            Gizmos.color = new Color(0, 1, 0, 1f);
            Gizmos.DrawWireCube(transform.position + transform.right * 6, Vector3.one);

            Gizmos.color = new Color(0, 0, 1, 0.75f);
            Gizmos.DrawLine(transform.position + transform.up * 2, transform.position + transform.right * 6);
        }
    }
}