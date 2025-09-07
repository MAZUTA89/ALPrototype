namespace ALP.Interactables
{
    public interface IInteractable
    {
        void OnMouseStartDrag();
        void OnMouseStopDrag();
        void OnDrag();
    }
}
