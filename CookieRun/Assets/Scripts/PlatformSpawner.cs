using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject obstaclePlatformPrefab;
    public int count = 4;

    public float timeBetSpawnMin = 0.25f;
    public float timeBetSpawnMax = 0.45f;

    private float timeBetSpawn;

    public float yMin = -3.5f;
    public float yMax = 1.5f;
    
    public float xMin = 1.4f;
    private float xPos = 20f;


    private GameObject[] platforms;
    private int currentIndex = 0;

    private Vector2 poolPosition = new Vector2(0, -25);
    private float lastSpawnTime;

    private void Start()
    {
        platforms = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            platforms[i] = Instantiate(obstaclePlatformPrefab,poolPosition,Quaternion.identity);
        }

        lastSpawnTime = 0f;
        timeBetSpawn = 0f;
    }

    private void Update()
    {
        if (GameManager.instance.isGameOver)
        {
            return;
        }

        if (Time.time >= lastSpawnTime + timeBetSpawn)
        {
            lastSpawnTime = Time.time;

            timeBetSpawn = Random.Range(timeBetSpawnMin,timeBetSpawnMax);

            float yPos = -4.5f;

            platforms[currentIndex].SetActive(false);
            platforms[currentIndex].SetActive(true);

            platforms[currentIndex].transform.position = new Vector2(xPos, yPos);
            currentIndex++;

            if (currentIndex >= count)
            {
                currentIndex = 0;
            }
            
            

        }
        
    }

}
