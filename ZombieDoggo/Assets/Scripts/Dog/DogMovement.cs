﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using de.crystalmesh;

[RequireComponent(typeof(Rigidbody))]
public class DogMovement : MonoBehaviour
{
    [Tooltip("Acceleration in left/right and forward/back direction")]
    [SerializeField]
    private Vector2 acceleration = new Vector2(15.0f, 20.0f);
    [Tooltip("Maximum movement speed in left/right and forward/back direction")]
    [SerializeField]
    private Vector2 maxVelocity = new Vector2(30.0f, 20.0f);

    private Rigidbody dogRigidBody = null;
    private ForceMode accelerationForceMode = ForceMode.VelocityChange;

    // Start is called before the first frame update
    void Awake()
    {
        dogRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 velocityChange = Vector3.zero;
        velocityChange += ProcessHorizontalInput();
        velocityChange += ProcessVerticalInput();
        dogRigidBody.AddForce(velocityChange * Time.fixedDeltaTime, accelerationForceMode);
        dogRigidBody.velocity = Utilities.ClampVector(dogRigidBody.velocity, new Vector3(maxVelocity.x, 0.0f, maxVelocity.y));
    }

    

    private Vector3 ProcessHorizontalInput()
    {
        Vector3 horInputVec = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        Vector3 velocityChange = Vector3.zero;
        if (Mathf.Abs(dogRigidBody.velocity.x) < maxVelocity.x)
            velocityChange = horInputVec* acceleration.x;
        return velocityChange;
    }

    private Vector3 ProcessVerticalInput()
    {
        Vector3 vertInputVec = new Vector3(0f, 0f, Input.GetAxis("Vertical"));
        Vector3 velocityChange = Vector3.zero;
        if (Mathf.Abs(dogRigidBody.velocity.z) < maxVelocity.y)
            velocityChange = vertInputVec* acceleration.y;
        return velocityChange;
    }
}
