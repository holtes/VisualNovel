using _Project.Develop.Runtime.Presentation.CharactersContainer.Models;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project.Develop.Runtime.Presentation.CharactersContainer.View
{
    public class CharactersContainerView : MonoBehaviour
    {
        [SerializeField] private RectTransform _charactersContainer;

        private CharacterViewPool _pool;

        private CharacterView _characterPrefab;

        [Inject]
        private void Construct(CharacterView characterPrefab)
        {
            _characterPrefab = characterPrefab;
        }

        private void Awake()
        {
            _pool = new CharacterViewPool(_charactersContainer, _characterPrefab);
        }

        public void ShowCharacters(List<CharacterToDisplay> characters)
        {
            _pool.ShowCharacters(characters);
        }
    }
}