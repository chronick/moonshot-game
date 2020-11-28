using System.Collections.Generic;
using Models;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Utils;

namespace Controllers.Main_Game {
    public class GameController : MonoBehaviour, IGameMessageDelegate {
        public ProgressBarController HealthProgressBar;

        public float missionTime = 10000f;

        public float baseTimeSpeedMultiplier = 85f;

        public ModalDialogController modalDialog;

        public Game Game;

        private bool isGameEnd;

        public static GameController Instance { get; protected set; }

        private void Awake() {
            if (Instance != null) {
                Debug.LogError("World Controller already exists! This shouldn't happen.");
            }

            Instance = this;

            this.Game = new Game(this.missionTime);
            this.Game.RegisterOnGameEndCallback(this.OnGameEnd);
            this.SetTimeSpeedMultiplier(1f);

            this.modalDialog.RegisterOnOpenCallback(this.OnModalDialogOpen);
            this.modalDialog.RegisterOnCloseCallback(this.OnModalDialogClose);
        }


        private void Start() {
            this.HealthProgressBar.maxValue = this.Game.Ship.Hull;

            for (var i = 0; i < 3; i++) {
                var someTime = Random.Range(100, this.missionTime);
                var damage = Random.Range(20, 250);
                this.Game.At(someTime, new AsteroidHitShipEvent(this, this.Game.Ship, damage));
            }
        }

        private void Update() {
            if (this.Game.IsEnded || this.Game.IsPaused) {
                return;
            }

            this.HealthProgressBar.SetValue(this.Game.Ship.Hull);

            var currentTime = this.Game.GlobalTime + Time.deltaTime * this.Game.TimeSpeedMultiplier;
            this.Game.Tick(currentTime);
        }

        private void OnDestroy() {
            this.modalDialog.UnregisterOnOpenCallback(this.OnModalDialogOpen);
            this.modalDialog.UnregisterOnCloseCallback(this.OnModalDialogClose);
        }

        public void SendActionableMessage(string message, Dictionary<string, UnityAction> actions) {
            this.modalDialog.Dialog(message, actions);
        }

        public void SendActionableMessage(string message, UnityAction action) {
            this.modalDialog.Dialog(message, action);
        }

        public void SendActionableMessage(string message) {
            this.modalDialog.Dialog(message);
        }
        
        private void OnModalDialogOpen() {
            this.Game.Pause();
        }

        private void OnModalDialogClose() {
            this.Game.Resume();
        }

        private void OnGameEnd(GameEndData data) {
            if (data.IsWin) {
                this.modalDialog.Dialog("You Won the game!", () => SceneManager.LoadScene("Scenes/MainMenu"));
            }

            else {
                this.modalDialog.Dialog($"You Lost the game!\nReason: {data.Reason}",
                    () => SceneManager.LoadScene("Scenes/MainMenu"));
            }
        }

        public void SetTimeSpeedMultiplier(float multiplier) {
            this.Game.SetTimeSpeedMultiplier(this.baseTimeSpeedMultiplier * multiplier);
        }
    }
}