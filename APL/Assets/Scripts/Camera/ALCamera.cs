using ALP.GameData.Camera;
using ALP.InputCode;
using ALP.InputCode.CameraInput;
using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ALP.CameraCode
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class ALCamera : MonoBehaviour
    {
        CinemachineVirtualCamera _vcComponent;

        ICameraInputService _cameraInputService;

        Vector2 _direction;
        Vector3 _targetPosition;
        CameraSO _cameraSO;

        [Inject]
        public void Construct(ICameraInputService cameraInputService, CameraSO cameraSO)
        {
            _cameraInputService = cameraInputService;
            _cameraSO = cameraSO;
        }

        public void Initialize(CinemachineVirtualCamera vc)
        {
            if(_vcComponent == null)
                _vcComponent = GetComponent<CinemachineVirtualCamera>();

            transform.position = vc.transform.position;
            transform.rotation = vc.transform.rotation;

            _vcComponent.m_Lens.FieldOfView = vc.m_Lens.FieldOfView;
            _vcComponent.m_Lens.Dutch = vc.m_Lens.Dutch;
        }
        #region UnityMethods
        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        private void Update()
        {
            UpdateMovement();
        }

        private void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime);
        }
        #endregion

        void UpdateMovement()
        {
            _direction = _cameraInputService.GetWASDDirection();

            Vector2 movement = _direction * _cameraSO.Camera2dMovementSpeed;

            _targetPosition = new Vector3(transform.position.x + movement.x, transform.position.y,
                transform.position.z + movement.y);

            Debug.Log(_cameraInputService.MouseWheel().y);

            if(_cameraInputService.MouseWheel().y < 0)
            {
                _vcComponent.m_Lens.FieldOfView += Time.deltaTime * _cameraSO.CameraUpDownSpeed;
            }
            else if(_cameraInputService.MouseWheel().y > 0)
            {
                _vcComponent.m_Lens.FieldOfView -= Time.deltaTime * _cameraSO.CameraUpDownSpeed;
            }

            _vcComponent.m_Lens.FieldOfView = Mathf.Clamp(_vcComponent.m_Lens.FieldOfView,
                _cameraSO.FOVMin, _cameraSO.FOVMax);
        }

    }
}
