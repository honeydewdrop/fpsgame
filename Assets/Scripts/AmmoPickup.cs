using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int AmmoAmt = 25;

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
        if(other.tag == "Player") // if the object that collides is the player
        {
            PlayerController.instance.currAmo += AmmoAmt; // the current ammount of ammo that the player has will be increased by the ammo ammt of the pick up item
            PlayerController.instance.UpdateAmmoUI();
            Destroy(gameObject); // get rid of the ammo on the ground
        }
    }
}
