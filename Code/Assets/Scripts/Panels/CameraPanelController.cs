using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CameraPanelController : MonoBehaviour
{
    public GameObject player;
    public Camera myCamera;
    public Button up;
    public Button down;
    public Button left;
    public Button right;
    public Toggle fix;

    // Start is called before the first frame update
    void Start()
    {
        up.onClick.AddListener(SetForward);
        down.onClick.AddListener(SetBackward);
        left.onClick.AddListener(SetLeft);
        right.onClick.AddListener(SetRight);
    }

    void SetForward()
    {
        if (player.GetComponent<PlayerController>().isPlayerMode)
        {
            if (!fix.isOn)
            {
                //move the player
                //Debug.Log("player up");
                player.GetComponent<PlayerController>().vertical = 1;
            }
            else
            {
                //rotate the camera
                myCamera.GetComponent<CameraController>().mouseY = 1;
            }
        }
    }

    void SetBackward()
    {
        if (player.GetComponent<PlayerController>().isPlayerMode)
        {
            if (!fix.isOn)
            {
                //move the player
                player.GetComponent<PlayerController>().vertical = -1;
            }
            else
            {
                myCamera.GetComponent<CameraController>().mouseY = -1;

            }
        }
    }

    void SetLeft()
    {
        if (player.GetComponent<PlayerController>().isPlayerMode)
        {
            if (!fix.isOn)
            {
                //move the player
                player.GetComponent<PlayerController>().horizontal = -1;
            }
            else
            {
                myCamera.GetComponent<CameraController>().mouseX = -1;

            }
        }
    }

    void SetRight()
    {
        if (player.GetComponent<PlayerController>().isPlayerMode)
        {
            if (!fix.isOn)
            {
                //move the player
                player.GetComponent<PlayerController>().horizontal = 1;
            }
            else
            {
                myCamera.GetComponent<CameraController>().mouseX = 1;

            }
        }
    }
}
