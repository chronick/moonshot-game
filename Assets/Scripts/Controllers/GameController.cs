using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;

public class GameController : MonoBehaviour {
    public ProgressBarController HealthProgressBar;

    public static GameController Instance { get; protected set; }

    public Universe Universe;
    
    // Start is called before the first frame update
    void Start()
    {
            if (Instance != null) {
                Debug.LogError("World Controller already exists! This shouldn't happen.");
            }

            Instance = this;
            
            this.Universe = new Universe();
    }

    // Update is called once per frame
    void Update()
    {
        this.HealthProgressBar.SetPercentValue(50);
        this.HealthProgressBar.SetPercentValue(24);
    }
}
