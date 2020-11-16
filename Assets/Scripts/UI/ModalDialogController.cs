using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI {
    public class ModalDialogController : MonoBehaviour {
        public Text messageText;
        public Transform buttonContainer;
        public GameObject buttonPrefab;
        private Action cbOnClose;

        private Action cbOnOpen;

        // Start is called before the first frame update
        private void Start() {
            this.gameObject.SetActive(false);
        }

        // Update is called once per frame
        private void Update() { }

        public void Dialog(string message, Dictionary<string, UnityAction> actions) {
            this.Open();
            this.messageText.text = message;

            // Remove all the old buttons
            foreach (Transform child in this.buttonContainer) {
                Destroy(child.gameObject);
            }

            // Make the new buttons
            foreach (var action in actions) {
                var go = Instantiate(this.buttonPrefab, this.buttonContainer);
                var button = go.GetComponent<Button>();
                button.GetComponentInChildren<Text>().text = action.Key;
                // Probably don't need to do this but no harm I suppose
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(action.Value);
                button.onClick.AddListener(this.Close);
            }
        }

        public void Dialog(string message, UnityAction onConfirm) {
            this.Dialog(message, new Dictionary<string, UnityAction> {{"OK", onConfirm}});
        }

        public void RegisterOnOpenCallback(Action cb) {
            this.cbOnOpen += cb;
        }

        public void UnregisterOnOpenCallback(Action cb) {
            this.cbOnOpen -= cb;
        }

        public void RegisterOnCloseCallback(Action cb) {
            this.cbOnClose += cb;
        }

        public void UnregisterOnCloseCallback(Action cb) {
            this.cbOnClose -= cb;
        }

        protected void Close() {
            this.gameObject.SetActive(false);
            this.cbOnClose?.Invoke();
        }

        protected void Open() {
            this.gameObject.SetActive(true);
            this.cbOnOpen?.Invoke();
        }
    }
}