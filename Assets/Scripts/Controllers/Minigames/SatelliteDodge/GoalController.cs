using UnityEngine;
using System.Collections;

public class GoalController : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log(other.gameObject.name);
		Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
		rb.isKinematic = true;
		rb.velocity = Vector2.right * 0;
		StartCoroutine(MoveObject(other.gameObject.transform, other.gameObject.transform.position, transform.position, 2f));
	}
	
	IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
         float i = 0.0f;
         float rate = 1.0f/time;
         while (i < 1.0f) {
             i += Time.deltaTime * rate;
             thisTransform.position = Vector3.Lerp(startPos, endPos, i);
             yield return null; 
         }
    }
}
