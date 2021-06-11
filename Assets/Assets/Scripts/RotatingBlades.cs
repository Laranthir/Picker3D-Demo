using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBlades : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;
    private Rigidbody rb;
    private Vector3 m_EulerAngleVelocity;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        m_EulerAngleVelocity = new Vector3(0, 0, rotationSpeed);
    }

    void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
