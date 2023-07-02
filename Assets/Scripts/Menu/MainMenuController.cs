using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject controlsText;
    public GameObject[] gameObjects;

    private bool showControlsText = false;

    public void StartGame()
    {
        this.gameObject.SetActive(false);

        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(true);
        }
    }

    public void ShowControls()
    {
        showControlsText = !showControlsText;
        controlsText.SetActive(showControlsText);
    }

    public void StartMultiplayer()
    {
        Debug.Log("Start Multiplayer");
        Debug.Log("This feature will be implemented in the near future. Stay tuned!");
    }

    public void Quit()
    {
        Debug.Log("Quit the Game!");
        Application.Quit();
    }
}
