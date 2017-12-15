using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MouseHover : MonoBehaviour {

    public GameObject TextPanel;
    public void DisplayText()
    {
        TextPanel.SetActive(true);

    }
    public void HideText()
    {
        TextPanel.SetActive(false);
    }
}
