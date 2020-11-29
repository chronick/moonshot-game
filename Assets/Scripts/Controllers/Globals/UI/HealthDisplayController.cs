using UI;
using UnityEngine;

namespace Controllers.Globals.UI {
    public class HealthDisplayController : MonoBehaviour {
        public ProgressBarController progressBar;

        // Start is called before the first frame update
        private void Start() {
            this.progressBar.maxValue = GameController.Instance.Game.Ship.Hull;
        }

        // Update is called once per frame
        private void Update() {
            this.progressBar.SetValue(GameController.Instance.Game.Ship.Hull);
        }
    }
}