using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ChefPanelController : MonoBehaviour
{
    public InputField SpawnRate;
    public Button SaveButton;
    public Button CancelButton;

    private ChefController chefController;
    private SelectController selectController;
    private void Start()
    {
        chefController = (ChefController)FindObjectOfType(typeof(ChefController));
        selectController = (SelectController)FindObjectOfType(typeof(SelectController));

        SaveButton.onClick.AddListener(SetSpawnRate);
        CancelButton.onClick.AddListener(ClosePanel);
    }

    void SetSpawnRate(){
        Debug.Log("Save:"+SpawnRate.text);
        chefController.time = float.Parse(SpawnRate.text);
        ClosePanel();
    }

    void ClosePanel()
    {
        selectController.DeSelectChef();
    }
}
