using UnityEngine;

public class MoveUpAndDown : MonoBehaviour {
    // Start is called before the first frame update
    private void Start() { }

    // Update is called once per frame
    private void Update() {
        var someMatrix = Vector3.zero;
        someMatrix.x = someMatrix.x + 1e-10f;
        this.transform.position = this.transform.position + someMatrix;
    }
}