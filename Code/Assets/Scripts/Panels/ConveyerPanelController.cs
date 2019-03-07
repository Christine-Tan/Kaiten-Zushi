using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ConveyerPanelController : MonoBehaviour
{
    public InputField Speed;
    public Button SaveButton;
    public Button CancelButton;

    private ConveyerController conveyerController;
    private SelectController selectController;
    // Start is called before the first frame update
    void Start()
    {
        selectController = (SelectController)FindObjectOfType(typeof(SelectController));
        conveyerController = (ConveyerController)FindObjectOfType(typeof(ConveyerController));

        Speed.text = conveyerController.oriSpeed.ToString();
        SaveButton.onClick.AddListener(SetConveyerSpeed);
        CancelButton.onClick.AddListener(ClosePanel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetConveyerSpeed()
    {
        conveyerController.oriSpeed = float.Parse(Speed.text);
        conveyerController.speed = float.Parse(Speed.text);

        ClosePanel();
    }

    void ClosePanel()
    {
        selectController.DeSelectConveyer();
    }
}
