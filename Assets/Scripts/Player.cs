using System;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float speed = 0.2f;
        [SerializeField] private float MinShakeInterval = 0.2f;
        [SerializeField] private float sqrShakeDetectionThreshold = 3.6f;

        [Header("duck")] 
        [SerializeField] private float duckTime;
        [SerializeField] private float duckCameraPos = -0.43f;
        [SerializeField] private GameObject cameraObj;
        [SerializeField] private float newColliderHeight;
        private float initColliderHeight;
        
        private float timeSinceLastShake;
        private CapsuleCollider capsuleCollider;

        private void Start()
        {
            capsuleCollider = GetComponent<CapsuleCollider>();
            initColliderHeight = capsuleCollider.height;
        }

        public void Move(Vector3 moveVector)
        {
            transform.position += new Vector3(moveVector.x, 0, 0);
        }

        private void Update()
        {
            transform.position += new Vector3(0, 0, speed);
            
            if ((Input.acceleration.sqrMagnitude >= sqrShakeDetectionThreshold
                && Time.unscaledTime >= timeSinceLastShake + MinShakeInterval) ||
                Input.GetKeyDown("d"))
            {
                timeSinceLastShake = Time.unscaledTime;
                Duck();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.name);
        }

        //should be FSM here
        private bool duck = false;
        public void Duck()
        {
            if (!duck)
            {
                cameraObj.transform
                    .DOLocalMoveY(duckCameraPos, duckTime)
                    .SetEase(Ease.InSine)
                    .OnComplete(() =>
                    {
                        capsuleCollider.height = newColliderHeight;
                        duck = true;
                    });
            }
            else
            {
                cameraObj.transform
                    .DOLocalMoveY(0, duckTime)
                    .SetEase(Ease.OutSine)
                    .OnComplete(() =>
                    {
                        capsuleCollider.height = initColliderHeight;
                        duck = false;
                    });
            }
        }
    }
}