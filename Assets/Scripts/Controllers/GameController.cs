using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;

public class GameController : MonoBehaviour {
    public ProgressBarController HealthProgressBar;

    public static GameController Instance { get; protected set; }

    public Game Game;

    public float missionTime = 10000f;

    public float baseTimeSpeedMultiplier = 85f;
    
    void Awake()
    {
            if (Instance != null) {
                Debug.LogError("World Controller already exists! This shouldn't happen.");
            }

            Instance = this;
            
            this.Game = new Game(this.missionTime);
            this.SetTimeSpeedMultiplier(1f);
    }

    void Update()
    {
        this.HealthProgressBar.SetPercentValue(50);
        this.HealthProgressBar.SetPercentValue(24);
    }

    public void SetTimeSpeedMultiplier(float multiplier) {
        this.Game.SetTimeSpeedMultiplier(this.baseTimeSpeedMultiplier * multiplier);
    }
}
