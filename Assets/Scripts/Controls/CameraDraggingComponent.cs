using UnityEngine;

namespace Controls
{
    public class CameraDraggingComponent : MonoBehaviour
    {
        [SerializeField] private Transform _virtualCameraToDrag;
        [SerializeField] private float _camDragSpeed = 25;

        [Tooltip("Closer to 0 is preference for height, closer to 1 is preference for width")]
        [SerializeField]
        [Range(0, 1)]
        private float _dragPreference = 0.5f;

        private Vector3 _dragOrigin;
        private Vector3 _virtualCameraOrigin;
        private bool _isDragging;


        private void LateUpdate()
        {
            // If we press the right mouse button, begin selection and remember the location of the mouse
            if ((Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
            {
                _dragOrigin = Input.mousePosition;
                _virtualCameraOrigin = _virtualCameraToDrag.position;
                return;
            }

            // Drag the camera
            if (!Input.GetMouseButton(1) && !Input.GetMouseButton(2)) return;

            var pos = Camera.main.ScreenToViewportPoint(_dragOrigin - Input.mousePosition);
            _virtualCameraToDrag.position =
                _virtualCameraOrigin + new Vector3(pos.x * _camDragSpeed * _dragPreference, 0,
                    pos.y * _camDragSpeed * (1-_dragPreference));
        }
    }
}