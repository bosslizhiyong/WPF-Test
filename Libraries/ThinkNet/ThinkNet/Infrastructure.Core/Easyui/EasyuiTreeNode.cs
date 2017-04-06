using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// Easyui树控件节点描述类
    /// </summary>
    public class EasyuiTreeNode
    {
        /// <summary>
        /// 节点标识
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 节点状态【 'open:展开' or 'closed:折叠'】
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// 节点的扩展属性
        /// </summary>
        public Dictionary<string, object> attributes { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        public List<EasyuiTreeNode> children { get; set; }
        /// <summary>
        /// Easyui树控件节点的类
        /// </summary>
        public EasyuiTreeNode()
        {
            id = string.Empty;
            text = string.Empty;
            state = NodeState.open.ToString();
            attributes = new Dictionary<string, object>();
            children = new List<EasyuiTreeNode>();
        }

        /// <summary>
        /// 添加一个子节点
        /// </summary>
        /// <param name="childNode"></param>
        public void AddChildren(EasyuiTreeNode childNode)
        {
            children.Add(childNode);
            state = NodeState.open.ToString();
        }

        /// <summary>
        /// 节点状态
        /// </summary>
        public enum NodeState
        {
            /// <summary>
            /// 展开
            /// </summary>
            open,
            /// <summary>
            /// 折叠
            /// </summary>
            closed
        }
    }
}
