using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class StatsMenu : MonoBehaviour {

    public GameObject MainMenu;

    public Text HighestScoreText, WordsTypedText;
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OnExitToMainMenuClicked();
    }

    void OnEnable()
    {
        HighestScoreText.text = StatsHandler.HighestScore.ToString();
        WordsTypedText.text = StatsHandler.WordsTyped.ToString();
    }

    public void OnExitToMainMenuClicked()
    {
        gameObject.SetActive(false);
        MainMenu.SetActive(true);
    }
}
