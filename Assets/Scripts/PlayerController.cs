using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody playerRigidBody;
    private GameObject focalPoint;
    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }
    // Update is called once per frame
    private void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");

        playerRigidBody.AddForce(focalPoint.transform.forward * speed * forwardInput * Time.deltaTime);

    }
}
