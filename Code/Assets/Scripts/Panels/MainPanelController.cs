using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanelController : MonoBehaviour
{
    public int finishedPlate;
    public int destroyedPlate;

    public Text finishedPlateText;
    public Text destroyedPlateText;
    public Dropdown levelDropdown;
    public Toggle playerView;

    //level
    private LevelController levelController;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        finishedPlate = 0;
        destroyedPlate = 0;
        levelDropdown.value = 0;

        levelController = (LevelController)FindObjectOfType<LevelController>();
        playerController = (PlayerController)FindObjectOfType<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        finishedPlateText.text = finishedPlate.ToString();
        destroyedPlateText.text = destroyedPlate.ToString();

        levelController.level = levelDropdown.value;

        //camera mode
        if (playerView.isOn&&!playerController.isPlayerMode)
        {
            playerController.SetPlayerMode();
        }
        else if(!playerView.isOn&&playerController.isPlayerMode)
        {
            playerController.SetOverviewMode();
        }
    }
}
