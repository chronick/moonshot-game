using UnityEngine;

namespace Controllers.Main_Game.Display {
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleSystemGameSpeedController : MonoBehaviour {
        // Start is called before the first frame update
        private void Start() {
            GameController.Instance.Game.RegisterTimeSpeedMultiplierChangedCallback(this.OnTimeSpeedMultiplierChanged);
        }

        private void OnTimeSpeedMultiplierChanged(float multiplier) {
            var main = this.GetComponent<ParticleSystem>().main;
            main.simulationSpeed = multiplier / 100;
        }
    }
}