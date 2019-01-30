using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class Track : MonoBehaviour
{
    public GameObject[] obstacles;
    public Vector2 numberOfObstacles;
    public List<GameObject> newObstacles;
    public GameObject coin;
    public Vector2 numberOfCoins;
    public List<GameObject> newCoins;
    // Start is called before the first frame update
    void Start()
    {
        int newNumberOfObstacles = (int)Random.Range(numberOfObstacles.x, numberOfObstacles.y);
        int newNumberOfCoins = (int)Random.Range(numberOfCoins.x, numberOfCoins.y);
        for (int i = 0; i < newNumberOfObstacles; i++)
        {
            newObstacles.Add(Instantiate(obstacles[Random.Range(0, obstacles.Length)], transform));
            newObstacles[i].SetActive(false);
        }

        for (int i = 0; i < newNumberOfCoins; i++)
        {
            newCoins.Add(Instantiate(coin, transform));
            newCoins[i].SetActive(false);
        }

        PositionateObstacles();
        PositionateCoins();

    }

    void PositionateObstacles()
    {

        for (int i = 0; i < newObstacles.Count; i++)
        {

            float posZmin = ((167f / newObstacles.Count) + (167f / newObstacles.Count) * i) - 30;
            float posZmax = ((167f / newObstacles.Count) + (167f / newObstacles.Count) * i + 1) - 30;
            newObstacles[i].transform.localPosition = new Vector3(124.62f, 73f, Random.Range(posZmin, posZmax));
            newObstacles[i].SetActive(true);
            if (newObstacles[i].GetComponent<ChangeLane>() != null)
            {
                int randomLane = Random.Range(-2, 1);
                newObstacles[i].transform.localPosition = new Vector3(127f + randomLane*2, 73f, Random.Range(posZmin, posZmax));
            }
        }
    }

    void PositionateCoins()
    {
        float minZPos = -20f;
        for (int i = 0; i < newObstacles.Count; i++)
        {

            float maxZPos = minZPos + 5f;
            float randomZpos = Random.Range(minZPos, maxZPos);
            Debug.Log("pos" + randomZpos);
            newCoins[i].transform.localPosition = new Vector3(124.62f, 73f, randomZpos);
            newCoins[i].SetActive(true);
            newCoins[i].GetComponent<ChangeLane>();
            int randomLane = Random.Range(-2, 1);
            newCoins[i].transform.localPosition = new Vector3(127f + randomLane * 2, 73f, randomZpos);
            minZPos = randomZpos + 8;

        }
    }


}
