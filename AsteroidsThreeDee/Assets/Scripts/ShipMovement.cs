using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float turnSpeed = 60f;
    public float moveSpeed = 25f;
    public GameObject bullet;
    
    private float actualSpeed = 0;
    private Vector3 direction;
    private float lerpConstant = .01f;
    private float yawing = 0;
    private float pitching = 0;
    private Rigidbody rb;
    private GameObject leftShootPoint;
    private GameObject rightShootPoint;
    private bool inCoolDown = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        leftShootPoint = GameObject.Find("LeftShootPoint");
        rightShootPoint = GameObject.Find("RightShootPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("a")) {
            yawing = -1;
        } else if(Input.GetKey("d")) {
            yawing = 1;
        } else {
            yawing = 0;
        }
        
        if(Input.GetKey("w")) {
            pitching = -1;
        } else if(Input.GetKey("s")) {
            pitching = 1;
        } else {
            pitching = 0;
        }
        
        if(Input.GetMouseButtonDown(0)) {
            Shoot();
        }
    
        Turn();
        Move();
    }
    
    void Turn() {
        float yaw = turnSpeed * Time.deltaTime * yawing;
        float pitch = turnSpeed * Time.deltaTime * pitching;
        transform.Rotate(pitch, yaw, 0);
    }
    
    void Move() {
        Vector3 inputVelocity = GetInput();
        direction += inputVelocity;
        Vector3.ClampMagnitude(direction, .01f);
        direction += Vector3.Lerp(direction, Vector3.zero, lerpConstant);
        transform.position = new Vector3(direction.x, direction.y, direction.z);
    }
    
    void Shoot() {
        inCoolDown = true;
        GameObject leftBullet = Instantiate(bullet);
        GameObject rightBullet = Instantiate(bullet);
        leftBullet.transform.position = leftShootPoint.transform.position;
        leftBullet.transform.rotation = leftShootPoint.transform.rotation;
        rightBullet.transform.position = rightShootPoint.transform.position;
        rightBullet.transform.rotation = rightShootPoint.transform.rotation;
        
        StartCoroutine(CoolDown());
    }
    
    IEnumerator CoolDown() {
        yield return new WaitForSeconds(.4f);
        inCoolDown = false;
    }
    
    private Vector3 GetInput() {
        actualSpeed = 0;
        if (Input.GetKey("space")) {
            actualSpeed += moveSpeed;
        }
        
        Vector3 tempVelocity = actualSpeed * transform.forward * Time.deltaTime;
        return tempVelocity;
    }
}
