using _Project.Develop.Runtime.Data.Characters;
using _Project.Develop.Runtime.Data.Dialogs.Nodes;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System;
using Sirenix.OdinInspector.Editor;

namespace _Project.Develop.Runtime.Data.Dialogs
{
    [CreateAssetMenu(menuName = "Dialog/Dialog Config")]
    public class DialogConfig : SerializedScriptableObject
    {
        [Required]
        [LabelText("Ссылка на конфиг персонажей")]
        [SerializeField] private CharactersConfig _charactersConfig;

        [ShowIf(nameof(HasCharactersConfig))]
        [SerializeReference]
        [ListDrawerSettings(Expanded = true, DraggableItems = true, ShowPaging = false)]
        [LabelText("Диалоги")]
        [TypeFilter("GetNodeTypes")]
        [OnValueChanged(nameof(InitConfig), IncludeChildren = true)]
        [OnCollectionChanged(nameof(OnNodesCollectionChanged))]
        [SerializeField] private List<DialogNode> _nodes = new();

        public List<DialogNode> Nodes => _nodes;

#if UNITY_EDITOR
        private void InitConfig()
        {
            foreach (var node in _nodes)
            {
                node.SetConfig(_charactersConfig);
                node.SetAvailableNodes(_nodes);
            }
        }

        private void OnNodesCollectionChanged(CollectionChangeInfo info)
        {
            if (info.ChangeType == CollectionChangeType.Add)
            {
                var newNode = _nodes[^1];
                string newId = Guid.NewGuid().ToString().Substring(0, 8);

                if (_nodes.Count >= 2)
                {
                    var prevNode = _nodes[^2];

                    UnityEditor.EditorUtility.CopySerializedManagedFieldsOnly(prevNode, newNode);

                    if (prevNode is SimpleNode simpleNode)
                        simpleNode.NextNodeId = newId;
                }

                newNode.Id = newId;
            }
        }

        private static IEnumerable<Type> GetNodeTypes()
        {
            yield return typeof(SimpleNode);
            yield return typeof(ChoiceNode);
        }

        private bool HasCharactersConfig() => _charactersConfig != null;
#endif

    }
}