using UnityEngine;

namespace Controllers.AsteroidDodge {
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour {
        // How powerful are the engines?
        public float thrust;
        
        // Forward thrust engines. Display purposes only.
        public GameObject mainEngines;
        
        // Reverse thrust engines. Display purposes only.
        public GameObject reverseEngines;

        // how long since the last collision should we be allowed to take damage?
        // prevents us from getting smoked by multiple "collsions" with the same object at once.
        public float damageTimeout = 0.5f;

        // how much damage should we take per collision (multiplied by relative velocity)?
        public float damageMultiplier = 10f;

        private Rigidbody rigidBody;
        private float timeSinceLastDamage;

        // Start is called before the first frame update
        private void Start() {
            this.rigidBody = this.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        private void Update() {
            this.timeSinceLastDamage += Time.deltaTime;
            
            this.mainEngines.gameObject.SetActive(Input.GetKey(KeyCode.UpArrow));
            this.reverseEngines.gameObject.SetActive(Input.GetKey(KeyCode.DownArrow));
        }

        private void FixedUpdate() {
            if (Input.GetKey(KeyCode.UpArrow)) {
                this.rigidBody.AddForce(this.transform.up * this.thrust, ForceMode.Impulse);
            }

            if (Input.GetKey(KeyCode.DownArrow)) {
                this.rigidBody.AddForce(this.transform.up * -this.thrust, ForceMode.Impulse);
            }

            if (Input.GetKey(KeyCode.LeftArrow)) {
                this.rigidBody.AddForce(this.transform.right * -this.thrust, ForceMode.Impulse);
            }

            if (Input.GetKey(KeyCode.RightArrow)) {
                this.rigidBody.AddForce(this.transform.right * this.thrust, ForceMode.Impulse);
            }
        }

        private void OnCollisionEnter(Collision other) {
            if (!(this.timeSinceLastDamage >= this.damageTimeout)) {
                return;
            }

            this.timeSinceLastDamage = 0f;
            Globals.GameController.Instance.Game.Ship.Hull -=
                this.damageMultiplier * other.relativeVelocity.magnitude;
        }
    }
}