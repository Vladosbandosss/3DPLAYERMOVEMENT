using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeMovementMotor : MonoBehaviour//движуха и поворот!!!!хз как но работает
{
    [HideInInspector]
    public Vector3 movementDirection;

    private Rigidbody rb;

    public float walkingSpeed = 5f;
    public float walkingSnapyness = 50f;
    public float turningSmoothing = 0.3f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 targetVelocity = movementDirection * walkingSpeed;
        Vector3 delataVelocity = targetVelocity - rb.velocity;

        if (rb.useGravity)
        {
            delataVelocity.y = 0f;
        }

        rb.AddForce(delataVelocity * walkingSnapyness, ForceMode.Acceleration);

        Vector3 faceDir = movementDirection;

        if (faceDir == Vector3.zero)
        {
            rb.angularVelocity = Vector3.zero;
        }
        else
        {
            float rotationAngle = AngleAroundAxis(transform.forward,faceDir,Vector3.up);
            rb.angularVelocity = (Vector3.up * rotationAngle * turningSmoothing);
        }
    }

    float AngleAroundAxis(Vector3 dirA,Vector3 dirB,Vector3 axis)
    {
        dirA = dirA - Vector3.Project(dirA, axis);
        dirB = dirB - Vector3.Project(dirB, axis);

        float angle = Vector3.Angle(dirA, dirB);
        return angle*(Vector3.Dot(axis,Vector3.Cross(dirA,dirB))<0?-1:1);
    }

}
