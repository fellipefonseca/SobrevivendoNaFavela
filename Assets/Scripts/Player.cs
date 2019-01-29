using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
	{
		

        public int maxLife = 3;
        public int currentLife;
        private UIManager uiManager;
        public float minSpeed = 10f;
        public float maxSpeed = 15f;
        public float invicibleTime;
        public GameObject model;           
        private bool invencible = false;
        Animator m_Animator;
        private int currentLane = 1;
        private Vector3 verticalTargetPosition;

        public float laneSpeed;
        public Vector3 teleportPoint;
        public Rigidbody rb;
        float m_Speed;
        public bool stopRun = false;     
   
        void Start()
        {
            m_Animator = GetComponent<Animator>();

            currentLife = maxLife;
            uiManager = FindObjectOfType<UIManager>();
            rb = GetComponent<Rigidbody>();
            m_Speed = 155.0f;
        }

         void FixedUpdate()
        {
      

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


  

}   


