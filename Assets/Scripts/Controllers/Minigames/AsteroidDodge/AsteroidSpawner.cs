using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers.Minigames.AsteroidDodge {
    public class AsteroidSpawner : MonoBehaviour {
        public List<GameObject> asteroidPrefabs;

        public Vector3 spread;
        public int wavesCount = 2;
        public int hazardCount = 10;
        public float startTime;
        public float waveWait = 5f;
        public float spawnWait = 0.5f;
        public float hazardSpeed = 2f;
        public float hazardTumble = 0.5f;

        private Action cbOnWavesCompleted;

        // Use this for initialization
        private void Start() {
            this.StartCoroutine(this.SpawnWaves());
        }

        public void OnDrawGizmos() {
            Gizmos.DrawWireCube(this.transform.position, new Vector3(this.spread.x, this.spread.y, this.spread.z));
        }

        private IEnumerator SpawnWaves() {
            yield return new WaitForSeconds(this.startTime);
            for (var j = 0; j < this.wavesCount; j++) {
                for (var i = 0; i < this.hazardCount; i++) {
                    var spawnPosition = new Vector2(Random.Range(-this.spread.x / 2f, this.spread.x / 2f),
                        Random.Range(-this.spread.y / 2f, this.spread.y / 2f));
                    var prefab = this.asteroidPrefabs[Random.Range(0, this.asteroidPrefabs.Count)];
                    var go = Instantiate(prefab, this.transform);
                    go.transform.localPosition = spawnPosition;
                    go.transform.localScale = Vector3.one * Random.Range(1f, 1f);

                    var rb = go.GetComponent<Rigidbody>();
                    rb.velocity = Vector3.down * this.hazardSpeed;
                    rb.angularVelocity = Random.insideUnitSphere * this.hazardTumble;

                    yield return new WaitForSeconds(this.spawnWait);
                }

                yield return new WaitForSeconds(this.waveWait);
            }
            
            this.cbOnWavesCompleted?.Invoke();
        }

        public void RegisterOnWavesCompletedCallback(Action cb) {
            this.cbOnWavesCompleted += cb;
        }

        public void UnregisterOnWavesCompletedCallback(Action cb) {
            this.cbOnWavesCompleted -= cb;
        }
    }
}