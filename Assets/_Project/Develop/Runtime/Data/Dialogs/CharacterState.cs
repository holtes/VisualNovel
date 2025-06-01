using _Project.Develop.Runtime.Core.Types;
using _Project.Develop.Runtime.Data.Characters;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Develop.Runtime.Data.Dialogs
{
    [Serializable]
    public class CharacterState : ICharacterConfigurable
    {
        [ValueDropdown(nameof(GetCharacterIds))]
        [PropertyOrder(0)]
        [SerializeField] private string _characterId;

        [ValueDropdown(nameof(GetAvailableEmotions), AppendNextDrawer = true)]
        [PropertyOrder(1)]
        [SerializeField] private EmotionType _emotionType;

        public string CharacterId => _characterId;
        public EmotionType EmotionType => _emotionType;

        [NonSerialized]
        private CharactersConfig _config;

#if UNITY_EDITOR
        public void SetConfig(CharactersConfig config)
        {
            _config = config;
        }

        private List<string> GetCharacterIds()
        {
            return _config?.Characters.Select(c => c.Id).ToList() ?? new();
        }

        private IEnumerable<EmotionType> GetAvailableEmotions()
        {
            var character = _config?.Characters.FirstOrDefault(c => c.Id == _characterId);
            return character?.Emotions.Select(e => e.type) ?? new List<EmotionType>();
        }
#endif
    }
}