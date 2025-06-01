using _Project.Develop.Runtime.Presentation.Background;
using _Project.Develop.Runtime.Presentation.CharactersContainer.View;
using _Project.Develop.Runtime.Presentation.ChoiceWindow.View;
using _Project.Develop.Runtime.Presentation.DialogWindow.View;
using _Project.Develop.Runtime.Presentation.CharactersContainer.Models;
using _Project.Develop.Runtime.Presentation.ChoiceWindow.Models;
using _Project.Develop.Runtime.Presentation.DialogWindow.Models;
using _Project.Develop.Runtime.Core.Interfaces;
using _Project.Develop.Runtime.Data.Dialogs.Nodes;
using _Project.Develop.Runtime.Data.Dialogs;
using _Project.Develop.Runtime.Domain.Models;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Zenject;
using R3;


namespace _Project.Develop.Runtime.Domain.Controllers
{
    public class DialogController : MonoBehaviour
    {
        [SerializeField] private DialogWindowView _dialogWindowView;
        [SerializeField] private CharactersContainerView _charactersContainerView;
        [SerializeField] private BackgroundView _backgroundView;
        [SerializeField] private ChoiceViewList _choiceViewList;

        private DialogModel _model;
        private ICharacterLibrary _characterLibrary;

        [Inject]
        private void Construct(DialogModel dialogModel, ICharacterLibrary characterLibrary)
        {
            _model = dialogModel;
            _characterLibrary = characterLibrary;
        }

        private void Start()
        {
            _dialogWindowView.OnNextBtnClicked
                .Subscribe(_ => HandleNext())
                .AddTo(this);

            _dialogWindowView.OnPreviousBtnClicked
                .Subscribe(_ => HandleBack())
                .AddTo(this);

            Show(_model.GetCurrentNode());
        }

        private void HandleNext()
        {
            if (_model.TryGoNext()) Show(_model.GetCurrentNode());
        }

        private void HandleBack()
        {
            if (_model.TryGoBack()) Show(_model.GetCurrentNode());
        }

        public void SelectChoice(string nodeId)
        {
            if (_model.TryGoTo(nodeId)) Show(_model.GetCurrentNode());
        }

        private void Show(DialogNode node)
        {
            _backgroundView.SetBackgroundSprite(node.Background);
            _charactersContainerView.ShowCharacters(GetCharactersData(node.CharactersOnScene));
            _dialogWindowView.SetCharacterName(GetSpeakerData(node.Speaker));
            _dialogWindowView.SetDialogText(node.Text);

            _choiceViewList.ClearChoices();
            if (node is ChoiceNode choiceNode)
            {
                _choiceViewList.SetChoices(GetChoicesData(choiceNode.Choices), SelectChoice);
            }
        }

        private List<CharacterToDisplay> GetCharactersData(List<CharacterState> characterStates)
        {
            var characters = characterStates.Select(c => new CharacterToDisplay
            {
                Id = c.CharacterId,
                Sprite = _characterLibrary.GetSprite(c.CharacterId, c.EmotionType)
            }).ToList();

            return characters;
        }

        private SpeakerNameUIData GetSpeakerData(Speaker speaker)
        {
            return new SpeakerNameUIData
            {
                SpeakerName = speaker.GetDisplayName(_characterLibrary),
                NameColor = speaker.GetDisplayColor(_characterLibrary)
            };
        }

        private List<ChoiceUIData> GetChoicesData(List<Choice> choices)
        {
            var choicesData = choices.Select(c => new ChoiceUIData
            {
                Text = c.Text,
                ChoiceId = c.NextNodeId
            }).ToList();

            return choicesData;
        }
    }
}