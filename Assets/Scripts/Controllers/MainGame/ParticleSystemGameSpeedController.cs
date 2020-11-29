using Controllers.Globals;
using UnityEngine;

namespace Controllers.MainGame {
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleSystemGameSpeedController : MonoBehaviour {
        // Start is called before the first frame update
        private void Start() {
            GameController.Instance.Game.RegisterTimeSpeedMultiplierChangedCallback(this.OnTimeSpeedMultiplierChanged);
        }

        public void OnDestroy() {
            GameController.Instance.Game.UnregisterTimeSpeedMultiplierChangedCallback(this.OnTimeSpeedMultiplierChanged);
        }

        private void OnTimeSpeedMultiplierChanged(float multiplier) {
            var main = this.GetComponent<ParticleSystem>().main;
            main.simulationSpeed = multiplier / 100;
        }
    }
}