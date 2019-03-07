using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseX;
    public float mouseY;

    Vector2 look;
    Vector2 smoothVec;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    public GameObject player;

    private void Start()
    {
        mouseX = 0;
        mouseY = 0;
    }

    private void Update()
    {
        if (player.GetComponent<PlayerController>().isPlayerMode)
        {
            var md = new Vector2(mouseX , mouseY);

            md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity*smoothing));

            smoothVec.x = Mathf.Lerp(smoothVec.x, md.x, 1f / smoothing);
            smoothVec.y = Mathf.Lerp(smoothVec.y, md.y, 1f / smoothing);
            look += smoothVec;

            transform.localRotation = Quaternion.AngleAxis(-look.y, Vector3.right);
            player.transform.localRotation = Quaternion.AngleAxis(look.x, player.transform.up);
        }
    }

    private void FixedUpdate()
    {
        mouseX = 0;
        mouseY = 0;
    }
}
