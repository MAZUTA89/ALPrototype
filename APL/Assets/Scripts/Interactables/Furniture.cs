using ALP.InputCode.MouseInput;
using System;
using UnityEngine;
using Zenject;

namespace ALP.Interactables
{
    public class Furniture : MonoBehaviour, IInteractable, IObstacle
    {
        public Vector3Int Position { get; private set; }

        IFurnitureInputService _furnitureInputController;

        [Inject]
        public void Construct(IFurnitureInputService furnitureInputService)
        {
            _furnitureInputController = furnitureInputService;
        }

        public void OnMouseClick()
        {
        }

        public void OnMouseStartDrag()
        {
        }

        public void OnMouseStopDrag()
        {
        }
    }
}
