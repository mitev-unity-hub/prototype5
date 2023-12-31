using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    private Rigidbody enemyRigidBody;
    private GameObject player;
    private int destroyThreshold = -10;
    // Start is called before the first frame update
    private void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRigidBody.AddForce( lookDirection * speed * Time.deltaTime);

        if (transform.position.y < destroyThreshold) 
        { 
            Destroy(gameObject);
        }
    }
}
