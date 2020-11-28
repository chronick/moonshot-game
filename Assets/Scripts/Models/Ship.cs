using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Models {
    public class Ship {
        public float Hull;

        public Ship(float hull) {
            this.Hull = hull;
        }

        public bool IsAlive() {
            if (this.Hull <= 0) {
                return false;
            }

            return true;
        }
    }

    public class ShipEquipment {
        public string Type;

        public ShipEquipment(string type) {
            this.Type = type;
        }
    }

}