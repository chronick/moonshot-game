using UnityEngine;

namespace Controllers.Display {
    public class MoveUpAndDown : MonoBehaviour {
        // Update is called once per frame
        [Range(1e-2f, 1)] public float baseSpeed = 1e-2f;
        [Range(0, 2)] public float delta = 0.4f; //delta is the difference between min y to max y.

        private float speed = 1e-2f;

        private void Start() {
            GameController.Instance.Game.RegisterTimeSpeedMultiplierChangedCallback(this.OnTimeSpeedMultiplierChanged);
        }

        private void Update() {
            var y = Mathf.PingPong(this.speed * Time.time, this.delta);
            var pos = new Vector3(this.transform.position.x, y, this.transform.position.z);
            this.transform.position = pos;
        }

        private void OnTimeSpeedMultiplierChanged(float multiplier) {
            this.speed = this.baseSpeed * multiplier / 100;
        }
    }
}