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

    private void Start()
    {

    }

    // Main game scene will be load when you click on the play button
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    // You will exit the game when you press the exit button
    public void ExitButton()
    {
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
}
