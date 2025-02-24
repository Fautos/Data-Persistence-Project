using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For SceneManager.LoadScene
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


// For EditorApplication (only used in edition mode)
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] TMP_InputField userNameField;
    [SerializeField] TMP_Text bestScores;

    private void Start()
    {
        WriteBestScores();
    }

    // Main game scene will be load when you click on the play button
    public void PlayButton()
    {
        if (userNameField.text.Length == 0)
        {
            Persistent_manager.Instance.userName = "Player";
        }
        SceneManager.LoadScene(1);
    }

    // You will exit the game when you press the exit button
    public void ExitButton()
    {
        // First we save the data
        Persistent_manager.Instance.SaveData();

        // And then we close the game
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }

    // To get the user name
    public void UserNameInput()
    {
        Persistent_manager.Instance.userName = userNameField.text;
        Debug.Log("User name: " + Persistent_manager.Instance.userName);
    }

    // To write the best scores board
    public void WriteBestScores()
    {
        bestScores.text = "Best scores:\n\n" + Persistent_manager.Instance.data.GetRecord();
        //Debug.Log(Persistent_manager.Instance.data.GetRecord()); 
    }
}
