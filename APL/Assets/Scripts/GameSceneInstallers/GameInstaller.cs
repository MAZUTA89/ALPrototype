using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using ALP.GameData.Camera;
using ALP.Interactables;

namespace ALP.GameSceneInstallers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] CameraSO CameraSO;
        public override void InstallBindings()
        {
            Container.BindInstance(CameraSO).AsSingle();

            Container.BindInterfacesAndSelfTo<Furniture>()
                .AsTransient();
        }
    }
}
