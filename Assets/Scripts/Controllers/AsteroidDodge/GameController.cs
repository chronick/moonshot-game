using UnityEngine;

namespace Controllers.AsteroidDodge {
    public class GameController : MonoBehaviour {
        public AsteroidSpawner asteroidSpawner;

        private void Start() {
            this.asteroidSpawner.RegisterOnWavesCompletedCallback(this.OnWavesCompleted);
        }

        private void OnWavesCompleted() {
            Globals.GameController.Instance.SendActionableMessage("You've cleared the asteroid field!",
                Globals.GameController.Instance.ResumeMainGame);
        }
    }
}