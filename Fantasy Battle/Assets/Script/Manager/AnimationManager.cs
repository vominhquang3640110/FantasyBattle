using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class AnimationManager : MonoBehaviour
    {
        static public AnimationManager instance;

        public float touchSpeed;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void TouchAnimation(RectTransform card)
        {
            float targetAngle = 90f;
            float currentAngle = card.localRotation.eulerAngles.z;
            if (currentAngle < targetAngle)
            {
                currentAngle += touchSpeed;
                card.localRotation = Quaternion.Euler(0, 0, currentAngle);
            }
            if (currentAngle > targetAngle)
            {
                card.localRotation = Quaternion.Euler(0, 0, targetAngle);
            }
        }
        public void UnTouchAnimation(RectTransform card)
        {
            float targetAngle = 0f;
            float currentAngle = card.localRotation.eulerAngles.z;
            if (currentAngle > targetAngle && currentAngle <= 90)
            {
                currentAngle -= touchSpeed;
                card.localRotation = Quaternion.Euler(0, 0, currentAngle);
            }
            else if (currentAngle != 0)
            {
                card.localRotation = Quaternion.Euler(0, 0, targetAngle);
            }
        }
    }
}
