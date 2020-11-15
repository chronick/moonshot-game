using UnityEngine;
using UnityEngine.UI;

namespace Controllers.Display {
    public class TimeDisplayController : MonoBehaviour {
        public Text currentTimeDisplay;

        // Start is called before the first frame update
        private void Start() { }

        // Update is called once per frame
        private void Update() {
            var now = GameController.Instance.Game.Now;
            this.currentTimeDisplay.text = $"{now.Hour}:{now.Minute}:{now.Second}";
        }
    }
}