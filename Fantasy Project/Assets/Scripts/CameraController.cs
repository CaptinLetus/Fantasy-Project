using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed;
    public float velocityRate;
    public Rigidbody rb;
    public Camera uiCamera;

    Vector3 moveVector;

    // Update is called once per frame
    void Update()
    {
        Vector3 rawMoveVector = Vector3.zero;

        // check each key independently so that we can combine keys 
        if (Input.GetKey(KeyCode.W)) rawMoveVector += new Vector3(0, 0, 1);
        if (Input.GetKey(KeyCode.S)) rawMoveVector -= new Vector3(0, 0, 1);
        if (Input.GetKey(KeyCode.A)) rawMoveVector -= new Vector3(1, 0, 0);
        if (Input.GetKey(KeyCode.D)) rawMoveVector += new Vector3(1, 0, 0);

        moveVector = Vector3.ClampMagnitude(rawMoveVector, 1);

        uiCamera.transform.position = transform.position;
        uiCamera.transform.rotation = transform.rotation;
    }

    void FixedUpdate()
    {
        Vector3 goalSpeed = moveVector * moveSpeed;
        rb.velocity = Vector3.Lerp(rb.velocity, goalSpeed, Time.fixedDeltaTime * velocityRate);
    }
}
