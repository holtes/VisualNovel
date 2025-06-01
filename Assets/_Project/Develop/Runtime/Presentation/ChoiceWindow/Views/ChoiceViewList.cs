using _Project.Develop.Runtime.Presentation.ChoiceWindow.Models;
using R3;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project.Develop.Runtime.Presentation.ChoiceWindow.View
{
    public class ChoiceViewList : MonoBehaviour
    {
        [SerializeField] private Transform _choicesContainer;

        private List<ChoiceView> _choiceViews = new();

        private ChoiceView _choicePrefab;

        [Inject]
        private void Construct(ChoiceView choicePrefab)
        {
            _choicePrefab = choicePrefab;
        }

        public void SetChoices(List<ChoiceUIData> choices, Action<string> actionOnChoice)
        {
            ClearChoices();

            foreach (var choice in choices)
            {
                var choiceView = Instantiate(_choicePrefab, _choicesContainer);
                choiceView.SetChoiceData(choice);
                choiceView.OnChoiceClicked
                    .Subscribe(actionOnChoice)
                    .AddTo(choiceView);

                _choiceViews.Add(choiceView);
            }
        }

        public void ClearChoices()
        {
            foreach (var choiceView in _choiceViews) Destroy(choiceView.gameObject);
            _choiceViews.Clear();
        }
    }
}