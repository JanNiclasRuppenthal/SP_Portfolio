using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject controlsText;
    public GameObject[] gameObjects;

    private bool showControlsText = false;

    public void StartGame()
    {
        // activate all GameObjects to play the game
        this.gameObject.SetActive(false);

        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(true);
        }
    }

    public void ShowControls()
    {
        // show the text with the copntrols
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
        Application.Quit(); // This only works in the build
    }
}
