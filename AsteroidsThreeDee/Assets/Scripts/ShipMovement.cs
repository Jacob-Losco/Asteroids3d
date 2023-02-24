using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Jacob Losco
public class ShipMovement : MonoBehaviour
{
    public float turnSpeed = 60f;
    public float moveSpeed = 25f;
    public float maxSpeed = 90f;
    public GameObject bullet;
    
    private float actualSpeed = 0;
    private Vector3 direction;
    public float lerpConstant = 1f;
    private float yawing = 0;
    private float pitching = 0;
    private Rigidbody rb;
    private GameObject leftShootPoint;
    private GameObject rightShootPoint;
    private bool inCoolDown = false;
    private ParticleSystem leftSmoke;
    private ParticleSystem rightSmoke;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        leftShootPoint = GameObject.Find("LeftShootPoint");
        rightShootPoint = GameObject.Find("RightShootPoint");
        leftSmoke = GameObject.Find("leftSmoke").GetComponent<ParticleSystem>();
        rightSmoke = GameObject.Find("rightSmoke").GetComponent<ParticleSystem>();
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
        
        if((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftControl)&& !inCoolDown)) {
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
        direction += GetInput();
        direction = Vector3.ClampMagnitude(direction, maxSpeed);
        direction = Vector3.Lerp(direction, Vector3.zero, lerpConstant);
        //direction += Vector3.Lerp(direction, Vector3.zero, lerpConstant);
        transform.position += new Vector3(direction.x, direction.y, direction.z);

    }
    
    void Shoot() {
        inCoolDown = true;
        if (!leftSmoke.isPlaying)
        {
            leftSmoke.Play();
            rightSmoke.Play();
        }
        GameObject leftBullet = Instantiate(bullet);
        GameObject rightBullet = Instantiate(bullet);
        leftBullet.transform.position = leftShootPoint.transform.position;
        leftBullet.transform.rotation = leftShootPoint.transform.rotation;
        rightBullet.transform.position = rightShootPoint.transform.position;
        rightBullet.transform.rotation = rightShootPoint.transform.rotation;
        
        StartCoroutine(CoolDown());
    }
    
    IEnumerator CoolDown() {
        yield return new WaitForSeconds(0.1f);
        inCoolDown = false;
    }
    
    private Vector3 GetInput() {
        actualSpeed = 0;
        if (Input.GetKey("space")) {
            actualSpeed += moveSpeed;
        }
        return actualSpeed * Time.deltaTime * transform.forward;
    }
}
