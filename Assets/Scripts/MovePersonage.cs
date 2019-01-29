using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePersonage : MonoBehaviour
{
    public Vector3 teleportPoint;
    public Rigidbody rb;
    float m_Speed;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        m_Speed = 50.0f;
    }

    void FixedUpdate()
    {
        rb.velocity = transform.forward * m_Speed;
        rb.MovePosition(transform.position + transform.forward * Time.deltaTime);
    }
}
