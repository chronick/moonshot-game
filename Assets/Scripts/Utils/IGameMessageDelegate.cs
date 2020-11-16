using System.Collections.Generic;
using UnityEngine.Events;

namespace Utils {
    public interface IGameMessageDelegate {
        void SendActionableMessage(string message, Dictionary<string, UnityAction> actions);
    }
}