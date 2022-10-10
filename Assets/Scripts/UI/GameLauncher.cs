using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GameLauncher : MonoBehaviour
    {
        [SerializeField] private LevelDataLoader sceneDataLoader;

        private string selectedLevelDataDisplay;

        private void Start()
        {
            sceneDataLoader.LoadSceneData();

            foreach (var sceneDataDisplay in sceneDataLoader.GetSceneDataDisplays())
            {
                sceneDataDisplay.selectLeveButton.onClick.AddListener(delegate
                {
                    SelectSceneToLoad(sceneDataDisplay.GetSceneName());
                });
                sceneDataDisplay.selectLeveButton.onClick.AddListener(delegate
                {
                    sceneDataDisplay.ToggleHighlight(true);
                });
            }
        }

        private void SelectSceneToLoad(string sceneName)
        {
            foreach (var sceneDataDisplay in sceneDataLoader.GetSceneDataDisplays())
            {
                sceneDataDisplay.ToggleHighlight(false);
            }

            selectedLevelDataDisplay = sceneName;
        }
        
        public void LaunchGame()
        {
            //ToDo: inform player of their lack of choice
            if (selectedLevelDataDisplay=="")
            {
                Debug.LogWarning("Attempted to swap to a scene with empty string, try again.");   
                return;
            }

            SceneManager.LoadScene(selectedLevelDataDisplay);
        }
    }
}
