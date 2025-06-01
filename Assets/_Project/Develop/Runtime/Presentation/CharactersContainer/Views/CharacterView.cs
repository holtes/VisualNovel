using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.Presentation.CharactersContainer.View
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private Image _characterImg;
        [SerializeField] private CanvasGroup _characterAlpha;
        [SerializeField] private LayoutElement _characterLayout;
        [SerializeField] private RectTransform _characterRect;

        public void SetCharacterSprite(Sprite characterSprite)
        {
            _characterImg.sprite = characterSprite;
        }

        public void SetOrientation(bool left)
        {
            _characterRect.localScale = new Vector3(left ? 1 : -1, 1, 1);
        }

        public void SetVisible(bool visible)
        {
            _characterAlpha.alpha = visible ? 1 : 0;
            _characterAlpha.blocksRaycasts = visible;
            _characterImg.raycastTarget = visible;
            _characterLayout.ignoreLayout = !visible;
        }
    }
}