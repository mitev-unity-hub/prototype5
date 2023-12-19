using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public bool hasPowerUp = false;
    public int powerUpExpTime = 7;
    public GameObject powerUpIndicator;

    private Rigidbody playerRigidBody;
    private GameObject focalPoint;
    private float knockbackForce = 15f;
    private Vector3 powerUpIndicatorOffset = new Vector3(0, -0.5f, 0);

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
        powerUpIndicator.transform.position = transform.position + powerUpIndicatorOffset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            hasPowerUp = true;
            powerUpIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerUpCountdownRoutine());
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

    IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(powerUpExpTime);
        hasPowerUp = false;
        powerUpIndicator.gameObject.SetActive(false);
    }
}
