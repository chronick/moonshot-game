using System;
using System.Collections;
using UnityEngine;

public class GoalController : MonoBehaviour {
    private Action onGoalEntered;

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.gameObject.name);
        var rb = other.gameObject.GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        rb.velocity = Vector2.right * 0;
        this.StartCoroutine(this.MoveObject(other.gameObject.transform,
            other.gameObject.transform.position,
            this.transform.position,
            2f));
    }

    private IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time) {
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f) {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
        this.onGoalEntered?.Invoke();
    }

    public void RegisterOnGoalEnteredCallback(Action cb) {
        this.onGoalEntered += cb;
    }
}