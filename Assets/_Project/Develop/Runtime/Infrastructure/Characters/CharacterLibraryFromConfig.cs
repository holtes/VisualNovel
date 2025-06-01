using _Project.Develop.Runtime.Core.Interfaces;
using _Project.Develop.Runtime.Core.Types;
using _Project.Develop.Runtime.Data.Characters;
using System.Linq;
using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.Characters
{
    public class CharacterLibraryFromConfig : ICharacterLibrary
    {
        private readonly CharactersConfig _config;

        public CharacterLibraryFromConfig(CharactersConfig config)
        {
            _config = config;
        }

        public Sprite GetSprite(string id, EmotionType emotion)
        {
            var character = _config.Characters.FirstOrDefault(c => c.Id == id);
            return character?.Emotions.FirstOrDefault(e => e.type == emotion).sprite;
        }

        public string GetDisplayName(string id)
        {
            return _config.Characters.FirstOrDefault(c => c.Id == id).DisplayName ?? id;
        }

        public Color GetColor(string id)
        {
            return _config.Characters.FirstOrDefault(c => c.Id == id).DisplayColor;
        }
    }
}