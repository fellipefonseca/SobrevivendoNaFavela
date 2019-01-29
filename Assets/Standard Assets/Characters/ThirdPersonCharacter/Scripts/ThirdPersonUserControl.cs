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
        
        private bool invencible = false;
        public float laneSpeed;
        private int currentLane = 1;
        private Vector3 verticalTargetPosition;
        public Vector3 teleportPoint;
        public Rigidbody rb;
        float m_Speed;

        public bool stopRun = false;

        Animator m_Animator;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            m_Speed = 5.0f;
            m_Animator = GetComponent<Animator>();
            currentLife = maxLife;
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ChangeLane(-1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ChangeLane(1);
            }

            Vector3 targetPosition = new Vector3(verticalTargetPosition.x, verticalTargetPosition.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneSpeed * Time.deltaTime);

        }

        void ChangeLane(int direction)
        {
            int targetLane = currentLane + direction;

            if (targetLane < 0 || targetLane > 2)
                return;

            currentLane = targetLane;
            verticalTargetPosition = new Vector3((currentLane - 1), 0, 0);
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
                // rb.MovePosition(transform.position - transform.forward * (Time.deltaTime));

                currentLife--;
                if (currentLife == 0)
                {

                    stopRun = true;
                    m_Speed = 0;
                    //Invoke("CallMenu", 2f);
                }

            }
        }

    }
}
