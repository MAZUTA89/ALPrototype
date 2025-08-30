using Zenject;
using ALP.InputCode;
using ALP.InputCode.CameraInput;

namespace ALP.GameSceneInstallers
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            ALInput mainInputMap = new ALInput();

            Container.BindInstance(mainInputMap)
                .AsSingle();

            Container.Bind<ICameraInputService>()
                .To<CameraInputService>().AsSingle();
        }
    }
}
