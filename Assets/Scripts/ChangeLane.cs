
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLane : MonoBehaviour
{

    public void PositionLane()
    {
        int randomLane = Random.Range(-1, 2);
        transform.position = new Vector3(124.62f, 73f, transform.position.z);
    }


}