using _Project.Develop.Runtime.Data.Dialogs;
using _Project.Develop.Runtime.Data.Dialogs.Nodes;
using System.Collections.Generic;
using System.Linq;

namespace _Project.Develop.Runtime.Domain.Models
{
    public class DialogModel
    {
        private readonly Dictionary<string, DialogNode> _nodeMap;
        private readonly Stack<DialogNode> _historyStack = new();

        private DialogNode _currentNode;

        public DialogModel(DialogConfig config)
        {
            _nodeMap = config.Nodes.ToDictionary(n => n.Id);
            _currentNode = _nodeMap.Values.FirstOrDefault();
        }

        public DialogNode GetCurrentNode() => _currentNode;

        public DialogNode GetNode(string nodeId) =>
            _nodeMap.TryGetValue(nodeId, out var node) ? node : null;

        public bool TryGoTo(string nodeId)
        {
            if (!_nodeMap.TryGetValue(nodeId, out var node)) return false;
            return TryGoTo(node);
        }

        public bool TryGoTo(DialogNode node)
        {
            if (node == null || node == _currentNode) return false;
            _historyStack.Push(_currentNode);
            _currentNode = node;
            return true;
        }

        public bool TryGoNext()
        {
            if (_currentNode is SimpleNode simple && _nodeMap.TryGetValue(simple.NextNodeId, out var nextNode))
            {
                return TryGoTo(nextNode);
            }

            return false;
        }

        public bool TryGoBack()
        {
            if (_historyStack.Count == 0) return false;
            _currentNode = _historyStack.Pop();
            return true;
        }

        public bool CanGoBack() => _historyStack.Count > 0;
    }
}