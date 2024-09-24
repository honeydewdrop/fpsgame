using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour

{
    public static PlayerController instance; // true for any vers of pc script

    public Rigidbody2D theRB;

    public Animator gunAnim;

    public float moveSpeed = 3f;

    private Vector2 moveInput; // movement (keys)
    private Vector2 mouseInput; // to control where the player sees

    public float mouseSensitivity = 3f;

    public Camera theViewCam;

    public GameObject bulletImpact;

    public int currAmo;

    public int currHealth;

    public int maxHealth = 100;

    public GameObject deathScreen;

    private bool hasDied;

    public TextMeshProUGUI healthText, ammoText;

    public Animator anim;

    private void Awake()
    {
        instance = this; // this = particular one running now
    }

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        healthText.text = currHealth.ToString() + "%";
        ammoText.text = currAmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasDied)
        {
            moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); // vector2s have x and y vals

            Vector3 moveHorizontal = transform.up * -moveInput.x;

            Vector3 moveVertical = transform.right * moveInput.y;

            theRB.velocity = (moveHorizontal + moveVertical) * moveSpeed; // rb is rigid body. so this controls the body of the object moving thru the world

            mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - mouseInput.x); // rotations measured in 4 values (check debug in unity)

            theViewCam.transform.localRotation = Quaternion.Euler(theViewCam.transform.localRotation.eulerAngles + new Vector3(0f, mouseInput.y, 0f)); // only change y axis

            if (Input.GetMouseButtonDown(0)) // to shoot if player clicks (only when clicked vs every moment it is pressed down)
            {
                if (currAmo > 0) // if theres still bullets to shoot
                {
                    Ray ray = theViewCam.ViewportPointToRay(new Vector3(.5f, .5f, 0f)); // ray of the bullets coming from the player 
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit)) // if hits anything
                    {
                        // Debug.Log("I'm looking at " + hit.transform.name); // where the bullet hits. if it hits the cube wall it says that
                        Instantiate(bulletImpact, hit.point, transform.rotation); // make bullet appear where we hit
                        if (hit.transform.tag == "Enemy")
                        {
                            hit.transform.parent.GetComponent<EnemyController>().takeDmg(); // run the take damage function on the enemy instance if hit
                        }
                    }
                    else
                    {
                        Debug.Log("I'm looking at nothing!");
                    }
                    currAmo--; // subtract from the ammo if an ammo is shot
                    gunAnim.SetTrigger("Shoot");
                    UpdateAmmoUI();
                }
            }

            if(moveInput != Vector2.zero) // if the vector two (player) is not standing still
            {
                anim.SetBool("isMoving", true); 
            } else
            {
                anim.SetBool("isMoving", false);
            }
        }
    }
    public void takeDamage(int damageAmount)
    {
        currHealth -= damageAmount;
        if(currHealth <= 0) // died
        {
            deathScreen.SetActive(true);
            hasDied = true;
            currHealth = 0;
        }

        healthText.text = currHealth.ToString() + "%"; 
    }

    public void addHealth(int healAmount) {
        currHealth += healAmount; // adding the heal amount to the curr health int
        if(currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }
        healthText.text = currHealth.ToString() + "%";
    }

    public void UpdateAmmoUI()
    {
        ammoText.text = currAmo.ToString();
    }

}
