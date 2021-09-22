using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CameraMovement : MonoBehaviour
{
    public bool lockedMode = true;
    private Vector3 forward, right;
    public float cameraSpeed = 0.5f;
    public float cameraDistance = 15f;
    Vector3 buttonDownMousePosition;

    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            lockedMode = !lockedMode;
        }

        if (lockedMode)
        {
            Vector3 playerPos = NetworkClient.localPlayer.transform.position;
            playerPos.x -= cameraDistance;
            playerPos.z -= cameraDistance;
            transform.position = playerPos;
        }

        if (!lockedMode)
        {
            if (Input.GetMouseButtonDown(2))
            {
                buttonDownMousePosition = Input.mousePosition;
            }
        } 
    }

    private void FixedUpdate()
    {
        //if (NetworkClient.localPlayer == null)
        //{
        //    Destroy(this);
        //}
        if (!lockedMode)
        {
            if (Input.GetMouseButton(2))
            {
                Vector3 dir = Input.mousePosition - buttonDownMousePosition;
                Vector3 rightMovement = right * cameraSpeed * Time.deltaTime * dir.x;
                Vector3 forwardMovement = forward * cameraSpeed * Time.deltaTime * dir.y;

                this.transform.position += rightMovement + forwardMovement;
            }
        }
    }
}

