using UnityEngine;

namespace Controllers.Minigames.SatelliteDodge {
    public class GameController : MonoBehaviour {

        public Transform origin;
        public GameObject obstaclePrefab;

        public float minRadius = 20f;
        public int orbitCount = 8;
        public float orbitDistance = 3f;

        public int minObstaclesPerOrbit = 10;
        public int maxObstaclesPerOrbit = 50;

        public void Start() {
            // populate each orbit
            for (int i = 0; i < this.orbitCount; i++) {
                
                // Add a random number of obstacles
                for (int j = 0; j < Random.Range(this.minObstaclesPerOrbit, this.maxObstaclesPerOrbit); j++) {
                    var obstacleGO = Instantiate(this.obstaclePrefab);
                    var circleComponent = obstacleGO.GetComponent<MoveInCircle>();

                    var radius = i * this.orbitDistance + this.minRadius;
                    circleComponent.center = this.origin.position;
                    circleComponent.radius = radius;
                    circleComponent.angle = Random.Range(0, Mathf.PI * 2);
                    circleComponent.speed = 2f / Mathf.Sqrt(radius);
                }
            }
        }
    }
}