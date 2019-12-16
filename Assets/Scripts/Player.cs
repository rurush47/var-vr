using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private bool godMode;
        [SerializeField] private float speed = 0.2f;
        [SerializeField] private float MinShakeInterval = 0.2f;
        [SerializeField] private float sqrShakeDetectionThreshold = 3.6f;

        [Header("Duck")]

        [SerializeField] private float duckTime;
        [SerializeField] private float duckCameraPos = -0.43f;
        [SerializeField] private float newColliderHeight;
        private float initColliderHeight;


        [Header("Refs")]
        [SerializeField] private GameObject cameraObj;
        [SerializeField] private TextMeshProUGUI scoreText;

        private float timeSinceLastShake;

        private CapsuleCollider capsuleCollider;
        
        [Header("Speed Increment")]
        [SerializeField] private float timeCheck = 5.0f;
        private float timeVar = 0.0f;

        private void Start()
        {
            capsuleCollider = GetComponent<CapsuleCollider>();
            initColliderHeight = capsuleCollider.height;
            Brake.onBrake += () => speed -= speedIncrease;
        }

        public void Move(Vector3 moveVector)
        {
            transform.position += new Vector3(moveVector.x, 0, 0);
        }

        private void Update()
        {
            if (gameOver)
            {
                return;
            }
            
            transform.position += new Vector3(0, 0, speed);
            
            if ((Input.acceleration.sqrMagnitude >= sqrShakeDetectionThreshold
                && Time.unscaledTime >= timeSinceLastShake + MinShakeInterval) ||
                Input.GetKeyDown("d"))
            {
                timeSinceLastShake = Time.unscaledTime;
                Duck();
            }
            timeVar += Time.deltaTime;
            if(timeVar >= timeCheck) IncrementSpeed();
        }

        [SerializeField] private float speedIncrease = 0.05f;
        private void IncrementSpeed()
        {
            speed += speedIncrease;
            timeVar = 0.0f;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 8)
            {
                Score(other.gameObject);
            }
            if (other.gameObject.layer == 9)
            {
                GameOver();
            }
        }

        private int score = 0;
        private void Score(GameObject gameObject)
        {
            score++;
            scoreText.text = score.ToString(); 
            gameObject.transform.parent.DOScale(Vector3.zero, 0.1f).SetEase(Ease.InSine);
        }

        private bool gameOver;
        private void GameOver()
        {
            if (godMode)
            {
                return;
            }
            
            gameOver = true;
            scoreText.text = "Game Over ! \n Score: " + score;
            GameManager.Instance.GameOver();
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