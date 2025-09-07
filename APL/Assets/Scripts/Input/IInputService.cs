namespace ALP.InputCode
{
    public interface IInputService
    {
        ALInput MainMap { get; }
        void Enable();
        void Disable();
        void Dispose();
        
    }
}
