using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameModeSelection : MonoBehaviour {

    public GameObject MainMenu;
	
    public void OnCasualModeClicked()
    {
        GameControllerScript.GameMode = (int)GameControllerScript.GameModes.Casual;
        LoadGame();
    }
    public void OnNormalModeClicked()
    {
        GameControllerScript.GameMode = (int)GameControllerScript.GameModes.Normal;
        LoadGame();
    }
    public void OnTimeModeClicked()
    {
        GameControllerScript.GameMode = (int)GameControllerScript.GameModes.Timed;
        LoadGame();
    }

    void LoadGame()
    {
        SceneManager.LoadSceneAsync("Scenes/Game");
    }
    public void OnReturnToMainMenuClicked()
    {
        gameObject.SetActive(false);
        MainMenu.SetActive(true);
    }
}
