﻿using System;
using Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class TimeDisplayController : MonoBehaviour {

        public Text currentTimeDisplay;
    
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update() {
            var now = GameController.Instance.Game.Now;
            this.currentTimeDisplay.text = $"{now.Hour}:{now.Minute}:{now.Second}";
        }
    }
}
