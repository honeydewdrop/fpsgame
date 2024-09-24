using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 3;
    public float playerRange = 10f; // how close player needs to be to trigger enemy movement

    public Rigidbody2D theRB;
    public float moveSpeed;

    public bool shouldShoot;
    public float fireRate = 0.5f;
    private float shotCounter;
    public GameObject bullet;
    public Transform firePoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            if(Vector3.Distance(transform.position, PlayerController.instance.transform.position) < playerRange) // distance between enemy and player
        {
            Vector3 playerDirection = PlayerController.instance.transform.position - transform.position; // to know which direction player is facing

            theRB.velocity = playerDirection.normalized * moveSpeed; // normalized makes the distance away to 1 on the axis

            if(shouldShoot)
            {
                shotCounter -= Time.deltaTime;
                if(shotCounter < 0)
                {
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    shotCounter = fireRate;
                }
            }
        } else
        {
            theRB.velocity = Vector2.zero;
        }
    }

    public void takeDmg()
    {
        health--; // take away one health point
        if(health <= 0)
        {
            Destroy(gameObject); // enemy dies
        }
    }
}
