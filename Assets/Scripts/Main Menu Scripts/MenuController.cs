using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{

    public GameObject OptionMenu;
    public GameObject StatsMenu;
    public GameObject GameModeSelectionMenu;

   public void OnStartButtonClicked()
    {
        gameObject.SetActive(false);
        GameModeSelectionMenu.SetActive(true);
    }

   public void OnStatsButtonClicked()
    {
        gameObject.SetActive(false);
        StatsMenu.SetActive(true);
    }

    public void OnOptionsButtonClicked()
    {
        gameObject.SetActive(false);
        OptionMenu.SetActive(true);
    }

    public void OnExitButtonClicked()
    {
        Application.Quit();
    }


}
