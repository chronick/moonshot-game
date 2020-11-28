using UnityEngine;

namespace Controllers.Minigames.SatelliteDodge {
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour {
        public float thrust;
        public GameObject mainEngines;
        public GameObject reverseEngines;
        private Rigidbody2D rigidBody2D;

        // Start is called before the first frame update
        private void Start() {
            this.rigidBody2D = this.GetComponent<Rigidbody2D>();
            // this.rigidBody2D.freezeRotation = true;
        }

        // Update is called once per frame
        private void Update() {
            this.mainEngines.gameObject.SetActive(Input.GetKey(KeyCode.UpArrow));
            this.reverseEngines.gameObject.SetActive(Input.GetKey(KeyCode.DownArrow));

        }

        private void FixedUpdate() {
            if (Input.GetKey(KeyCode.UpArrow)) {
                this.rigidBody2D.AddForce(this.transform.up * this.thrust, ForceMode2D.Impulse);
            }

            if (Input.GetKey(KeyCode.DownArrow)) {
                this.rigidBody2D.AddForce(this.transform.up * -this.thrust, ForceMode2D.Impulse);
            }
            
            if (Input.GetKey(KeyCode.LeftArrow)) {
                // this.transform.Rotate(Vector3.forward * 1);
                this.rigidBody2D.AddTorque(1);
            }

            if (Input.GetKey(KeyCode.RightArrow)) {
                // this.transform.Rotate(Vector3.forward * -1);
                this.rigidBody2D.AddTorque(-1);
            }
        }
    }
}