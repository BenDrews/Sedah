using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerToggleScreen : NetworkBehaviour
{

    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetMenu(GameObject target)
    {
        menu = target;
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.M) && menu != null)
        {
            if (menu.active)
            {
                menu.SetActive(false);
            }
            else
            {
                menu.SetActive(true);
            }
        }
    }
}
