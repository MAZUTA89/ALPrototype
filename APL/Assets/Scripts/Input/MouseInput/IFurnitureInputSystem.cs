using ALP.InputCode;
using System;

namespace ALP.InputCode.MouseInput
{
    public interface IFurnitureInputSystem : IInputService
    {
        bool IsStartDragFurniture();
        bool IsEndDragFurniture();
    }
}
