using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageAsteroids : MonoBehaviour
{
    // Start is called before the first frame update
    public int spawnNum = 3;
    public float spawnRate = 2;
    public float minSpawnRadius = 10;
    public float maxSpawnRadius = 50;
    //public int level = 1;
    //public float levelRate = 5; // level changes once every levelRate seconds
    public List<GameObject> asteroids;

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

    private IEnumerator Despawn(GameObject obj)
    {
        yield return new WaitForSeconds(Random.Range(1f, 4f));
        Destroy(obj);
    }

    private IEnumerator SpawnAsteroid()
    {
        yield return new WaitForSeconds(Random.Range(0, spawnRate));
        int c = Random.Range(0, asteroids.Count); //c: classification
        if (c < asteroids.Count && c >= 0)
        {
            GameObject asteroid = Instantiate(asteroids[c]);
            NewSpawn(asteroid);
            RotationDrift(asteroid);
            asteroid.GetComponent<MoveDrift>().manager = this;
        }
    }
    private void NewSpawn(GameObject obj)
    {
        Vector3 pos = Random.onUnitSphere * Random.Range(minSpawnRadius, maxSpawnRadius);
        
        //border spawn:
        obj.transform.position = pos + transform.position;
    }
    private void RotationDrift(GameObject obj)
    {
        Vector2 direction = DirectionDrift(obj);
        obj.transform.Rotate(Vector3.forward, Vector2.Angle(Vector2.up, direction));
    }
    private Vector2 DirectionDrift(GameObject obj)
    {
        float tiltX = Random.Range(-8f, 8f);
        float tiltY = Random.Range(-6f, 6f);
        //drift toward center
        Vector2 direction = new Vector2(-obj.transform.position.x + tiltX, -obj.transform.position.y + tiltY);
        if (direction != Vector2.zero)
            direction.Normalize();
        return direction;
    }


    // Update is called once per frame
    void Update()
    {
        //scoreText.text = "Score: " + gameScore.ToString();

        float delta = Time.time - lastSpawn;
        if (delta > spawnRate)
        {
            lastSpawn = Time.time;
            for (int i = 0; i < spawnNum; i++)
            {
                if (asteroids.Count > 0)
                    StartCoroutine(SpawnAsteroid());
            }
        }
    }
}