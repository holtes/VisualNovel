using _Project.Develop.Runtime.Core.Interfaces;
using _Project.Develop.Runtime.Data.Characters;
using _Project.Develop.Runtime.Data.Dialogs;
using _Project.Develop.Runtime.Data.Settings;
using _Project.Develop.Runtime.Domain.Models;
using _Project.Develop.Runtime.Infrastructure.Characters;
using _Project.Develop.Runtime.Presentation.CharactersContainer.View;
using _Project.Develop.Runtime.Presentation.ChoiceWindow.View;
using UnityEngine;
using Zenject;

namespace _Project.Develop.Runtime.Bootstrap.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private CharactersConfig _charactersConfig;
        [SerializeField] private DialogConfig _dialogConfig;

        [SerializeField] private CharacterView _characterPrefab;
        [SerializeField] private ChoiceView _choicePrefab;

        public override void InstallBindings()
        {
            InstallGameConfig();
            InstallTypingDelay();
            InstallCharactersLibrary();
            InstallDialogModel();
            InstallCharacterPrefab();
            InstallChoicePrefab();
        }

        private void InstallGameConfig()
        {
            Container
                .Bind<GameConfig>()
                .FromInstance(_gameConfig);
        }

        private void InstallTypingDelay()
        {
            Container
                .Bind<float>()
                .WithId("TypingDelay")
                .FromInstance(_gameConfig.TypingDelay);
        }

        private void InstallCharactersLibrary()
        {
            Container
                .Bind<ICharacterLibrary>()
                .To<CharacterLibraryFromConfig>()
                .AsSingle()
                .WithArguments(_charactersConfig);
        }

        private void InstallDialogModel()
        {
            Container
                .Bind<DialogModel>()
                .AsSingle()
                .WithArguments(_dialogConfig);
        }

        private void InstallCharacterPrefab()
        {
            Container
                .Bind<CharacterView>()
                .FromInstance(_characterPrefab);
        }

        private void InstallChoicePrefab()
        {
            Container
                .Bind<ChoiceView>()
                .FromInstance(_choicePrefab);
        }
    }
}