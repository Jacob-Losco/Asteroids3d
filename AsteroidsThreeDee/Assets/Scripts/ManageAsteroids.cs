using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageAsteroids : MonoBehaviour
{
    // Start is called before the first frame update
    private int spawnNum = 10;
    public float spawnDensity = 0.5f;
    public float spawnRate = 2;
    private float _spawnRate = 1;
    public float minSpawnRadius = 10;
    public float maxSpawnRadius = 50;
    public int maxNumAsteroids = 500;
    private int numAsteroids = 0;
    public float rearCulling = 100;
    //public int level = 1;
    //public float levelRate = 5; // level changes once every levelRate seconds
    public List<GameObject> asteroids;
    public GameObject asteroidMed;
    public GameObject asteroidLittle;
    private float lastSpawn;


    void Start()
    {
        //player = Instantiate(playerPrefab);
        //player.GetComponent<SpaceshipMove>().manager = this;
        //player.transform.position = Vector2.zero;
       
        if (asteroids.Count == 0)
            return;
        lastSpawn = Time.time - spawnRate;
        
    }
    private IEnumerator SpawnAsteroid()
    {
        yield return new WaitForSeconds(Random.Range(0, _spawnRate));
        int c = Random.Range(0, asteroids.Count); //c: classification
        if (c < asteroids.Count && c >= 0)
        {
            GameObject asteroid = Instantiate(asteroids[c]);
            numAsteroids++;
            NewSpawn(asteroid);
            asteroid.GetComponent<AsteroidMove>().manager = this;
        }
    }
    
    private void NewSpawn(GameObject obj)
    {
        Vector3 pos = Random.onUnitSphere * Random.Range(minSpawnRadius, maxSpawnRadius) ;
        obj.transform.position = pos + transform.position + transform.forward*(maxSpawnRadius/rearCulling);
    }
    
    public void decrementAsteroid() {
        numAsteroids--;
    }
    
    public void AsteroidDestroyed(Vector3 oldAsteroidPosition, string oldAsteroidSize)
    {
        if(oldAsteroidSize == "AsteroidBig") {
            decrementAsteroid();
            GameObject newAsteroidOne = Instantiate(asteroidMed);
            newAsteroidOne.transform.position = oldAsteroidPosition;
            newAsteroidOne.GetComponent<AsteroidMove>().manager = this;
            GameObject newAsteroidTwo = Instantiate(asteroidMed);
            newAsteroidTwo.transform.position = oldAsteroidPosition;
            newAsteroidTwo.GetComponent<AsteroidMove>().manager = this;
        }
        if(oldAsteroidSize == "AsteroidMed") {
            GameObject newAsteroidOne = Instantiate(asteroidLittle);
            newAsteroidOne.transform.position = oldAsteroidPosition;
            newAsteroidOne.GetComponent<AsteroidMove>().manager = this;
            GameObject newAsteroidTwo = Instantiate(asteroidLittle);
            newAsteroidTwo.transform.position = oldAsteroidPosition;
            newAsteroidTwo.GetComponent<AsteroidMove>().manager = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float density = Mathf.Pow(Mathf.Max(maxSpawnRadius - minSpawnRadius, 1) * spawnDensity, 3)*Mathf.PI*(4/3);
        if (spawnRate != 0) _spawnRate = 1 / Mathf.Pow(spawnRate + density,2);
        //scoreText.text = "Score: " + gameScore.ToString();

        float delta = Time.time - lastSpawn;
        if (delta > _spawnRate)
        {
            lastSpawn = Time.time;
            for (int i = 0; i < spawnNum; i++)
            {
                if (asteroids.Count > 0 && numAsteroids < maxNumAsteroids)
                    StartCoroutine(SpawnAsteroid());
            }
        }
    }
}
