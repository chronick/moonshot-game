using System;
using System.Collections.Generic;
using UnityEngine.Events;
using Utils;

namespace Models {
    // public interface IActionableMessageProvider {
    //     string GetMessage();
    // }
    //
    // public interface IActionableActionsProvider {
    //     Dictionary<string, UnityAction> GetActions();
    // }
    //
    // public interface IGameActionsProvider : IActionableActionsProvider, IActionableMessageProvider { }
    //
    // public class ActionableMessageEvent : IScheduleEvent {
    //     private readonly IActionableActionsProvider actionProvider;
    //     private readonly IGameMessageDelegate messageDelegate;
    //     private readonly IActionableMessageProvider messageProvider;
    //
    //     public ActionableMessageEvent(
    //         IGameMessageDelegate messageDelegate,
    //         IActionableActionsProvider actionProvider,
    //         IActionableMessageProvider messageProvider
    //     ) {
    //         this.messageDelegate = messageDelegate;
    //         this.actionProvider = actionProvider;
    //         this.messageProvider = messageProvider;
    //     }
    //
    //     public ActionableMessageEvent(IGameMessageDelegate messageDelegate, IGameActionsProvider gameActionsProvider) :
    //         this(messageDelegate, gameActionsProvider, gameActionsProvider) { }
    //
    //     public void OnEventTriggered(float time) {
    //         var actions = this.actionProvider.GetActions();
    //         if (actions == null) {
    //             throw new Exception("No Actions on GameMessageEvent");
    //         }
    //
    //         var message = this.messageProvider.GetMessage();
    //         if (message == null) {
    //             throw new Exception("No Message on GameMessageEvent");
    //         }
    //
    //         this.messageDelegate.SendActionableMessage(message, actions);
    //     }
    // }

    public class AsteroidHitShipEvent : IScheduleEvent {
        private readonly float amount;
        private readonly string message = "Oh no! An asteroid hit the hull! There was some damage.";
        private readonly IGameMessageDelegate messageDelegate;
        private readonly Ship ship;

        public AsteroidHitShipEvent(IGameMessageDelegate messageDelegate, Ship ship, float amount) {
            this.messageDelegate = messageDelegate;
            this.ship = ship;
            this.amount = amount;
        }

        public void OnEventTriggered(float time) {
            this.messageDelegate.SendActionableMessage(this.message, this.OnAcknowledge);
        }

        private void OnAcknowledge() {
            this.ship.Hull -= this.amount;
        }
    }

    public class DerelictShipEvent : IScheduleEvent {
        private readonly string message =
            "You get a distress signal from a nearby derelict ship. Should we board the ship or leave it alone?";

        private readonly IGameMessageDelegate messageDelegate;
        private readonly Ship ship;

        public DerelictShipEvent(IGameMessageDelegate messageDelegate, Ship ship) {
            this.messageDelegate = messageDelegate;
            this.ship = ship;
        }

        public void OnEventTriggered(float time) {
            this.messageDelegate.SendActionableMessage(this.message,
                new Dictionary<string, UnityAction> {{"Board Ship", this.BoardShip}, {"Leave it alone", null}});
        }

        private void BoardShip() {
            this.messageDelegate.SendActionableMessage("You have boarded the ship");

            // TODO: Gain a random two resources, but when you do you run the risk of losing a crew member, ammo, mood.
            throw new NotImplementedException();
        }
    }

    public interface IShipEvent : IEvent<Ship> { }

    public interface IShipEquipmentEvent : IEvent<ShipEquipment> { }

    public interface IGameEvent : IEvent<Game> { }

    public class GameEvent : IGameEvent {
        private Game game;
        public void OnEventTriggered(Game arg) {
            this.game = arg;

            this.game.At(this.game.GlobalTime, this.DoSomething);
        }

        private void DoSomething(float arg) {
            
        }
    }

    public class ShipStatusModifierEvent {
        private Ship ship;

        public string GetMessage() {
            return "This is a message.";
        }
    }

    public class ShipEquipmentEvent : IShipEquipmentEvent {
        private ShipEquipment ship;

        public void OnEventTriggered(ShipEquipment arg) {
            arg.Type = "Disabled";
        }
    }
}