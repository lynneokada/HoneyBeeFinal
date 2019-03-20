using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    [SerializeField]
    GameObject ControlsPanel;

    public void startPressed() 
    {
        SceneManager.LoadScene("MainScene");
    }

    public void openControlsPressed()
    {
        ControlsPanel.SetActive(true);
    }

    public void closeControlsPressed()
    {
        ControlsPanel.SetActive(false);
    }
}
