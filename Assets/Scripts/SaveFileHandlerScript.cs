using UnityEngine;
using System.Collections;

public class SaveFileHandlerScript : MonoBehaviour {

    public static SaveFileHandlerScript handler;
    void Awake()
    {
        if (handler == null)
        {
            handler = this;
            SaveFileHandler.Load();
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

    }

    void OnApplicationQuit()
    {
        SaveFileHandler.Save();
    }

}
