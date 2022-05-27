using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSettings : MonoBehaviour
{

    bool isCursorLocked;

    // Start is called before the first frame update
    void Start()
    {
        isCursorLocked = true; ;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isCursorLocked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
