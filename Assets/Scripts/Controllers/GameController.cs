using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public ProgressBarController HealthProgressBar;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.HealthProgressBar.SetPercentValue(50);
        this.HealthProgressBar.SetPercentValue(24);
    }
}
