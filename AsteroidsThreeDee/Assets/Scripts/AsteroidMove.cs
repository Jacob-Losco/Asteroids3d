using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMove : MonoBehaviour
{
    public ManageAsteroids manager;
    public float despawnDistance = 100;
    public float speed = 5;
    public Vector3 dir;
    public int size = 1;
    public bool locked = false; //used for testing only!
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

    void DriftAway()
    {
        Vector3 relpos = manager.transform.position - this.transform.position;
        if (relpos.magnitude > despawnDistance)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DriftAway();
        this.transform.position += speed * Time.deltaTime * dir;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");

        GameObject obj = collision.gameObject;
        Vector3 diff = (collision.transform.position - this.transform.position).normalized;
        if (obj.tag == "Asteroid")
        {
            AsteroidMove src = obj.GetComponent<AsteroidMove>();
            dir += diff * 0.5f;
            src.dir -= diff * 0.5f;
            
        }
    }
}
