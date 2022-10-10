using System;
using Cinemachine;
using UnityEngine;

namespace Controls
{
    public class CameraZoomingComponent : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
        [SerializeField] private float _zoomSpeed = 0.1f;
        [SerializeField] private float _zoomMinAmount;
        [SerializeField] private float _zoomMaxAmount;

        private void LateUpdate()
        {
            HandleZoom();
        }

        private void HandleZoom()
        {
            _cinemachineVirtualCamera.m_Lens.OrthographicSize -= Input.mouseScrollDelta.y * _zoomSpeed;

            if (_zoomMinAmount > _cinemachineVirtualCamera.m_Lens.OrthographicSize)
            {
                _cinemachineVirtualCamera.m_Lens.OrthographicSize = _zoomMinAmount;
            }
            else if (_zoomMaxAmount < _cinemachineVirtualCamera.m_Lens.OrthographicSize)
            {
                _cinemachineVirtualCamera.m_Lens.OrthographicSize = _zoomMaxAmount;
            }
        }
    }
}