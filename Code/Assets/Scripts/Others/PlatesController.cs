using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesController : MonoBehaviour
{
    //for timer
    public bool isTiming;
    public float time = 15;
    public float timer;
    public AudioClip audioClip;

    //for drag
    public bool isDraggable;
    public Transform sphere;

    private ConveyerController conveyer;
    private ChefController chefController;
    private MainPanelController mainPanel;

    //for check complete the tour
    private bool back;
    private Vector3 startPos;
    private float speed;


    // Start is called before the first frame update
    void Start()
    {
        conveyer = (ConveyerController)FindObjectOfType(typeof(ConveyerController));
        chefController = (ChefController)FindObjectOfType(typeof(ChefController));
        mainPanel = (MainPanelController)FindObjectOfType(typeof(MainPanelController));

        startPos = transform.position;
        back = false;

        sphere = null;
        isDraggable = true;
        isTiming = false;
        timer = time;
    }

    private void Update()
    {
        if (isTiming)
        {
            //Debug.Log("time :" + timer);
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                //finished on table
                mainPanel.finishedPlate += 1;
                Destroy(transform.gameObject);
            }
        }

        speed = conveyer.speed;
    }

    private void OnCollisionStay(Collision collision)
    {
        switch (collision.collider.name)
        {
            case "belt_s":
                {
                    transform.Translate(Vector3.back * speed * Time.deltaTime);
              
                    break;
                }
            case "belt_w":
                {
                    //Debug.Log("enter w");
                    transform.Translate(Vector3.left * speed * Time.deltaTime);
          
                    break;
                }
            case "belt_n":
                {
                    //Debug.Log("enter n");
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);

                    break;
                }
            case "belt_e":
                {
                    //Debug.Log("enter e");
                    transform.Translate(Vector3.right * speed * Time.deltaTime);
            
                    break;
                }
        }

        //check if complete the whole tour;
        if (!(System.Math.Abs(transform.position.x - startPos.x) < 0.1f && (System.Math.Abs(transform.position.z - startPos.z) < 0.1f)))
        {
            back = true;
        }
        else
        {
            if (back)
            {
                //complete tour
                chefController.plates.Remove(transform.gameObject);
                DestroyPlate(transform.gameObject);
            }
        }

        if (collision.collider.CompareTag("table")&&!isTiming)
        {
            //Debug.Log("on table");
            isDraggable = false;
            //begin timer
            isTiming = true;
            timer = time;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //collide with another plate/ drop on the floor
        if (collision.gameObject.CompareTag("plate") || collision.gameObject.CompareTag("plane"))
        {
            DestroyPlate(transform.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("sphere"))
        {
            Debug.Log("exit sphere from plate");
            DestroyPlate(transform.gameObject);
        }
    }

    public void DestroyPlate(GameObject plate)
    {
        Debug.Log("destroy with sound");
        AudioSource.PlayClipAtPoint(audioClip, plate.transform.position);
        mainPanel.destroyedPlate += 1;
        Destroy(plate);
    }

    private void OnDestroy()
    {
        if (sphere != null)
        {
            Destroy(sphere.gameObject);
        }
    }
}
