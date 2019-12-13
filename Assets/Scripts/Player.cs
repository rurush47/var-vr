using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float speed = 0.2f;
        public void Move(Vector3 moveVector)
        {
            transform.position += new Vector3(moveVector.x, 0, 0);
        }

        private void Update()
        {
            transform.position += new Vector3(0, 0, speed);
        }
    }
}