using Zenject;
using ALP.Input;

namespace ALP.GameSceneInstallers
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputSystem>()
                .AsSingle();
        }
    }
}
