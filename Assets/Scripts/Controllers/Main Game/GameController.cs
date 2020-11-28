using System.Collections.Generic;
using Models;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Utils;

namespace Controllers.Main_Game {
    public class GameController : MonoBehaviour, IGameMessageDelegate {
        public float missionTime = 10000f;

        public float baseTimeSpeedMultiplier = 85f;

        public ModalDialogController modalDialog;

        public GameObject mainUIContainer;
        public GameObject healthProgressContainer;
        public GameObject timelineDisplayContainer;

        public ProgressBarController healthProgressBar;

        public GameObject loadingProgressContainer;
        public ProgressBarController loadingProgress;

        public Game Game;

        private bool isGameEnd;

        private AsyncOperation sceneLoading;

        public static GameController Instance { get; protected set; }

        private void Awake() {
            if (Instance != null) {
                Debug.LogError("GameController instance already exists! This shouldn't happen.");
            }

            Instance = this;

            DontDestroyOnLoad(this.gameObject);

            this.loadingProgressContainer.SetActive(false);
            this.loadingProgress.maxValue = 1f;

            this.mainUIContainer.SetActive(false);

            this.modalDialog.RegisterOnOpenCallback(this.OnModalDialogOpen);
            this.modalDialog.RegisterOnCloseCallback(this.OnModalDialogClose);
        }

        private void Update() {
            // handle loading screen
            if (this.sceneLoading != null && !this.sceneLoading.isDone) {
                this.loadingProgressContainer.SetActive(true);
                this.loadingProgress.SetValue(this.sceneLoading.progress);
                return;
            }

            // Don't update the game if it is not active
            if (this.Game == null || this.Game.IsEnded || this.Game.IsPaused) {
                return;
            }

            // Update UI
            this.healthProgressBar.SetValue(this.Game.Ship.Hull);

            // Update Game
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

        public void StartGame() {
            this.LoadScene("Scenes/MainGame");
            this.Game = new Game(this.missionTime);
            this.Game.RegisterOnGameEndCallback(this.OnGameEnd);
            this.SetTimeSpeedMultiplier(1f);

            // enable main UI
            this.mainUIContainer.SetActive(true);

            // initialize health bar
            this.healthProgressBar.maxValue = this.Game.Ship.Hull;

            for (var i = 0; i < 3; i++) {
                var someTime = Random.Range(100, this.missionTime);
                var damage = Random.Range(20, 250);
                this.Game.At(someTime, new AsteroidHitShipEvent(this, this.Game.Ship, damage));
            }

            for (var i = 0; i < 2; i++) {
                var someTime = Random.Range(100, this.missionTime);
                var damage = Random.Range(20, 250);
                this.Game.At(someTime, this.BeginAsteroidDodgeMinigame);
            }

            this.Game.At(0, this.BeginSatelliteDodgeMinigame);
        }
        

        public void ResumeMainGame() {
            this.LoadScene("Scenes/MainGame");
            this.SetTimeSpeedMultiplier(1f, true);
            this.mainUIContainer.SetActive(true);
            this.timelineDisplayContainer.SetActive(true);
            this.healthProgressContainer.SetActive(true);
        }

        private void BeginAsteroidDodgeMinigame(float time) {
            this.SetTimeSpeedMultiplier(0, true);
            this.timelineDisplayContainer.SetActive(false);
            this.SendActionableMessage("Try to avoid the asteroids!",
                () => { this.LoadScene("Scenes/AsteroidDodge"); });
        }

        private void BeginSatelliteDodgeMinigame(float time) {
            this.SetTimeSpeedMultiplier(0, true);
            this.timelineDisplayContainer.SetActive(false);
            this.SendActionableMessage("Try to get off the planet!",
                () => { this.LoadScene("Scenes/SatelliteDodge"); });
        }

        private void LoadScene(string sceneName) {
            // this.sceneLoading = SceneManager.LoadSceneAsync(sceneName);
            // this.sceneLoading.allowSceneActivation = true;
            SceneManager.LoadScene(sceneName);
        }

        private void OnModalDialogOpen() {
            this.Game.Pause();
        }

        private void OnModalDialogClose() {
            this.Game.Resume();
        }

        private void OnGameEnd(GameEndData data) {
            if (data.IsWin) {
                this.modalDialog.Dialog("You Won the game!", this.RestartGame);
            }

            else {
                this.modalDialog.Dialog($"You Lost the game!\nReason: {data.Reason}", this.RestartGame);
            }
        }

        private void RestartGame() {
            Destroy(Instance.gameObject);
            SceneManager.LoadScene("Scenes/MainMenu");
        }

        public void SetTimeSpeedMultiplier(float multiplier, bool overridePrevious = false) {
            this.Game.SetTimeSpeedMultiplier(this.baseTimeSpeedMultiplier * multiplier, overridePrevious);
        }
    }
}