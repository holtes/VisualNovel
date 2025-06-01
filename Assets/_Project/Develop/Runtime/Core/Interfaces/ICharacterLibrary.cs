using _Project.Develop.Runtime.Core.Types;
using UnityEngine;

namespace _Project.Develop.Runtime.Core.Interfaces
{
    public interface ICharacterLibrary
    {
        Sprite GetSprite(string characterId, EmotionType emotion);
        string GetDisplayName(string characterId);
        Color GetColor(string characterId);
    }
}