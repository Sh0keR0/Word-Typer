using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameOverMenu : MonoBehaviour
{
    public Text ScoreText, TimeText, TimeTextFlavor;
    GameControllerScript controller;
    public void OnRetryButtonClicked()
    {
        gameObject.SetActive(false);
        GameControllerScript.GameControllerInstance.RestartGame();
    }

    void OnEnable()
    {
        controller = GameControllerScript.GameControllerInstance;
        ScoreText.text = controller.Score.ToString();
        TimeText.text = controller.TimePlaying.ToString() + " Seconds";
        
        if(GameControllerScript.GameMode == (int)GameControllerScript.GameModes.Timed)
        {
            TimeTextFlavor.text = "Total Time:";
        }

    }


}
