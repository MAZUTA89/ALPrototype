using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using ALP.GameData.Camera;
using ALP.Interactables;
using ALP.CursorRay;

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
            Container.BindInterfacesAndSelfTo<WakeUpFurniture>()
                .AsTransient();
            Container.BindInterfacesAndSelfTo<PlayerFurniture>()
                .AsTransient();

            Container.Bind<ALCursor>().
                FromComponentInHierarchy(true)
                .AsSingle();
        }
    }
}
