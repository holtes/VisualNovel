using _Project.Develop.Runtime.Presentation.DialogWindow.Models;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using System;
using System.Threading.Tasks;
using Zenject;
using R3;


namespace _Project.Develop.Runtime.Presentation.DialogWindow.View
{
    public class DialogWindowView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _characterName;
        [SerializeField] private TMP_Text _dialogText;
        [SerializeField] private Button _dialogWindowBtn;
        [SerializeField] private Button _previousBtn;
        [SerializeField] private Button _nextBtn;

        public Observable<Unit> OnPreviousBtnClicked => _onPreviousBtnClicked;
        public Observable<Unit> OnNextBtnClicked => _onNextBtnClicked;

        private Subject<Unit> _onPreviousBtnClicked = new Subject<Unit>();
        private Subject<Unit> _onNextBtnClicked = new Subject<Unit>();

        private CancellationTokenSource _printCts;
        private bool _isTyping = false;

        private float _typingDelay;

        [Inject]
        private void Construct([Inject(Id = "TypingDelay")] float typingDelay)
        {
            _typingDelay = typingDelay;
        }

        private void Awake()
        {
            _previousBtn
                .OnClickAsObservable()
                .Subscribe(_ => SetPreviousDialog())
                .AddTo(this);
            _nextBtn
                .OnClickAsObservable()
                .Subscribe(_ => SetNextDialog())
                .AddTo(this);
            _dialogWindowBtn
                .OnClickAsObservable()
                .Subscribe(_ => ControlDialogPrinting());
        }

        public void SetCharacterName(SpeakerNameUIData nameUIData)
        {
            _characterName.text = nameUIData.SpeakerName;
            _characterName.color = nameUIData.NameColor;
        }

        public async void SetDialogText(string fullText)
        {
            _dialogText.text = "";
            _printCts?.Cancel();

            _printCts = new CancellationTokenSource();
            var token = _printCts.Token;

            _isTyping = true;

            try
            {
                int totalChars = fullText.Length;

                if (totalChars == 0)
                {
                    _isTyping = false;
                    return;
                }

                float delayPerChar = _typingDelay / totalChars;

                for (int i = 0; i < totalChars; i++)
                {
                    _dialogText.text = fullText.Substring(0, i + 1);
                    await Task.Delay(TimeSpan.FromSeconds(delayPerChar), token);
                }
            }
            catch (OperationCanceledException)
            {
                _dialogText.text = fullText;
            }
            finally
            {
                _isTyping = false;
            }
        }

        private void SetPreviousDialog()
        {
            _onPreviousBtnClicked.OnNext(Unit.Default);
        }

        private void SetNextDialog()
        {
            _onNextBtnClicked.OnNext(Unit.Default);
        }

        private void ControlDialogPrinting()
        {
            if (_isTyping) _printCts?.Cancel();
            else SetNextDialog();
        }
    }
}