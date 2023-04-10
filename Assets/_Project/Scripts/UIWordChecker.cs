using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class UIWordChecker : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private Char _prefabChar;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _attemptText;
        [SerializeField] private GameObject _tipParent;
        [SerializeField] private TMP_Text _tipText;
        [SerializeField] private Button _tipButton;

        [Space(15F)] [SerializeField] private TMP_Text _levelsPassedText;
        [SerializeField] private TMP_Text _attemtsUsedText;
        [SerializeField] private TMP_Text _finalTittle;

        [SerializeField] private List<WordInfo> _words = new List<WordInfo>();

        [SerializeField] private UnityEvent OnGameOver;

        private List<Char> _chars = new(8);
        private int _level = 1;
        private int _attempts = 3;
        private int _attempsUsed = 0;

        public void GenerateWord()
        {
            if (_level - 1 >= _words.Count)
            {
                OnGameOver?.Invoke();
                return;
            }

            ClearWord();
            DisplayStats();
            _attempts = 3;

            for (int i = 0; i < _words[_level - 1].Word.Length; i++)
            {
                char c = _words[_level - 1].Word[i];
                Char charObject = Instantiate(_prefabChar, _parent);
                charObject.SetState(CharState.Free);
                charObject.CorrectChar = c;
                int i1 = i;
                charObject.OnValueChange += () => SelectFocusChar(i1);
                _chars.Add(charObject);
            }
        }

        public void FillTip()
        {
            string tip = _words[_level - 1].Word;

            while (_words[_level - 1].Word == tip)
            {
                tip = ShuffleString(_words[_level - 1].Word);
            }

            _tipText.text = tip;
        }

        public void CheckCorrectWord()
        {
            int countCorrect = 0;
            foreach (Char charObject in _chars)
            {
                if (charObject.IsCorrectChar)
                {
                    countCorrect++;
                    charObject.SetState(CharState.Correct);
                }
                else
                {
                    charObject.SetState(CharState.Incorrect);
                }
            }

            if (countCorrect == _chars.Count)
            {
                _level++;
                _tipButton.interactable = true;
                GenerateWord();
            }
            else
            {
                _attempts--;
                _attempsUsed++;
                if (_attempts == 0)
                {
                    OnGameOver?.Invoke();
                }
            }

            if (_level <= _words.Count)
                DisplayStats();
        }

        public void RestartLevels()
        {
            _level = 1;
            _attempts = 3;
            _attempsUsed = 0;
            _tipButton.interactable = true;
            _tipParent.SetActive(false);
            
        }

        public void Exit()
        {
            Application.Quit();
        }
        
        public void DisplayFinishStats()
        {
            if (_level > _words.Count / 1.2F)
            {
                _finalTittle.text = $"Ты молодец!";
            }
            else
            {
                _finalTittle.text = $"Ты можешь лучше!";
            }

            _levelsPassedText.text = $"Пройдено {_level - 1}/{_words.Count} уровней";
            _attemtsUsedText.text = $"Потрачено всего {_attempsUsed} попыток";
        }

        private void ClearWord()
        {
            _tipParent.SetActive(false);
            _tipText.text = "";

            foreach (Char c in _chars)
            {
                Destroy(c.gameObject);
            }

            _chars.Clear();
        }

        private void DisplayStats()
        {
            _description.text = _words[_level - 1].Description;
            _levelText.text = $"Уровень: {_level}/{_words.Count}";
            _attemptText.text = $"Осталось {_attempts} попытка(и)";
        }


        private void SelectFocusChar(int value)
        {
            if (value + 1 < _chars.Count)
            {
                _chars[value + 1].GetComponent<TMP_InputField>().Select();
            }
        }

        private string ShuffleString(string input)
        {
            char[] array = input.ToUpper().ToCharArray();
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                (array[k], array[n]) = (array[n], array[k]);
            }

            string str = "";
            foreach (char c in array)
            {
                str += c + " ";
            }

            return str;
        }
    }
}