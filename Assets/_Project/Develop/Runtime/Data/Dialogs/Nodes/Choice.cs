using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Develop.Runtime.Data.Dialogs.Nodes
{
    [Serializable]
    public class Choice
    {
        [LabelText("Текст выбора")]
        [SerializeField] private string _text;

        [SerializeReference]
        [LabelText("Cледующая нода")]
        [ValueDropdown(nameof(GetAvailableNodes))]
        [SerializeField] string _nextNodeId;

        public string Text => _text;
        public string NextNodeId => _nextNodeId;

        [NonSerialized]
        private List<DialogNode> _allNodes;

        [NonSerialized]
        private DialogNode _currentNode;

#if UNITY_EDITOR

        public void SetAvailableNodes(List<DialogNode> allNodes)
        {
            _allNodes = allNodes;
        }

        public void SetCurrentNode(DialogNode node)
        {
            _currentNode = node;
        }

        private IEnumerable<ValueDropdownItem<string>> GetAvailableNodes()
        {
            return _allNodes?
                .Where(n => n != _currentNode)
                .Select(n => new ValueDropdownItem<string>(n.Id, n.Id))
                ?? Enumerable.Empty<ValueDropdownItem<string>>();
        }
#endif
    }
}