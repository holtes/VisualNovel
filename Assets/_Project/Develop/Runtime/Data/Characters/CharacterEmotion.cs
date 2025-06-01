using _Project.Develop.Runtime.Core.Types;
using System;
using UnityEngine;

namespace _Project.Develop.Runtime.Data.Characters
{
    [Serializable]
    public struct CharacterEmotion
    {
        public EmotionType type;
        public Sprite sprite;
    }
}