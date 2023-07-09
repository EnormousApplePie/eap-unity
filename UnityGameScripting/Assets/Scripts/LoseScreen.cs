using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
    public Button mainMenuButton;
    public Button restartLevelButton;

    private void Start()
    {
        CentralManager.RegisterUIButton(mainMenuButton, UIElement.QuitLevelButton, true);
        CentralManager.RegisterUIButton(restartLevelButton, UIElement.RestartLevelButton, true);
    }


    public static void LoadLevel(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad, LoadSceneMode.Single);
    }
}
