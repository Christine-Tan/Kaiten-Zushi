using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float vertical;
    public float horizontal;
    public float speed = 10.0f;
    public bool isPlayerMode;
    public GameObject cameraPanel;

    private Vector3 oriPos;
    private Quaternion oriRotation;
    private Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        isPlayerMode = false;
        cameraPanel.SetActive(false);
        oriPos = transform.position;
        oriRotation = transform.rotation;
        playerPos = new Vector3(oriPos.x, oriPos.y-10, oriPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerMode)
        {
            float up = vertical * speed;
            float forward = horizontal * speed;
            transform.Translate(0, 5 * up * Time.deltaTime, 5 * forward * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        vertical = 0;
        horizontal = 0;
    }

    public void SetPlayerMode()
    {
        isPlayerMode = true;
        cameraPanel.SetActive(true);

        transform.position = oriPos;
        transform.rotation = oriRotation;
    }

    public void SetOverviewMode()
    {
        isPlayerMode = false;
        transform.position = oriPos;
        transform.rotation = oriRotation;
        cameraPanel.SetActive(false);
    }
}
