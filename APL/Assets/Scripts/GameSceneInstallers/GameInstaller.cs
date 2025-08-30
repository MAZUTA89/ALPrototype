using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using ALP.GameData.Camera;

namespace ALP.GameSceneInstallers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] CameraSO CameraSO;
        public override void InstallBindings()
        {
            Container.BindInstance(CameraSO).AsSingle();
        }
    }
}
