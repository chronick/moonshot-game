using UnityEngine;

namespace Controllers.Main_Game.Display {
    public class MoveUpAndDown : MonoBehaviour {
        // Update is called once per frame
        [Range(1e-2f, 1)] public float baseSpeed = 0.5f;
        [Range(0, 2)] public float delta = 0.4f;

        private float speed = 1e-2f;

        private void Start() {
            GameController.Instance.Game.RegisterTimeSpeedMultiplierChangedCallback(this.OnTimeSpeedMultiplierChanged);
        }

        private void Update() {
            var y = Mathf.Sin(this.speed * Time.time) * this.delta;
            var position = this.transform.position;
            var pos = new Vector3(position.x, y, position.z);
            this.transform.position = pos;
        }

        private void OnTimeSpeedMultiplierChanged(float multiplier) {
            this.speed = this.baseSpeed * multiplier / 100;
        }
    }
}