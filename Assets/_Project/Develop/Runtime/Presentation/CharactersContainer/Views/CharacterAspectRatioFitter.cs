using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.Presentation.CharactersContainer
{
    [ExecuteAlways]
    [RequireComponent(typeof(LayoutElement))]
    public class CharacterAspectRatioFitter : MonoBehaviour
    {
        [SerializeField] private RectTransform _target;
        [SerializeField] private LayoutElement _layoutElement;
        [SerializeField] private float _aspectRatio = 0.75f;

        private float _lastHeight = -1f;


        private void OnRectTransformDimensionsChange()
        {
            UpdateSize();
        }

        private void UpdateSize()
        {
            float height = _target.rect.height;


            if (Mathf.Approximately(height, _lastHeight)) return;

            _lastHeight = height;

            float width = height / _aspectRatio;

            _target.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);

            _layoutElement.preferredWidth = width;
        }
    }
}