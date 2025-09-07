using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ALP.UI
{
    public class SceneUI : IDisposable
    {
        TextMeshProUGUI _turnText;
        TextMeshProUGUI _levelNameText;
        Button _closeButton;

        public SceneUI(TextMeshProUGUI turnText,
            TextMeshProUGUI levelNameText,
            Button closeButton)
        {
            _levelNameText = levelNameText;
            _turnText = turnText;
            _closeButton = closeButton;

            _closeButton.onClick.AddListener(OnCloseButton_Click);
        }

        public void SetLevelText(string text)
        {
            _levelNameText.text = text;
        }
        public void SetCurrentTurns(int turns)
        {
             _turnText.text = turns.ToString();
        }

        private void OnCloseButton_Click()
        {
            Application.Quit();
        }

        public void Dispose()
        {
            _closeButton.onClick.RemoveListener(OnCloseButton_Click);
        }
    }
}
