using System;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

        public int maxLife = 3;
        public int currentLife;
        public float invicibleTime = 2;

        private bool invencible = false;
        public float laneSpeed;
        private int currentLane = 1;
        private Vector3 verticalTargetPosition;
        public Vector3 teleportPoint;
        public Rigidbody rb;
        float m_Speed;

        public bool stopRun = false;

        Animator m_Animator;

        private bool jumping = false;
        private float jumpStart;
        public float jumpLength;
        public float jumpHeight;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            m_Speed = 9.0f;
            m_Animator = GetComponent<Animator>();
            currentLife = maxLife;
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ChangeLane(-2);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ChangeLane(2);
            }else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }

            if (jumping)
            {
                float ratio = (transform.position.z - jumpStart) / jumpLength;
                if(ratio >= 0.5)
                {
                    jumping = false;
                }
                else
                {
                    verticalTargetPosition.y = Mathf.Sin((ratio) * Mathf.PI * jumpHeight) + 2;
                }
            }
            else
            {
                verticalTargetPosition.y = Mathf.MoveTowards(verticalTargetPosition.y, 0 ,5*Time.deltaTime);
            }
            float x = verticalTargetPosition.x + 143;
            float z = transform.position.y + 20;
            Vector3 targetPosition = new Vector3(x, verticalTargetPosition.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneSpeed * Time.deltaTime);


        }

        void Jump()
        {
            if (!jumping)
            {
                jumpStart = transform.position.z;
                jumping = true;
            }
        }

        void ChangeLane(int direction)
        {
            int targetLane = currentLane + direction;

            if (targetLane < -2 || targetLane > 4)
                return;

            currentLane = targetLane;
            float x  = Convert.ToSingle(currentLane - 1);
            verticalTargetPosition = new Vector3(x, 0, 0);
        }
        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {

            if (!stopRun)
            {
                rb.velocity = transform.forward * m_Speed;
                rb.MovePosition(transform.position + transform.forward * Time.deltaTime);
            }
            // read inputs
         
        }

        private void OnTriggerEnter(Collider other)
        {
            if (invencible)
            {
                return;
            }
            if (other.CompareTag("Obstacle"))
            {

                //rb.MovePosition(transform.position - transform.forward * (Time.deltaTime));

                stopRun = true;
                currentLife--;

                if (currentLife == 0)
                {
                    m_Speed = 0;
                    //Invoke("CallMenu", 2f);
                }
                else
                {
                    StartCoroutine(Blinking(invicibleTime));
                }
            }
        }

        IEnumerator Blinking(float time)
        {
            invencible = true;
            float timer = 0;
            float currentBlik = 1f;
            float lastBliking = 0;
            float blinkPeriod = 0.1f;
            bool enabled = false;
            yield return new WaitForSeconds(1f);
            //m_Speed = minSpeed;
            stopRun = false;

            while (timer < time && invencible)
            {
                yield return null;
                timer += Time.deltaTime;
                lastBliking += Time.deltaTime;
                if (blinkPeriod < lastBliking)
                {
                    lastBliking = 0;
                    currentBlik = 1f - currentBlik;
                    enabled = !enabled;

                }
            }
            invencible = false;

        }

    }
}
