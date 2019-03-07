using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectController : MonoBehaviour
{
    //for panels
    public GameObject conveyerPanel;
    public GameObject chefPanel;
    public GameObject saucePanel;
    public GameObject sphere;

    //for select
    private GameObject selectedObj;
    private bool isSelected;
    private Color selectedColor = Color.gray;

    private ConveyerController conveyer;

    //for drag
    private GameObject draggedObj;
    private float dist;
    private Vector3 offset;
    private Transform newSphere;

    // Start is called before the first frame update
    void Start()
    {
        isSelected = false;
        conveyer = (ConveyerController)FindObjectOfType(typeof(ConveyerController));

        //panel init
        conveyerPanel.SetActive(false);
        saucePanel.SetActive(false);
        chefPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos;
        Vector3 touchPos;
        int i = 0;
        int id = 0;
        //if using finger
        //Touch[] touches = Input.touches;
        //if (Input.touchCount == 1)
        //{
        //    touchPos = touches[0].position;
        //    //id = touches[0].fingerId;
        //}
        //else
        //{
        //    return;
        //}
        //else
        //{

        touchPos = Input.mousePosition;
        //}
        //for(int i = 0; i < Input.touchCount; i++)
        //{

        //OnMouseDown()
        //if(touches[i].phase == TouchPhase.Began)
        if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(touchPos);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // if it hit a plate
                if (hit.collider != null && hit.transform.CompareTag("plate"))
                {
                    Debug.Log("Plate touched");
                    //check if this plate is draggable
                    PlatesController platesController = hit.collider.GetComponent<PlatesController>();
                    if (platesController.isDraggable)
                    {
                        draggedObj = hit.transform.gameObject;
                        dist = Camera.main.WorldToScreenPoint(hit.transform.position).z;
                        newPos = Camera.main.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, dist));
                        offset = hit.transform.position - newPos;
                        SelectPlate(draggedObj);
                    }
                }

                //if select conveyer/chef/sauce plate/camera
                else if (hit.collider != null&&hit.collider.name!="Plane"&&hit.collider.name!="Sphere")
                {
                    if (isSelected)
                    {
                        //if the original selected object is same as now
                        if (selectedObj.transform.CompareTag(hit.transform.tag))
                        {
                            //just de-select it
                            DeselectHandler(selectedObj);
                        }
                        else
                        {
                            //de-select the original one
                            DeselectHandler(selectedObj);
                            //select current one
                            SelectHandler(hit.transform.gameObject);
                        }
                    }
                    else
                    {
                        SelectHandler(hit.transform.gameObject);
                    }
                }

            }
        }

        //OnMouseDrag
        //if( touches[i].phase == TouchPhase.Moved || touches[i].phase == TouchPhase.Stationary)
        if ((Input.GetMouseButton(0)) && draggedObj != null)
        {
            //Debug.Log("Plate dragged");
            newPos = Camera.main.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, dist));
            draggedObj.transform.position = newPos + offset;
        }

        //OnMouseUp
        //if( touches[i].phase == TouchPhase.Ended || touches[i].phase == TouchPhase.Canceled)
        if ((Input.GetMouseButtonUp(0) ) && draggedObj!=null)
        {
            Debug.Log("Plate Released");
            DeselectHandler(draggedObj);
            draggedObj = null;
        }
        //}
    }

    private void SelectHandler(GameObject obj)
    {
        if (obj.CompareTag("conveyer"))
        {
            Debug.Log("Conveyer selected");
            SelectConveyer();
        }
        if (obj.CompareTag("chef"))
        {
            Debug.Log("Chef selected");
            SelectChef(obj);
        }
        if (obj.CompareTag("plate"))
        {
            Debug.Log("Plate selected");
            SelectPlate(obj);
        }
        if (obj.CompareTag("sauce"))
        {
            Debug.Log("Sauce selected");
            SelectSauce(obj);
        }
        else
        {
            Debug.Log(obj.name);
        }
        selectedObj = obj;
        isSelected = true;
    }

    private void DeselectHandler(GameObject obj) 
    {
        if (obj.CompareTag("conveyer"))
        {
            Debug.Log("Conveyer de-selected");
            DeSelectConveyer();
        }
        if (obj.CompareTag("chef"))
        {
            Debug.Log("Chef de-selected");
            //de-select chef
            DeSelectChef();
        }
        if (obj.CompareTag("plate"))
        {
            DeselectPlate(obj);
        }
        if (obj.CompareTag("sauce"))
        {
            Debug.Log("Sauce deselected");
            DeselectSauce(obj);
        }
        else
        {
            Debug.Log(obj.name);
        }

    }

    private void SetSelectColor(GameObject obj)
    {
        obj.GetComponent<Renderer>().material.color = selectedColor;
    }

    private void SelectConveyer()
    {
        //show the panel
        conveyerPanel.SetActive(true);

        GameObject[] conveyers = GameObject.FindGameObjectsWithTag("conveyer");
        foreach (GameObject c in conveyers)
        {
            SetSelectColor(c);
        }
        //stop the moving plate
        conveyer.speed = 0f;

    }

    public void DeSelectConveyer()
    {
        //hide the panel
        conveyerPanel.SetActive(false);

        //move the plate TODO did not resume
        conveyer.speed = conveyer.oriSpeed;

        Debug.Log("after deselect speed " + conveyer.speed);

        Color oriConveyerColor = new Color(0.991f, 0.823f, 0.528f, 1f);
        GameObject[] conveyers = GameObject.FindGameObjectsWithTag("conveyer");
        foreach (GameObject c in conveyers)
        {
            c.GetComponent<Renderer>().material.color = oriConveyerColor;
        }

        selectedObj = null;
        isSelected = false;
    }

    private void SelectChef(GameObject obj)
    {
        //show the panel
        chefPanel.SetActive(true);

        //change color
        SetSelectColor(obj);
    }

    public void DeSelectChef()
    {
        //hide the panel
        if (chefPanel.activeSelf)
        {
            chefPanel.SetActive(false);
        }

        //turn to original color
        Color oriChefColor = new Color(1f, 0.986f, 0.986f, 1.000f);
        selectedObj.GetComponent<Renderer>().material.color = oriChefColor;

        selectedObj = null;
        isSelected = false;
    }

    private void SelectPlate(GameObject obj)
    {
        SetSelectColor(obj);
        Debug.Log("init sphere");
        newSphere = Instantiate(sphere.transform, draggedObj.transform.position, Quaternion.identity);
        obj.GetComponent<PlatesController>().sphere = newSphere;
        //stop scoop/sauce
        //if(obj.name == "IcecreamPlate")
        //{
        //    //TODO
        //}else if(obj.name == "SpecialPlate")
        //{
        //    //TODO
        //}
    }

    private void DeselectPlate(GameObject obj)
    {
        //Color oriColor = new Color();
        obj.GetComponent<Renderer>().material.color = new Color(0.943f, 0.619f, 0.619f, 1.000f);
        Destroy(newSphere.gameObject);
    }

    private void SelectSauce(GameObject obj)
    {
        //stop moving
        GameObject sauce1 = obj;
        GameObject sauce2 = sauce1.transform.parent.GetChild(sauce1.transform.GetSiblingIndex() + 1).gameObject;

        sauce1.GetComponent<SauceOrbiter>().speed = 0;
        SetSelectColor(sauce1);
        sauce2.GetComponent<SauceOrbiter>().speed = 0;
        SetSelectColor(sauce2);
    }

    private void DeselectSauce(GameObject obj)
    {
        GameObject sauce1 = obj;
        GameObject sauce2 = sauce1.transform.parent.GetChild(sauce1.transform.GetSiblingIndex() + 1).gameObject;

        sauce1.GetComponent<SauceOrbiter>().speed = sauce1.GetComponent<SauceOrbiter>().oriSpeed;
        sauce1.GetComponent<Renderer>().material.color = new Color(1f, 0.55f, 0.22f, 1.000f);

        sauce2.GetComponent<SauceOrbiter>().speed = sauce2.GetComponent<SauceOrbiter>().oriSpeed;
        sauce2.GetComponent<Renderer>().material.color = new Color(0.11f, 0.73f, 0.2f, 1f);

       
    }
}
