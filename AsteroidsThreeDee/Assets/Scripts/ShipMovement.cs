using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float turnSpeed = 60f;
    public float moveSpeed = 45f;
    
    private float yawing = 0;
    private float pitching = 0;
    private float moving = 0;
    void Start()
    {
    
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
        
        if(Input.GetKey("space")) {
            moving = 1;
        } else {
            moving = 0;
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
        transform.position += transform.forward * moveSpeed * Time.deltaTime * moving;
    }
}
