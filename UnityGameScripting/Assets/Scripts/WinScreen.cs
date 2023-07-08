using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    public Button mainMenuButton;
    public Button goEndlessButton;
    public Button nextLevelButton;

    private void Start()
    {
        CentralManager.RegisterUIButton(mainMenuButton, UIElement.QuitLevelButton, true);
        CentralManager.RegisterUIButton(goEndlessButton, UIElement.GoEndlessButton, true);
        CentralManager.RegisterUIButton(nextLevelButton, UIElement.NextLevelButton, true);
    }

    
}
