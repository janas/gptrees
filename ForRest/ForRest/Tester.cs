using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ForRest
{
    public partial class Tester : Form
    {
        public Tester()
        {
            InitializeComponent();
        }

        private TreeNode[] nextLevel(ForRest.Provider.BLL.Node<int> node)
        {
            if (node == null)
                return null;
            ForRest.Provider.BLL.NodeList<int> nodeList = node.GetNeighborsList();
            if (nodeList == null)
                return null;
            List<TreeNode> resultList = new List<TreeNode>();
            foreach (ForRest.Provider.BLL.Node<int> n in nodeList)
            {
                if (n == null)
                    continue;
                string print = "";
                for (int i = 0; i < n.Values.Count; i++)
                    print += n.Values[i].ToString() + " ";
                TreeNode[] whatever = nextLevel(n);
                if (whatever == null)
                    resultList.Add(new TreeNode(print));
                else
                    resultList.Add(new TreeNode(print, whatever));
            }
            resultList.RemoveAll(item => item==null);
            TreeNode[] result = new TreeNode[resultList.Count];
            resultList.CopyTo(result);
            return result;
        }

        private void treeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ForRest.BST.BinarySearchTree<int> bst = new ForRest.BST.BinarySearchTree<int>();
            bst.Add(1);
            bst.Add(3);
            bst.Add(-1);
            bst.Add(10);
            bst.Add(9);
            bst.Add(2);
            bst.Add(6);
            bst.Add(4);
            bst.Add(-2);
            bst.Add(0);

            TreeNode tn = null;
            if (nextLevel(bst.Root) == null)
                tn = new TreeNode(bst.Root.Values[0].ToString());
            else
                tn = new TreeNode(bst.Root.Values[0].ToString(), nextLevel(bst.Root));
            treeView1.Nodes.Add(tn);
            treeView1.ExpandAll();
        }
    }
}
