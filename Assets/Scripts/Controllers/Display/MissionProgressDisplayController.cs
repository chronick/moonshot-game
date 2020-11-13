using UI;
using UnityEngine;

namespace Controllers.Display {
    public class MissionProgressDisplayController : MonoBehaviour {
        public ProgressBarController progressBar;
    
        // Start is called before the first frame update
        void Start() {
            this.progressBar.MaxValue = GameController.Instance.Game.MissionTime;
        }

        // Update is called once per frame
        void Update()
        {
            this.progressBar.SetValue(GameController.Instance.Game.GlobalTime);
        }
    }
}
