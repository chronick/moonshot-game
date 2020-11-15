using UnityEngine;
using UnityEngine.UI;

namespace UI {
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(RectTransform))]
    public class ProgressBarController : MonoBehaviour {
        public Color color;

        [Min(1f)] public float maxValue = 100f;

        private Image progressBarImage;
        private RectTransform rectTransform;

        // Start is called before the first frame update
        private void Start() {
            this.rectTransform = this.GetComponent<RectTransform>();

            this.progressBarImage = this.GetComponent<Image>();
            this.progressBarImage.color = this.color;
        }

        public void SetValue(float value) {
            var newValue = Mathf.Clamp(value / this.maxValue, 0, 1);
            if (this.rectTransform != null) {
                this.rectTransform.localScale = new Vector3(newValue, 1f, 1f);
            }
        }

        public void SetPercentValue(float percentValue) {
            this.SetValue(Mathf.Lerp(0, this.maxValue, percentValue / 100f));
        }
    }
}