using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Utils {
    public interface IGameMessageDelegate {
        void SendActionableMessage(string message, Dictionary<string, UnityAction> actions);
        void SendActionableMessage(string message, UnityAction action);
        void SendActionableMessage(string message);
    }
}