using System.Collections.Generic;
using UnityEngine.Events;
using Utils;

namespace Models {
    public class AsteroidHullEvent : IScheduleEvent {
        private readonly float amount;
        private readonly string message = "Oh no! An asteroid hit the hull! There was some damage.";
        private readonly IGameMessageDelegate messageDelegate;
        private readonly Ship ship;

        public AsteroidHullEvent(IGameMessageDelegate messageDelegate, Ship ship, float amount) {
            this.messageDelegate = messageDelegate;
            this.ship = ship;
            this.amount = amount;
        }

        #region IScheduleEvent Members

        public void OnEventTriggered(float time) {
            this.messageDelegate.SendActionableMessage(this.message,
                new Dictionary<string, UnityAction> {{"OK", this.OnAcknowledge}});
        }

        #endregion

        private void OnAcknowledge() {
            this.ship.Hull -= this.amount;
        }
    }

    // public class GameScheduleEvent : IScheduleEvent {
    //     private Dictionary<string, UnityAction> actions;
    //
    //     private string message;
    //     private IGameMessageDelegate messageDelegate;
    //
    //     public void OnEventTriggered(float time) {
    //         this.messageDelegate.SendActionableMessage(this.message, this.actions);
    //     }
    //
    // }

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
}