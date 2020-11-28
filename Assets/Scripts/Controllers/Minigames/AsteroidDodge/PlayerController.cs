using UnityEngine;

namespace Controllers.Minigames.AsteroidDodge {
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour {
        public float thrust;
        public GameObject mainEngines;
        public GameObject reverseEngines;
        private Rigidbody rigidBody;

        // Start is called before the first frame update
        private void Start() {
            this.rigidBody = this.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        private void Update() {
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
    }
}