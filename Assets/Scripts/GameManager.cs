using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LockCursor();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) // when player wants to quit/pause let them see the mouse
        {
            UnlockCursor();
        }
        if(Input.GetMouseButton(0)) // if left click relock cursor
        {
            LockCursor();
        }
    }
    
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked; // lock cursor and stop the mouse from being visible on screen 
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None; // allow cursor to be seen
        Cursor.visible = true;
    }
}
