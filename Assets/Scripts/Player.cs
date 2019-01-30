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
        public int coins;
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
 

    private void OnTriggerEnter(Collider other)
        {
        if (other.CompareTag("coin")){
            coins++;
            uiManager.UpdateCoins(coins);
            other.transform.parent.gameObject.SetActive(false);
        }
        if (invencible)
            {
                return;
            }
            if (other.CompareTag("Obstacle"))
            {

            //rb.MovePosition(transform.position - transform.forward * (Time.deltaTime));

            currentLife--;
            uiManager.UpdateLives(currentLife);
            if (currentLife == 0)
                {
                stopRun = true;

                m_Speed = 0;
                uiManager.gameOverPanel.SetActive(true);
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
            while(timer < time  && invencible) {
                model.SetActive(enabled);
                yield return null;
                timer += Time.deltaTime;
                lastBliking += Time.deltaTime;
                if(blinkPeriod < lastBliking)
                {
                    lastBliking = 0;
                    currentBlik = 1f - currentBlik;
                    enabled = !enabled;

                }
            }
            model.SetActive(true);
            invencible = false;

        }
        void CallMenu()
    {
        //GameManeger.gm.EndRun();
    }

}   


