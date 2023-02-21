using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//David Richman
public class AsteroidMove : MonoBehaviour
{
    public ManageAsteroids manager;
    public float speed = 5;
    public Vector3 dir;
    public int size = 1;
    public float spinSpeed = 1;
    private Vector3 spinAxis;
    public bool locked = false; //used for testing only!
    private bool despawning = false;
    private AudioSource impact;
    
    // Start is called before the first frame update
    void Start()
    {
        impact = GetComponent<AudioSource>();
        spinAxis = Random.onUnitSphere;
        if (!locked)
        {
            spinSpeed = Random.Range(0, spinSpeed);
            dir = Random.onUnitSphere;
            //Texture Rotation;
            Vector3 rotation = Random.onUnitSphere;
            transform.rotation = new Quaternion(rotation.x, rotation.y, rotation.z, 1);
        }
    }
    void Spin()
    {
        transform.Rotate(spinAxis, spinSpeed);
    }

    void CheckDespawn()
    {
        Vector3 relpos = this.transform.position - manager.transform.position;
        float angle = Mathf.Abs(Vector3.Angle(relpos, manager.transform.forward));
        float despawnRadius = manager.maxSpawnRadius;
        if (angle > 110) despawnRadius /= manager.rearCulling;
        if (relpos.magnitude > despawnRadius && !despawning)
        {
            despawning = true;
            StartCoroutine(Despawn());
        }
    }
    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(Random.Range(0,5)); //delay despawn if player returns 
        if (despawning) { Destroy(this.gameObject); manager.decrementAsteroid(); }
        else despawning = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDespawn();
        Spin();
        this.transform.position += speed * Time.deltaTime * dir;
    }
    private void OnTriggerEnter(Collider other)
    {

        GameObject obj = other.gameObject;
        Vector3 diff = (this.transform.position-other.transform.position).normalized;
        if (obj.tag == "AsteroidBig" || obj.tag == "AsteroidMed" || obj.tag == "AsteroidLittle")
        {
            AsteroidMove src = obj.GetComponent<AsteroidMove>();
            dir += diff;
            src.dir -= diff;
        }
        if (obj.tag == "Player")
        {
            if (!impact.isPlaying)
            {
                impact.PlayOneShot(impact.clip);
            }
            StatsManager src = obj.GetComponent<StatsManager>();
            src.health -= 10 * size;
        }
        if (obj.tag == "Bullet") {
            if(!impact.isPlaying) {
                impact.PlayOneShot(impact.clip);
            }
            manager.AsteroidDestroyed(this.transform.position, this.tag);
            Destroy(this.gameObject);
        }
    }
    IEnumerator yieldDestroy()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }
}
