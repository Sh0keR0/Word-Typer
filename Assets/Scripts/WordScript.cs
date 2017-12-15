using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WordScript : MonoBehaviour {

    public float Speed;
    public string Text;
    public float FadeSpeed = 0.5f;
    TextMesh textMesh;
    public GameObject popupText;

    
    
	void Start ()
    {
        textMesh = GetComponent<TextMesh>();
        Text = GetComponent<TextMesh>().text;
        GameControllerScript.GameControllerInstance.WordsOnScreen.Add(gameObject);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        transform.position = new Vector3(transform.position.x + ((Speed / 20) * GameControllerScript.GameControllerInstance.WordSpeedMultiplier), transform.position.y, 0);

        if(transform.position.x > GameControllerScript.GameControllerInstance.MaxCoords.x)
        {
            GameControllerScript.GameControllerInstance.WordsOnScreen.Remove(gameObject);
            Destroy(gameObject);
            if (GameControllerScript.GameMode == (int)GameControllerScript.GameModes.Normal)
            {
                GameControllerScript.GameControllerInstance.PlayerLives--;
            }
        }
	
	}
    public void CompletedWord()
    {
        GameControllerScript.GameControllerInstance.WordsOnScreen.Remove(gameObject);
        StartCoroutine(FadeWordAnimation());
        popupText.SetActive(true);
        StatsHandler.WordsTyped++;
        
    }

    IEnumerator FadeWordAnimation()
    {
        while(textMesh.color != new Color(0,0,0))
        {
            textMesh.color = new Color(textMesh.color.r - FadeSpeed/10, textMesh.color.g - FadeSpeed/10, textMesh.color.b - FadeSpeed/10);
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }

}
