using ALP.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ALP.GameSceneInstallers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] TextMeshProUGUI _turnText;
        [SerializeField] TextMeshProUGUI _levelNameText;
        [SerializeField] Button _closeButton;
        public override void InstallBindings()
        {
            SceneUI sceneUI = new SceneUI(_turnText,
                _levelNameText, _closeButton);

            Container.BindInstance(sceneUI)
                .AsSingle();
        }
    }
}
