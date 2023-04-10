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
                CharState.Correct => new Color(0.3f, 1f, 0.3f),
                CharState.Incorrect => new Color(1f, 0.2f, 0.2f),
                CharState.Free => new Color(1f, 1f, 1f),
                _ => throw new ArgumentOutOfRangeException(nameof(charState), charState, null)
            };
        }
    }
}