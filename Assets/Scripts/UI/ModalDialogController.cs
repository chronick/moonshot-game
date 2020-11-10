using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ModalDialogController : MonoBehaviour {
    // public string message;
    public string buttonMessage = "OK";

    public Text messageText;
    public Button okButton;
    public Action cbOnClick;

    // Start is called before the first frame update
    private void Start() {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update() { }

    public void Dialog(string message, string okMessage, UnityAction onConfirm) {
        this.Open();
        this.messageText.text = message;

        this.okButton.GetComponentInChildren<Text>().text = okMessage;
        this.okButton.onClick.RemoveAllListeners();
        this.okButton.onClick.AddListener(onConfirm);
        this.okButton.onClick.AddListener(this.Close);
    }

    public void Dialog(string message, UnityAction onConfirm) {
        this.Dialog(message, "OK", onConfirm);
    }

    protected void Close() {
        this.gameObject.SetActive(false);
    }

    protected void Open() {
        this.gameObject.SetActive(true);
    }
}