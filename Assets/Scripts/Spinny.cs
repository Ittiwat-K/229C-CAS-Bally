using UnityEngine;

public class Spinny : MonoBehaviour
{
    Rigidbody rb;
    public Vector3 angularVelocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.angularVelocity = angularVelocity;
    }
}
