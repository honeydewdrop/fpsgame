using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{

    private SpriteRenderer theSR;

    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        theSR.flipX = true; // og is flipped wrong so this makes it readable the right way
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(PlayerController.instance.transform.position, -Vector3.forward); // always facing player
    }
}
