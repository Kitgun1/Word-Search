using System;
using NaughtyAttributes;

namespace _Project.Scripts
{
    [Serializable]
    public struct WordInfo
    {
        public string Word;
        [ResizableTextArea, AllowNesting] public string Description;
    }
}