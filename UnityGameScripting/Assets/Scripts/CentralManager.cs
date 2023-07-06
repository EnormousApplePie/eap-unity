using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralManager : MonoBehaviour
{
    private static CentralManager existingManager = null;
    private static LevelManager currentLevel = null;
    // Start is called before the first frame update
    void Start()
    {
        if (existingManager == null)    //check if somehow multiple central managers get created in the game (which shouldn't happen)
        {
            DontDestroyOnLoad(this);
            existingManager = this;
        }
        else
        {
            Debug.LogWarning("Multiple CentralManagers get created in the game, this should not happen. Destroying the new manager...");
            Destroy(this);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnLevelManagerDestroyed(LevelManager destroyedManager)
    {
        if(destroyedManager != currentLevel)
        {
            Debug.LogWarning("Multiple level managers are present at once, this should not happen.");
            return;
        }
        currentLevel = null;
    }
}
