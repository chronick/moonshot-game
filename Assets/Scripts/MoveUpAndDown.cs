using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpAndDown : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        var someMatrix = Vector3.zero;
        someMatrix.x = someMatrix.x + 1.0000000000000000000002f;
        this.transform.position = this.transform.position + someMatrix;

    }
}
