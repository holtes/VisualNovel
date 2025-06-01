using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _Project.Develop.Runtime.Data.Characters
{
    [Serializable]
    public class CharacterData
    {
        [LabelText("ID ���������")]
        [SerializeField] private string _id;

        [LabelText("��� ��� UI")]
        [SerializeField] private string _displayName;

        [LabelText("���� ����������� � UI")]
        [ColorPalette]
        [SerializeField] private Color _displayColor;

        [ValidateInput(nameof(HasNoDuplicateEmotions), "������ �� ������ �����������")]
        [ValidateInput(nameof(EmotionsExists), "� ��������� ������ ���� ���� �� ���� ������")]
        [TableList(AlwaysExpanded = true)]
        [SerializeField] private List<CharacterEmotion> _emotions = new();

        public string Id => _id;
        public string DisplayName => _displayName;
        public Color DisplayColor => _displayColor;
        public List<CharacterEmotion> Emotions => _emotions;

#if UNITY_EDITOR
        private bool HasNoDuplicateEmotions()
        {
            return _emotions.Select(e => e.type).Distinct().Count() == _emotions.Count;
        }

        private bool EmotionsExists()
        {
            return _emotions.Count != 0;
        }
#endif
    }
}