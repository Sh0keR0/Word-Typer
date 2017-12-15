using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour {

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OnContinueButtonClicked();
    }

    public void OnContinueButtonClicked()
    {
        GameControllerScript.GameControllerInstance.Unpause();
    }

    public void OnExitToMainMenuButtonClicked()
    {
        SceneManager.LoadSceneAsync("MainMenu");
        
    }
}
