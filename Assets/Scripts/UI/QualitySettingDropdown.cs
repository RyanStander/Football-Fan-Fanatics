using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class QualitySettingDropdown : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown qualityDropdown;
        
        private void Start()
        {
            qualityDropdown.value = QualitySettings.GetQualityLevel();
        }

        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }
    }
}
