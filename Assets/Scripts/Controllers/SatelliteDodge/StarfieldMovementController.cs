using UnityEngine;

namespace Controllers.SatelliteDodge {
    public class StarfieldMovementController : MonoBehaviour {
        public Rigidbody2D rigidbody2D;
        public Transform targetTransform;

        public ParticleSystem particleSystem;

        // Start is called before the first frame update
        private void Start() {
            this.particleSystem = this.GetComponent<ParticleSystem>();
        }

        // Update is called once per frame
        private void Update() {
            var velocity = this.rigidbody2D.velocity;
            var angle = Vector2.Angle(velocity, Vector2.up);
            this.transform.localRotation = Quaternion.Euler(0, 0, angle);
        
            var particleSystemMain = this.particleSystem.main;
            var minMaxGradient = particleSystemMain.startColor;
            var color = minMaxGradient.color;

            color.a = Mathf.InverseLerp(0, 20, velocity.magnitude);
            minMaxGradient.color = color;
        }

        private void OnDrawGizmos() {
            Gizmos.DrawRay(this.targetTransform.position, this.rigidbody2D.velocity);
        }

    }
}