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
            Debug.Log(gameObject.name + " Clicked");
        }

        public void OnMouseStartDrag()
        {
            Debug.Log(gameObject.name + " Start drag");
        }

        public void OnMouseStopDrag()
        {
            Debug.Log(gameObject.name + " End drag");
        }

        public void OnDrag()
        {
            Debug.Log(gameObject.name + " Dragging");
        }
    }
}
