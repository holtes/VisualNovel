using UnityEngine;

namespace _Project.Develop.Runtime.Data.Settings
{
    [CreateAssetMenu(menuName = "Settings/Game Config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private float _typingDelay;

        public float TypingDelay => _typingDelay;
    }
}