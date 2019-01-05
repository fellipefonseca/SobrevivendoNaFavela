using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class Track : MonoBehaviour
{
    public GameObject[] obstacles;
    public Vector2 numberOfObstacles;
    public List<GameObject> newObstacles;

    // Start is called before the first frame update
    void Start()
    {
        int newNumberOfObstacles = (int)Random.Range(numberOfObstacles.x, numberOfObstacles.y);

        for (int i = 0; i < newNumberOfObstacles; i++)
        {
            newObstacles.Add(Instantiate(obstacles[Random.Range(0, obstacles.Length)], transform));
            newObstacles[i].SetActive(false);
        }
        PositionateObstacles();
    }

    void PositionateObstacles()
    {
        Debug.Log(newObstacles.Count);

        for (int i = 0; i < newObstacles.Count; i++)
        {

            float posZmin = ((167f / newObstacles.Count) + (167f / newObstacles.Count) * i) - 30;
            float posZmax = ((167f / newObstacles.Count) + (167f / newObstacles.Count) * i + 1) - 30;
            newObstacles[i].transform.localPosition = new Vector3(124.62f, 73f, Random.Range(posZmin, posZmax));
            newObstacles[i].SetActive(true);
            Debug.Log("QQQQ");
            if (newObstacles[i].GetComponent<ChangeLane>() != null)
            {
                Debug.Log("QQQQaaa");
                int randomLane = Random.Range(-2, 1);

                newObstacles[i].transform.localPosition = new Vector3(127f + randomLane*2, 73f, Random.Range(posZmin, posZmax));
            }
        }
    }


}
