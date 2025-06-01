using _Project.Develop.Runtime.Presentation.ChoiceWindow.Models;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using R3;

namespace _Project.Develop.Runtime.Presentation.ChoiceWindow.View
{
    public class ChoiceView : MonoBehaviour
    {
        [SerializeField] private Button _choiceBtn;
        [SerializeField] private TMP_Text _choiceText;

        private string _choiceId;

        public Observable<string> OnChoiceClicked => _onChoiceClicked;

        private Subject<string> _onChoiceClicked = new();

        private void Awake()
        {
            _choiceBtn
                .OnClickAsObservable()
                .Subscribe(_ => ChoiceClick())
                .AddTo(this);
        }

        public void SetChoiceData(ChoiceUIData choiceUIData)
        {
            _choiceText.text = choiceUIData.Text;
            _choiceId = choiceUIData.ChoiceId;
        }

        private void ChoiceClick()
        {
            _onChoiceClicked.OnNext(_choiceId);
        }
    }
}