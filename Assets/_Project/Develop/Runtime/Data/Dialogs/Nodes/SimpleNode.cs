using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;

namespace _Project.Develop.Runtime.Data.Dialogs.Nodes
{
    [System.Serializable]
    public class SimpleNode : DialogNode
    {
        [UnityEngine.SerializeReference]
        [LabelText("Следующая нода")]
        [ValueDropdown(nameof(GetAvailableNodes))]
        public string NextNodeId = string.Empty;

#if UNITY_EDITOR
        public IEnumerable<ValueDropdownItem<string>> GetAvailableNodes()
        {
            var freeNodes = _allNodes?
                .Where(n => n != this)
                .Select(n => new ValueDropdownItem<string>(n.Id, n.Id))
                ?? Enumerable.Empty<ValueDropdownItem<string>>();


            var availableNodes = new List<ValueDropdownItem<string>>();
            availableNodes.Add(new ValueDropdownItem<string>("null", string.Empty));
            availableNodes.AddRange(freeNodes);

            return availableNodes;
        }
#endif
    }
}