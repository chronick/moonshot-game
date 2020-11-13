using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menus {
    public class MainMenuController : MonoBehaviour {
        private AsyncOperation sceneLoading;
        public GameObject mainMenu;
        public GameObject loadingProgressContainer;
        public ProgressBarController loadingProgress;


        // Start is called before the first frame update
        private void Start() {
            this.mainMenu.SetActive(true);
            this.loadingProgressContainer.SetActive(false);
        }

        // Update is called once per frame
        private void Update() {
            if (this.sceneLoading != null) {
                this.loadingProgress.SetValue(this.sceneLoading.progress);
            }
        }

        public void StartGame() {
            this.sceneLoading = SceneManager.LoadSceneAsync("Scenes/MainGame");
            this.sceneLoading.allowSceneActivation = true;
            this.mainMenu.SetActive(false);
            this.loadingProgressContainer.SetActive(true);
            this.loadingProgress.MaxValue = 1f;
        }
    }
}