using _Project.Develop.Runtime.Core.Types;
using _Project.Develop.Runtime.Core.Interfaces;
using _Project.Develop.Runtime.Data.Characters;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _Project.Develop.Runtime.Data.Dialogs
{
    [Serializable]
    public class Speaker : ICharacterConfigurable
    {
        [LabelText("Тип")]
        [SerializeField] private SpeakerType _type = SpeakerType.Character;

        [ShowIf(nameof(_type), SpeakerType.Character)]
        [ValueDropdown(nameof(GetCharacterIds))]
        [LabelText("ID персонажа")]
        [SerializeField] private string _characterId;

        [ShowIf(nameof(_type), SpeakerType.Manual)]
        [LabelText("Имя вручную")]
        [SerializeField] private string _manualName;

        [ShowIf(nameof(_type), SpeakerType.Manual)]
        [LabelText("Цвет вручную")]
        [ColorPalette]
        [SerializeField] private Color _manualNameColor;

        [NonSerialized]
        private CharactersConfig _config;

        public string GetDisplayName(ICharacterLibrary characterLibrary)
        {
            return _type switch
            {
                SpeakerType.Character => characterLibrary.GetDisplayName(_characterId) ?? $"{_characterId}",
                SpeakerType.Manual => _manualName,
                _ => "???"
            };
        }

        public Color GetDisplayColor(ICharacterLibrary characterLibrary)
        {
            return _type switch
            {
                SpeakerType.Character => characterLibrary.GetColor(_characterId),
                SpeakerType.Manual => _manualNameColor,
                _ => Color.black
            };
        }

#if UNITY_EDITOR
        public void SetConfig(CharactersConfig config)
        {
            _config = config;
        }

        private IEnumerable<string> GetCharacterIds()
        {
            return _config?.Characters.Select(c => c.Id) ?? Enumerable.Empty<string>();
        }
#endif
    }
}