using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour {

	
	void Start ()
    {
        if(SettingsHandler.SoundOn)
        {
            GetComponent<AudioSource>().Play();

        }
	
	}
	
	
}
