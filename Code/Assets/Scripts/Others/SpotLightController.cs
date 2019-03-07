using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightController : MonoBehaviour
{
    public GameObject target;
    public GameObject originPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target!=null && target.activeInHierarchy)
        {
            transform.LookAt(target.transform);
        }
        else
        {
            transform.LookAt(originPos.transform);
        }
    }
}
