using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class Char : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Image _image;

        [HideInInspector] public char CorrectChar;
        [HideInInspector] public bool IsCorrectChar = false;

        public event UnityAction OnValueChange;
        
        public void OnValueChanged()
        {
            string text = _inputField.text;
            IsCorrectChar = string.Equals(CorrectChar.ToString(), text, StringComparison.CurrentCultureIgnoreCase);
            OnValueChange?.Invoke();
        }

        public void SetState(CharState charState)
        {
            _image.color = charState switch
            {
                CharState.Correct => new Color(0.33f, 1f, 0.36f),
                CharState.Incorrect => new Color(1f, 0.3f, 0.25f),
                CharState.Free => new Color(0.7764707f, 0.8313726f, 0.8901961f),
                _ => throw new ArgumentOutOfRangeException(nameof(charState), charState, null)
            };
        }
    }
}