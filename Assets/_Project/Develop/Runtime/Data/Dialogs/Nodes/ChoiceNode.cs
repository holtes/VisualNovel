using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace _Project.Develop.Runtime.Data.Dialogs.Nodes
{
    [System.Serializable]
    public class ChoiceNode : DialogNode
    {
        [LabelText("Выбор игрока")]
        [ListDrawerSettings(Expanded = true)]
        [OnValueChanged(nameof(SetAvailableNodesForChoices), IncludeChildren = true)]
        [UnityEngine.SerializeField] private List<Choice> _choices = new();

        public List<Choice> Choices => _choices;

#if UNITY_EDITOR
        private void SetAvailableNodesForChoices()
        {
            foreach (var choice in _choices)
            {
                choice.SetAvailableNodes(_allNodes);
                choice.SetCurrentNode(this);
            }
        }
#endif
    }
}