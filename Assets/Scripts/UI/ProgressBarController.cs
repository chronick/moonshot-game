using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]
public class ProgressBarController : MonoBehaviour {
    
    public Color color;
    
    [Min(1f)]
    public float MaxValue = 100f;

    private Image progessBarImage;
    private RectTransform rectTransform;
    
    // Start is called before the first frame update
    void Start() {
        this.progessBarImage = this.GetComponent<Image>();
        this.rectTransform = this.GetComponent<RectTransform>();
    }

    public void SetValue(float value) {
        var newValue = Mathf.Clamp(value / this.MaxValue, 0, this.MaxValue);
        this.rectTransform.localScale = new Vector3(newValue, 1f, 1f);
    }

    public void SetPercentValue(float percentValue) {
        this.SetValue(Mathf.Lerp(0, this.MaxValue, percentValue / 100f));
    }
}
