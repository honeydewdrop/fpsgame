using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthPickupAmt = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") // if the object that collides is the player
        {
            PlayerController.instance.addHealth(healthPickupAmt); // the current ammount of health that the player has will be increased by the health ammt of the pick up item
            Destroy(gameObject); // get rid of the ammo on the ground
        }
    }
}
