using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class OptionsMenu : MonoBehaviour {


    public Text SoundOnText, DifficultyText;
    public GameObject MainMenu;

    public AudioSource music;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OnExitBackToMainMenuClicked();
    }

    void OnEnable()
    {
        UpdateText();
    }

    void UpdateText()
    {
        if (SettingsHandler.SoundOn)
        {
            SoundOnText.text = "Sound is ON";

        }
        else
        {
            SoundOnText.text = "Sound is OFF";
        }
        DifficultyText.text = SettingsHandler.ReturnDifficultyName(SettingsHandler.Difficulty) + " Difficulty";
    }

	public void OnSoundOptionClicked()
    {
        SettingsHandler.SoundOn = !SettingsHandler.SoundOn;
        if(SettingsHandler.SoundOn)
        {
            music.Play();
        }
        else
        {
            music.Stop();
        }
        UpdateText();
    }

    public void OnDifficultyOptionClicked()
    {
        SettingsHandler.Difficulty++;
        if (SettingsHandler.Difficulty > SettingsHandler.MaxDifficultyNumber)
            SettingsHandler.Difficulty = 0;

        UpdateText();

        
    }

    public void OnExitBackToMainMenuClicked()
    {
        gameObject.SetActive(false);
        MainMenu.SetActive(true);
    }
}
