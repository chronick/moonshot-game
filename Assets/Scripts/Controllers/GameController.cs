using Models;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers {
    public class GameController : MonoBehaviour {
        public ProgressBarController HealthProgressBar;

        public float missionTime = 10000f;

        public float baseTimeSpeedMultiplier = 85f;

        public ModalDialogController modalDialog;

        public Game Game;

        private bool isGameEnd = false;

        public static GameController Instance { get; protected set; }

        private void Awake() {
            if (Instance != null) {
                Debug.LogError("World Controller already exists! This shouldn't happen.");
            }

            Instance = this;

            this.Game = new Game(this.missionTime);
            this.Game.RegisterOnGameEndCallback(this.OnGameEnd);
            this.SetTimeSpeedMultiplier(1f);
        }

        private void Start() {
            // this.HealthProgressBar.SetValue(200);
        }

        private void Update() {
            if (this.isGameEnd) return;
            
            var currentTime = this.Game.GlobalTime + Time.deltaTime * this.Game.TimeSpeedMultiplier;
            this.Game.Tick(currentTime);
        }

        private void OnGameEnd(GameEndData data) {
            this.isGameEnd = true;
            if (data.IsWin) {
                Debug.Log("You Won the game!");
                this.modalDialog.Dialog("You Won the game!", () => SceneManager.LoadScene("Scenes/MainMenu"));
            }

            else {
                Debug.Log("You Lost the game!");
                this.modalDialog.Dialog($"You Lost the game! Reason: {data.Reason}",
                    () => SceneManager.LoadScene("Scenes/MainMenu"));
            }
        }

        public void OnModalClicked() {
            Debug.Log("Modal Clicked!");
        }

        private void DoSomething(float t) {
            Debug.Log("Something Happened!");
            this.modalDialog.Dialog("Something Happened!", this.OnModalClicked);
        }

        public void SetTimeSpeedMultiplier(float multiplier) {
            this.Game.SetTimeSpeedMultiplier(this.baseTimeSpeedMultiplier * multiplier);
        }
    }
}