using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelDataDisplay : MonoBehaviour
    {
        [SerializeField] private Image _levelDisplayImage;
        [SerializeField] private TMP_Text _levelDisplayName;
        [SerializeField] private GameObject _highlightObject;
        public Button selectLeveButton;
        private string sceneName;

        public void InitializeData(string sceneName, string displayName, Sprite displayImage)
        {
            this.sceneName = sceneName;
            _levelDisplayImage.sprite = displayImage;
            _levelDisplayName.text = displayName;
        }

        public string GetSceneName() => sceneName;

        public void ToggleHighlight(bool setActive)
        {
            _highlightObject.SetActive(setActive);
        }
    }
}
