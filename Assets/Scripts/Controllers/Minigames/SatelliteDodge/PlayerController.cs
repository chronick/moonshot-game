using UnityEngine;

namespace Controllers.Minigames.SatelliteDodge {
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour {
        // Forward thrust engines. Display purposes only.
        public GameObject mainEngines;
        
        // Reverse thrust engines. Display purposes only.
        public GameObject reverseEngines;
        
        // How powerful are the engines?
        public float thrust;
        
        // how long since the last collision should we be allowed to take damage?
        // prevents us from getting smoked by multiple "collsions" with the same object at once.
        public float damageTimeout = 0.5f;
        
        // how much damage should we take per collision (multiplied by relative velocity)?
        public float damageMultiplier = 10f;
        
        private Rigidbody2D rigidBody2D;
        private float timeSinceLastDamage;

        // Start is called before the first frame update
        private void Start() {
            this.rigidBody2D = this.GetComponent<Rigidbody2D>();
            // this.rigidBody2D.freezeRotation = true;
        }

        // Update is called once per frame
        private void Update() {
            this.mainEngines.gameObject.SetActive(Input.GetKey(KeyCode.UpArrow));
            this.reverseEngines.gameObject.SetActive(Input.GetKey(KeyCode.DownArrow));
            this.timeSinceLastDamage += Time.deltaTime;
        }

        private void FixedUpdate() {
            if (Input.GetKey(KeyCode.UpArrow)) {
                this.rigidBody2D.AddForce(this.transform.up * this.thrust, ForceMode2D.Impulse);
            }

            if (Input.GetKey(KeyCode.DownArrow)) {
                this.rigidBody2D.AddForce(this.transform.up * -this.thrust, ForceMode2D.Impulse);
            }

            if (Input.GetKey(KeyCode.LeftArrow)) {
                this.rigidBody2D.AddTorque(1);
            }

            if (Input.GetKey(KeyCode.RightArrow)) {
                this.rigidBody2D.AddTorque(-1);
            }
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (!(this.timeSinceLastDamage >= this.damageTimeout)) {
                return;
            }

            this.timeSinceLastDamage = 0f;
            Main_Game.GameController.Instance.Game.Ship.Hull -=  this.damageMultiplier * other.relativeVelocity.magnitude;
        }
    }
}