using UnityEngine;

public class AutomaticSize : MonoBehaviour {
    public enum SizeDirection {
        VERTICAL,
        HORIZONTAL
    }

    public float childSize = 35f;

    public SizeDirection direction = SizeDirection.VERTICAL;

    // Start is called before the first frame update
    private void Start() {
        this.AdjustSize();
    }

    public void AdjustSize() {
        var sizeDelta = this.GetComponent<RectTransform>().sizeDelta;
        var finalSize = this.transform.childCount * this.childSize;
        if (this.direction == SizeDirection.VERTICAL) {
            sizeDelta.y = finalSize; 
        }
        else {
            sizeDelta.x = finalSize;
        }
        this.GetComponent<RectTransform>().sizeDelta = sizeDelta;
    }
}