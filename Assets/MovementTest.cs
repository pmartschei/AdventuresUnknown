using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{

    private Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rigidBody.AddForce(Vector3.up*5);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigidBody.AddForce(-Vector3.up * 5);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigidBody.AddForce(Vector3.right * 5);
            //Quaternion deltaRotation = Quaternion.Euler(new Vector3(0,0,-250) * Time.deltaTime);
            //rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigidBody.AddForce(-Vector3.right * 5);
            //Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 0, 250) * Time.deltaTime);
            //rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);
        }
    }
    private void FixedUpdate()
    {
        if (rigidBody.velocity.magnitude > 3)
        {
            rigidBody.velocity = rigidBody.velocity.normalized * 3;
        }
    }
}
