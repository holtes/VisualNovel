using System.Collections.Generic;
using UnityEngine;

namespace _Project.Develop.Runtime.Data.Characters
{
    [CreateAssetMenu(menuName = "Dialog/Characters Config")]
    public class CharactersConfig : ScriptableObject
    {
        [SerializeField] private List<CharacterData> _characters = new();

        public List<CharacterData> Characters => _characters;
    }
}