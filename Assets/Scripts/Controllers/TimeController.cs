using UnityEngine;
using Utils;

namespace Controllers {
    public class TimeController : MonoBehaviour {
        public float InitialTime { get; protected set; } = 0;

        private float currentTime;

        // Start is called before the first frame update
        void Start() {
            this.currentTime = this.InitialTime;
            var universe = GameController.Instance.Game;
            universe.SetGlobalTime(this.currentTime);
        }

        // Update is called once per frame
        void Update() {
            var game = GameController.Instance.Game;
            var timeMultiplier = game.TimeSpeedMultiplier;
            this.currentTime += (Time.deltaTime * timeMultiplier);
            game.SetGlobalTime(this.currentTime);
        }

        public void SetTimeSpeedMultiplier(float multiplier) {
            GameController.Instance.Game.SetTimeSpeedMultiplier(multiplier);
        }
    }
}
