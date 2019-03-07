using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{
    private int total;
    private List<Collider> collidedObjects = new List<Collider>();
    // Start is called before the first frame update
    void Start()
    {
        total = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = new Collider[collidedObjects.Count];

        collidedObjects.CopyTo(colliders);
        foreach(Collider c in colliders)
        {
            if (c == null)
            {
                collidedObjects.Remove(c);
                Debug.Log("count" + collidedObjects.Count);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collidedObjects.Contains(collision.collider) && collision.collider.tag == "plate")
        {
            collidedObjects.Add(collision.collider);
        }

        //Debug.Log("total" + collidedObjects.Count);
        if (collidedObjects.Count > 4)
        {
            Debug.Log("destroy");
            collidedObjects.Remove(collision.collider);
            Destroy(collision.gameObject);

        }
    }
}
