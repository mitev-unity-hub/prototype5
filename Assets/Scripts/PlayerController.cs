using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public bool hasPowerUp = false;

    private Rigidbody playerRigidBody;
    private GameObject focalPoint;
    private float knockbackForce = 15f;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            hasPowerUp = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 knockbackDirection = collision.gameObject.transform.position - transform.position;

            enemyRigidBody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);

            Debug.Log("A player collided with: " + collision.gameObject + ". And has powerUp set to: " + hasPowerUp);
        }
    }
}
