using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SauceOrbiter : MonoBehaviour
{
    public float speed;
    public int direction;
    public float oriSpeed;
    // Start is called before the first frame update
    void Start()
    {
        oriSpeed = 30;
        this.speed = 30;
        this. direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        var obj = transform.parent;
        transform.RotateAround(obj.position,Vector3.up, direction * speed * Time.deltaTime);
        transform.Rotate(Vector3.up);
    }
}
