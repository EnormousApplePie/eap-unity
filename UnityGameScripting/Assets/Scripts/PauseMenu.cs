using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button returnButton;
    public Button quitButton;

    private void Start()
    {
        CentralManager.RegisterUIButton(returnButton, UIElement.ReturnButton, true);
        CentralManager.RegisterUIButton(quitButton, UIElement.QuitLevelButton, true);
    }
}
