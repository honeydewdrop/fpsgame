using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public int damageAmount;

    public float bulletSpeed = 5f;

    public Rigidbody2D theRB;

    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = PlayerController.instance.transform.position - transform.position; // following the player
        direction.Normalize();
        direction = direction * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = direction * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player") // if enemy bullet comes in contact w player
        {
            PlayerController.instance.takeDamage(damageAmount); // make the instance of the player take damage according to the passed in damageamount
            Destroy(gameObject); // delete enemy bullet from scene
        }
        if(other.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
