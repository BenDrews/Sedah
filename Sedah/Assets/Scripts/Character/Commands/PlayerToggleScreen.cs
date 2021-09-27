using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerToggleScreen : NetworkBehaviour
{

    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetCanvas(Canvas target)
    {
        canvas = target;
        Debug.Log("activated" + canvas);
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.M) && canvas != null)
        {
            if (canvas.enabled)
            {
                canvas.enabled = false;
            }
            else
            {
                canvas.enabled = true;
            }
        }
    }
}
