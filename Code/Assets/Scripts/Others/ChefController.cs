using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefController : MonoBehaviour
{
    public GameObject SushiPlate;
    public GameObject SpecialPlate;
    public GameObject IcecreamPlate;
    public GameObject SpotLight;
    public float time;
    public Transform oriPlate;
    public List<GameObject> plates;

    private int oldest;


    // Start is called before the first frame update
    void Start()
    {
        time = 5;
        plates = new List<GameObject>();
        oldest = 0;
        CreatePlate();
    }

    // Update is called once per frame
    void Update()
    {

        //make the spot light look at oldest plate
        SpotLightController sl = (SpotLightController)FindObjectOfType(typeof(SpotLightController));
        if (plates.Count > 0)
        {
            sl.target = plates[oldest].gameObject;
        }
        else
        {
            sl.target = null;
        }

    }

    void CreatePlate()
    {
        //Debug.Log(r);

        //create new plate randomly
        int r = Random.Range(0, 3);
        if (r % 3 == 0)
        {
            Transform newPlate = Instantiate(SushiPlate.transform, oriPlate.position, Quaternion.identity);
            plates.Add(newPlate.gameObject);
        }
        else if (r % 3 == 1)
        {
            Transform newPlate = Instantiate(SpecialPlate.transform, oriPlate.position, Quaternion.identity);
            plates.Add(newPlate.gameObject);
        }
        else
        {
            Transform newPlate = Instantiate(IcecreamPlate.transform, oriPlate.position, Quaternion.identity);
            plates.Add(newPlate.gameObject);
        }
        StartCoroutine(MyWait());
    }

    IEnumerator MyWait()
    {
        yield return new WaitForSeconds(time);
        CreatePlate();
    }
}
