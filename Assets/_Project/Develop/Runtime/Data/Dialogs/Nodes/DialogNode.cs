using _Project.Develop.Runtime.Data.Characters;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Develop.Runtime.Data.Dialogs.Nodes
{
    [Serializable]
    public abstract class DialogNode : ICharacterConfigurable
    {
        [SerializeField] private string _id;

        [LabelText("Задний фон")]
        [SerializeField] private Sprite _background;

        [LabelText("Кто на сцене")]
        [SerializeField] private List<CharacterState> _charactersOnScene = new();

        [LabelText("Кто говорит")]
        [SerializeField] private Speaker _speaker = new();

        [TextArea(3, 8)]
        [LabelText("Текст")]
        [SerializeField] private string _text;

        public string Id { get { return _id; } set { _id = value; } }
        public Sprite Background => _background;
        public List<CharacterState> CharactersOnScene => _charactersOnScene;
        public Speaker Speaker => _speaker;
        public string Text => _text;

        [NonSerialized]
        protected List<DialogNode> _allNodes;

#if UNITY_EDITOR
        public void SetConfig(CharactersConfig config)
        {
            foreach (var character in _charactersOnScene) character?.SetConfig(config);
            _speaker?.SetConfig(config);
        }

        public void SetAvailableNodes(List<DialogNode> allNodes)
        {
            _allNodes = allNodes;
        }
#endif
    }
}