using ALP.UI;
using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace ALP.GameSceneInstallers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] TextMeshProUGUI _turnText;
        [SerializeField] TextMeshProUGUI _levelNameText;
        public override void InstallBindings()
        {
            SceneUI sceneUI = new SceneUI(_turnText, _levelNameText);

            Container.BindInstance(sceneUI)
                .AsSingle();
        }
    }
}
