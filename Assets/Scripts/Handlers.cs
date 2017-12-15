using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveFileHandler
{

    public static readonly string dataFileLocation = Application.dataPath + "/settings.dat";
    public static readonly string wordsTextFileLocation = Application.dataPath + "/Words.txt"; // Used only when updating the words list and converting it into binary file
    public static readonly string WordsDataFileLocation = Application.dataPath + "/data.dat";

    public static void Load()
    {
        if (File.Exists(dataFileLocation))
        {
            Debug.Log(dataFileLocation);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(dataFileLocation, FileMode.Open);
            Data dat = (Data)bf.Deserialize(file);
            file.Close();

            SettingsHandler.Difficulty = dat.Difficulty;
            SettingsHandler.SoundOn = dat.SoundOn;

            StatsHandler.HighestScore = dat.HighestScore;
            StatsHandler.WordsTyped = dat.WordsTyped;
        }
    }

    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(dataFileLocation);
        Data dat = new Data();
        dat.SoundOn = SettingsHandler.SoundOn;
        dat.Difficulty = SettingsHandler.Difficulty;
        dat.WordsTyped = StatsHandler.WordsTyped;
        dat.HighestScore = StatsHandler.HighestScore;

        bf.Serialize(file, dat);
        file.Close();

    }

    public static void ConvertWordsToBinaryFile() // Used to convert a simple text  file to binary, used ONLY when updating the words list for the game.
    {

        string[] words = File.ReadAllLines(wordsTextFileLocation);


        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(WordsDataFileLocation);
        bf.Serialize(file, words);
        file.Close();

      

    }

    public static string[] ReturnWordsList()
    {
        string[] words;


        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(WordsDataFileLocation, FileMode.Open);
        words = (string[])bf.Deserialize(file);
        file.Close();

        return words;
    }


}

[System.Serializable]
public class Data
{
    public bool SoundOn;
    public int Difficulty;

    public int HighestScore;
    public int WordsTyped;
}


public static class SettingsHandler
{
    public static bool SoundOn = true;
    public static int Difficulty = 1;
    public enum DifficultyName
    {
        Easy,
        Normal,
        Hard,
        Insane
    }
    public static int MaxDifficultyNumber = 3;

    public static string ReturnDifficultyName(int num)
    {
        if (num == 0)
            return "Easy";
        else if (num == 1)
            return "Normal";
        else if (num == 2)
            return "Hard";
        else if (num == 3)
            return "Insane";
        else
            return "ERROR";
    }


 }

public static class StatsHandler
{
    public static int WordsTyped = 0;
    public static int HighestScore = 0;



}

