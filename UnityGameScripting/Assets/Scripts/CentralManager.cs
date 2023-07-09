using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CentralManager : MonoBehaviour
{
    private static CentralManager existingManager = null;
    private static LevelManager currentLevel = null;
    // Start is called before the first frame update
    void Start()
    {
        if (existingManager != null)    //check if somehow multiple central managers get created in the game (which shouldn't happen)
        {
            Debug.LogWarning("Multiple CentralManagers get created in the game, this should not happen. Destroying the new manager...");
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);
        existingManager = this;

    }

    void Update()
    {
        
    }

    void OnLevelManagerDestroyed(LevelManager destroyedManager)
    {
        if(destroyedManager != currentLevel)
        {
            Debug.LogWarning("Multiple level managers are present at once, this should not happen.");   //maybe change this to make it clearer later
            return;
        }
        currentLevel = null;
    }


    public static void RegisterUIButton(Button addingButton, UIElement typeOfButton, bool isForLevel)   //might remove this later as buttons should generally handle themselves
    {
        if(addingButton == null)
        {
            Debug.LogError("UI button of type \"" + typeOfButton + "\" that is null attempted to load, skipping...");
            return;
        }
        if (isForLevel)
        {
            if (currentLevel == null) Debug.LogError("Level UI button of type \"" + typeOfButton + "\" is being loaded despite no level present");
            else
            {
                currentLevel.RegisterUIButton(addingButton, typeOfButton);
            }
        }
    }
    public static void RegisterUIText(TextMesh addingText, UIElement typeOfText, bool isForLevel)
    {
        if (addingText == null)
        {
            Debug.LogError("UI text of type \"" + typeOfText + "\" that is null attempted to load, skipping...");
            return;
        }
        if (isForLevel)
        {
            if (currentLevel == null) Debug.LogError("Level UI text of type \"" + typeOfText + "\" is being loaded despite no level present");
            else
            {
                currentLevel.RegisterUIText(addingText, typeOfText);
            }
        }
    }

    
}
