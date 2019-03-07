using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoopRotater : MonoBehaviour
{
    public GameObject dummy;

    private Vector3 center;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
      
        //self-rotate 
        transform.Rotate(new Vector3(0,30, 0) * Time.deltaTime);
        transform.position = dummy.transform.position;

    }

}
