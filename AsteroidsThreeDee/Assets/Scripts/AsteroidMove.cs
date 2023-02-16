using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMove : MonoBehaviour
{
    public ManageAsteroids manager;
    public float speed = 5;
    public Vector3 dir;
    public int size = 1;
    public bool locked = false; //used for testing only!
    private bool despawning = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!locked)
        {
            dir = Random.onUnitSphere;
            Vector3 rotation = Random.onUnitSphere;
            transform.rotation = new Quaternion(rotation.x, rotation.y, rotation.z, 1);
        }
    }

    void CheckDespawn()
    {
        Vector3 relpos = this.transform.position - manager.transform.position;
        float angle = Mathf.Abs(Vector3.Angle(relpos, manager.transform.forward));
        float despawnRadius = manager.maxSpawnRadius;
        if (angle > 110) despawnRadius /= manager.rearCulling;
        if (relpos.magnitude > despawnRadius)
        {
            despawning = true;
            StartCoroutine(Despawn());
        }
        else despawning = false;
    }
    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(Random.Range(0,5)); //delay despawn if player returns 
        if (despawning) Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        CheckDespawn();
        this.transform.position += speed * Time.deltaTime * dir;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided");

        GameObject obj = other.gameObject;
        Vector3 diff = (this.transform.position-other.transform.position).normalized;
        if (obj.tag == "Asteroid")
        {
            AsteroidMove src = obj.GetComponent<AsteroidMove>();
            dir += diff;
            src.dir -= diff;
            
        }
    }
}
