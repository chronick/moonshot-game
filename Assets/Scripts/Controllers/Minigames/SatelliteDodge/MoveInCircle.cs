using UnityEngine;

namespace Controllers.Minigames.SatelliteDodge {
    public class MoveInCircle : MonoBehaviour 
    {
        public Vector3 center = Vector3.zero;
        public float radius = 0.1f;
        public float speed = 5f;
        public float angle;
 
        private void FixedUpdate()
        {
            this.angle += this.speed * Time.deltaTime;
            var offset = new Vector3(Mathf.Sin(this.angle), Mathf.Cos(this.angle), this.center.z) * this.radius;
            transform.position = this.center + offset;
        }
    }
}