using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.Presentation.Background
{
    public class BackgroundView : MonoBehaviour
    {
        [SerializeField] private Image _backgroundImg;

        public void SetBackgroundSprite(Sprite backgroundSprite)
        {
            _backgroundImg.sprite = backgroundSprite;
        }
    }
}