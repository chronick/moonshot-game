using UnityEngine;

namespace Controllers.SatelliteDodge {
    public class MoonLandingGameController : MonoBehaviour {
        public Transform origin;
        public GameObject obstaclePrefab;
        
        public GoalController goalController;

        public float minRadius = 70f;
        public int orbitCount = 5;
        public float orbitDistance = 3f;

        public int minObstaclesPerOrbit = 10;
        public int maxObstaclesPerOrbit = 30;

        public void Start() {
            // Register Game End callbacks
            this.goalController.RegisterOnGoalEnteredCallback(this.OnGoalEntered);

            // populate each orbit
            for (var i = 0; i < this.orbitCount; i++) {
                // Add a random number of obstacles
                for (var j = 0; j < Random.Range(this.minObstaclesPerOrbit, this.maxObstaclesPerOrbit); j++) {
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

        private void OnGoalEntered() {
            Globals.GameController.Instance.OnMoonLanded();
        }
    }
}