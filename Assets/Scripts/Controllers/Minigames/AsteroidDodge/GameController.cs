using UnityEngine;

namespace Controllers.Minigames.AsteroidDodge {
    public class GameController : MonoBehaviour {
        public AsteroidSpawner asteroidSpawner;

        private void Start() {
            this.asteroidSpawner.RegisterOnWavesCompletedCallback(this.OnWavesCompleted);
        }

        private void OnWavesCompleted() {
            Main_Game.GameController.Instance.SendActionableMessage("You've cleared the asteroid field!",
                Main_Game.GameController.Instance.ResumeMainGame);
        }
    }
}