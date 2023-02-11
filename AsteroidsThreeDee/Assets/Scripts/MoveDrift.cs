using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDrift : MonoBehaviour
{
    public ManageAsteroids manager;
    public float despawnDistance = 100;
    public float speed = 5;
    public Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        dir = Random.onUnitSphere;
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
}
