using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayNWController : MonoBehaviour
{
    private LevelController levelController;
    private int level;
    // Start is called before the first frame update
    void Start()
    {

        levelController = (LevelController)FindObjectOfType(typeof(LevelController));
        level = levelController.level;
    }

    // Update is called once per frame
    void Update()
    {
        level = levelController.level;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (level == 0)
        {
            //move it automatically
            //Debug.Log("got it");
            GameObject plate = collision.gameObject;

            //move it to the trayer
            Vector3 newPos = new Vector3(transform.position.x, plate.transform.position.y, transform.position.z+1.5f);
            plate.transform.position = new Vector3(transform.position.x, plate.transform.position.y, transform.position.z);
            //plate.transform.position = Vector3.MoveTowards(plate.transform.position, newPos, Time.deltaTime);

            //StartCoroutine(MyWait(plate, newPos));
            plate.transform.position = newPos;

        }

    }

    IEnumerator MyWait(GameObject plate, Vector3 newPos)
    {
        //Debug.Log("wait");
        yield return new WaitForSeconds(1);
    }
}
