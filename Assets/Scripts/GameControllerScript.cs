using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GameControllerScript : MonoBehaviour {


    public static GameControllerScript GameControllerInstance;



    public Vector2 MinCoords, MaxCoords;
    string[] Words;
    public GameObject TextPrefab;
    public List<GameObject> WordsOnScreen;

    public GameObject InputTextPrefab;
    TMP_InputField InputText;

    int _score;
    public Text ScoreText;
    public GameObject PauseMenu;
    public int Score { get { return _score; } set { _score = value; UpdateScoreText();  } }

    public static int GameMode;
    public enum GameModes
    {
        Casual, // No score, can not fail
        Normal, // Game over after enough words move out of the screen
        Timed // Game Over after specific amount of time
    }
    private int playerLives;
    public int PlayerLives { get { return playerLives; } set { playerLives = value; OnPlayerLifeChanged(); } }
    public Text Lives;
    public Text LivesText;
    float timePlaying;
    public float TimePlaying { get { return Mathf.Round(timePlaying); } }
    [HideInInspector]public bool IsGameOver;
    public GameObject GameOverMenuRef;

    public float TimeLimit;

    public bool GamePaused { get { return Time.timeScale == 1 ? false : true; } }

    #region Difficulty Related Values
    public float WordAppearDelay = 1f; // delay between each word
    public float WordSpeedMultiplier = 1;
    #endregion

    void Start ()
    {
        
        GameControllerInstance = this;
        InputText = InputTextPrefab.GetComponent<TMP_InputField>();

        Words = SaveFileHandler.ReturnWordsList();

        StartGame();
        
	}


	public void RestartGame()
    {
        foreach(GameObject word in WordsOnScreen)
        {
            Destroy(word);
        }
        StartGame();
    }


    void SetDifficultyValues()
    {
        if (GameMode == (int)GameModes.Casual || GameMode == (int)GameModes.Normal)
        {
            if (SettingsHandler.Difficulty == (int)SettingsHandler.DifficultyName.Easy)
            {
                WordAppearDelay = WordAppearDelay * 2; // words appear twice as slow
                WordSpeedMultiplier = 0.5f; // words moves at half speed
            }
            else if (SettingsHandler.Difficulty == (int)SettingsHandler.DifficultyName.Normal)
            {
                // if it's normal diff, use regular values
            }
            else if (SettingsHandler.Difficulty == (int)SettingsHandler.DifficultyName.Hard)
            {
                WordAppearDelay = WordAppearDelay * 0.75f;
                WordSpeedMultiplier = 1.25f;
            }
            else if (SettingsHandler.Difficulty == (int)SettingsHandler.DifficultyName.Insane)
            {
                WordAppearDelay = WordAppearDelay * 0.5f; // words appear twice as slow
                WordSpeedMultiplier = 1.5f;
            }
        }
        else if(GameMode ==(int)GameModes.Timed)
        {
            WordAppearDelay = WordAppearDelay * 0.65f;
            WordSpeedMultiplier = 1.8f;
        }
    }



    public void StartGame()
    {
        timePlaying = 0f;
        Time.timeScale = 1;
        SetDifficultyValues();
        WordsOnScreen = new List<GameObject>();
        if (GameMode == (int)GameModes.Casual)
        {
            Score = -1;
        }
        else
        {
            Score = 0;
        }
        PlayerLives = 10;
        IsGameOver = false;
        StartCoroutine(DisplayWords());
    }

    void OnPlayerLifeChanged()
    {
        if (GameMode == (int)GameModes.Casual)
        {
            LivesText.text = "CASUAL";
        }
        else if(GameMode == (int)GameModes.Normal)
        {
            LivesText.text = PlayerLives.ToString();
        }
        if (PlayerLives <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        IsGameOver = true;
        Time.timeScale = 0;
        GameOverMenuRef.SetActive(true);
        
    }



    IEnumerator DisplayWords()
    {
        yield return new WaitForSeconds(1);
        while(true)
        {
            string WordToDisplay = Words[Random.Range(0, Words.Length)];
            GameObject word = Instantiate(TextPrefab) as GameObject;
            word.GetComponent<TextMesh>().text = WordToDisplay;
            word.GetComponent<Transform>().position = new Vector3(MinCoords.x, Random.Range(MinCoords.y, MaxCoords.y), 0);
            yield return new WaitForSeconds(WordAppearDelay);

        }
    }

    public void CheckForValidWords()
    {
        foreach(GameObject s in WordsOnScreen)
        {
            WordScript script = s.GetComponent<WordScript>();
            if (script.Text.ToLower() == InputText.text.ToLower())
            {

                script.CompletedWord();
                if(GameMode != (int)GameModes.Casual)
                      Score++;

                if (Score > StatsHandler.HighestScore)
                    StatsHandler.HighestScore = Score;
                InputText.text = "";
                break;
            }

            
        }
    }


    void UpdateScoreText()
    {
        if (Score == -1)
        {
            ScoreText.text = "CASUAL";
        }
        else
        {
            ScoreText.text = Score.ToString();
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !IsGameOver)
        {
            Pause();
        }
        else
        {
            if(IsGameOver == false && InputText.isFocused == false && GamePaused == false)
            {
                if (Input.GetKeyDown(KeyCode.Delete))
                {
                   
                }
                else
                {
                    InputText.ActivateInputField();
                    InputText.text += Input.inputString;
                }
            }
        }
        timePlaying += Time.deltaTime;

        if (GameMode == (int)GameModes.Timed)
        {
            Lives.text = "Time:";
            LivesText.text = Mathf.RoundToInt((TimeLimit - timePlaying)).ToString();
            if (timePlaying > TimeLimit)
                GameOver();
        }



    }


   

    #region Pausing the game
   public void Pause()
    {
        Time.timeScale = 0;
        InputText.interactable = false;
        PauseMenu.SetActive(true);
    }

   public void Unpause()
    {
        Time.timeScale = 1;
        InputText.interactable = true;
        PauseMenu.SetActive(false);
    }
    #endregion


}
