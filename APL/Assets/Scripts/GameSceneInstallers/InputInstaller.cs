using Zenject;
using ALP.InputCode.CameraInput;
using ALP.InputCode.MouseInput;

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

            Container.Bind<IFurnitureInputService>()
                .To<FurnitureInputService>().AsSingle();
        }
    }
}
