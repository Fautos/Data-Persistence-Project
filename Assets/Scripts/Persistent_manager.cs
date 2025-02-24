using System.Collections;
using System.Collections.Generic;
// Comment if WebGL
//using System.Security.Policy;
using UnityEngine;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine.SocialPlatforms.Impl;
using System.Linq;
using System;

public class Persistent_manager : MonoBehaviour
{
    public static Persistent_manager Instance;
    public string userName;
    public ScoreData data = new ScoreData();

    // Método para relizar la continuidad entre escenas
    private void Awake()
    {
        // Patrón "Singleton"
        // Con esto evitamos que se generen infinitos MainManager volver al menú de título
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Load the saved data
        LoadScoreBoard();

    }

    // To load the saved data
    public void LoadScoreBoard()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<ScoreData>(json);

        }
        else{
            Debug.Log("No saved data found.");
            CreateInitData();
        }
    }

    // To save the data
    public void SaveData()
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    // Clases para guardar datos entre sesiones
    // Cada record se guarda en una clase tipo "ScoreEntry" la cual contiene un string con el nombre y un int con la puntuación
    [System.Serializable] 
    public class ScoreEntry
    {
        public string userName;
        public int score;

        public ScoreEntry(string userName, int score)
        {
            this.userName = userName;
            this.score = score;
        }
    }
    
    [System.Serializable]
    public class ScoreData
    {
        public List<ScoreEntry> topScores = new List<ScoreEntry>();

        public void AddScore(string userName, int score)
        {
            topScores.Add(new ScoreEntry(userName, score));
            topScores.Sort((x, y) => y.score.CompareTo(x.score));

            if (topScores.Count > 5)
            {
                topScores.RemoveAt(topScores.Count - 1);
            }
        }

        public (string, int) GetBest()
        {
            return (topScores.First().userName, topScores.First().score);
        }

        public string GetRecord()
        {
            string Records = "";

            foreach (ScoreEntry Score in topScores)
            {
                Records += Score.userName + ": " + Score.score + "\n";
            }

            return Records;
        }
    }

    private void CreateInitData()
    {   
        for (int i = 0; i <5; i++)
        {
            data.AddScore("_", 0);
        }
    }

}
