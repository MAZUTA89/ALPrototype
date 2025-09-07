using System;
using TMPro;
using UnityEngine;

namespace ALP.UI
{
    public class SceneUI
    {
        TextMeshProUGUI _turnText;
        TextMeshProUGUI _levelNameText;

        public SceneUI(TextMeshProUGUI turnText,
            TextMeshProUGUI levelNameText)
        {
            _levelNameText = levelNameText;
            _turnText = turnText;
        }

        public void SetLevelText(string text)
        {
            _levelNameText.text = text;
        }
        public void SetCurrentTurns(int turns)
        {
             _turnText.text = turns.ToString();
        }
    }
}
