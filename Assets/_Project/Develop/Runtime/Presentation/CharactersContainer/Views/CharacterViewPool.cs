using _Project.Develop.Runtime.Presentation.CharactersContainer.Models;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Develop.Runtime.Presentation.CharactersContainer.View
{
    public class CharacterViewPool
    {
        private readonly RectTransform _parent;
        private readonly CharacterView _prefab;
        private readonly List<CharacterView> _pool = new();

        public CharacterViewPool(RectTransform parent, CharacterView prefab)
        {
            _parent = parent;
            _prefab = prefab;
        }

        public void ShowCharacters(List<CharacterToDisplay> characters)
        {
            if (characters.Count == 0) return;

            EnsureSize(characters.Count);

            for (int i = 0; i < _pool.Count; i++)
            {
                var active = i < characters.Count;
                var slot = _pool[i];

                if (active)
                {
                    var data = characters[i];
                    var isLeft = i < characters.Count / 2;

                    slot.SetCharacterSprite(data.Sprite);
                    slot.SetOrientation(!isLeft);
                }

                slot.SetVisible(active);
            }
        }

        private void EnsureSize(int count)
        {
            while (_pool.Count < count) AddSlot();
        }

        private CharacterView AddSlot()
        {
            var character = Object.Instantiate(_prefab, _parent);
            _pool.Add(character);
            return character;
        }
    }
}