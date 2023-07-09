using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUI : MonoBehaviour
{
    public TextMeshProUGUI scoreCounter;
    public TextMeshProUGUI waveCounter;



    // Start is called before the first frame update
    void Start()
    {
        CentralManager.RegisterUIText(scoreCounter, UIElement.ScoreCounter, true);
        CentralManager.RegisterUIText(waveCounter, UIElement.WaveCounter, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
