using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rotate : MonoBehaviour
{
            
    public float spinSpeed = 500;

    private Matrix4x4 rotateY;
    private float spinner;
    private MeshFilter mf;
    private Vector3[] origVerts;
    private Vector3[] newVerts;
    
    // Start is called before the first frame update
    void Start()
    {
        mf = GetComponent<MeshFilter>();
        origVerts = mf.mesh.vertices;
        newVerts = new Vector3[origVerts.Length];
    }

    // Update is called once per frame
    void Update()
    {
        Matrix4x4 translate = Matrix4x4.Translate(new Vector3(0, 0, 1));
        Vector3 newPosition = translate.MultiplyPoint(transform.position);
        
        spinner += spinSpeed * Time.deltaTime;
        rotateY = Matrix4x4.Rotate(Quaternion.Euler(1, spinner, 1));
        rotateY = rotateY * translate;
        for(int i = 0; i < origVerts.Length; i++) {
            newVerts[i] = rotateY.MultiplyPoint3x4(origVerts[i]);
        }
        mf.mesh.vertices = newVerts;
    }
}

