using UnityEngine;

namespace UI
{
    public class PauseGame : MonoBehaviour
    {
        public void PauseGameTime()
        {
            Time.timeScale = 0;
        }
    
        public void ResumeGameTime()
        {
            Time.timeScale = 1;
        }
    }
}
