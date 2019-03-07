using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyOrbiter : MonoBehaviour
{
    public GameObject axis;
    public float speed;
    public int direction;

    // Start is called before the first frame update
    void Start()
    {
        this.speed = 60;
        this.direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(axis.transform.position, axis.transform.right, direction * speed * Time.deltaTime);

    }
}
