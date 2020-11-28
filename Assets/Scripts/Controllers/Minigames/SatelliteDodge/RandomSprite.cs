using System.Collections.Generic;
using UnityEngine;

namespace Controllers.Minigames.SatelliteDodge {
    [RequireComponent(typeof(SpriteRenderer))]
    public class RandomSprite : MonoBehaviour {
        public List<Sprite> sprites;
    
        // Start is called before the first frame update
        void Start() {
            var sprite = this.sprites[Random.Range(0, this.sprites.Count)];
            this.GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }
}
