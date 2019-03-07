using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerController : MonoBehaviour
{
    public float speed;
    public float oriSpeed;
    // Start is called before the first frame update
    void Start()
    {
        this.oriSpeed = 1f;
        this.speed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
