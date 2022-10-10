using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class LevelDataLoader : MonoBehaviour
    {
        [SerializeField] private LevelDataHolder _levelDataHolder;
        [SerializeField] private Transform _spawnParent;

        private List<LevelDataDisplay> _levelDataDisplays;

        public void LoadSceneData()
        {
            _levelDataDisplays = new List<LevelDataDisplay>();

            foreach (Transform child in _spawnParent.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (var levelData in _levelDataHolder.LevelDatas)
            {
                var levelDataDisplayObject = Instantiate(_levelDataHolder.LevelDataDisplayPrefab, _spawnParent);

                if (!levelDataDisplayObject.TryGetComponent(out LevelDataDisplay dataDisplay)) continue;

                dataDisplay.InitializeData(levelData.SceneName, levelData.LevelDisplayName,
                    levelData.LevelPreviewSprite);
                _levelDataDisplays.Add(dataDisplay);
            }
        }

        public IEnumerable<LevelDataDisplay> GetSceneDataDisplays() => _levelDataDisplays;
    }
}
