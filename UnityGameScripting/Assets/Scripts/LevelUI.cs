using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUI : MonoBehaviour
{
    public TextMeshProUGUI scoreCounter;
    public TextMeshProUGUI waveCounter;
    public TextMeshProUGUI healthCounter;



    // Start is called before the first frame update
    void Start()
    {
        CentralManager.RegisterUIText(scoreCounter, UIElement.ScoreCounter, true);
        CentralManager.RegisterUIText(waveCounter, UIElement.WaveCounter, true);
        CentralManager.RegisterUIText(healthCounter, UIElement.HealthCounter, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
