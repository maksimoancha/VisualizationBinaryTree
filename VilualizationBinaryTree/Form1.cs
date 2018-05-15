using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLinkedList;

namespace VilualizationBinaryTree
{
    public partial class MainForm : Form
    {
        private MyBinaryTree<int> _binaryTree = null;
        private List<int> _listValues = null;
        private int _levelDepth = 0;
        private int _heightDepth = 50;
        private int _span = 20;
        


        public MainForm()
        {
            InitializeComponent();
            
            _binaryTree = new MyBinaryTree<int>();
            _binaryTree.Add(50);
            _binaryTree.Add(25);
            _binaryTree.Add(70);
            _binaryTree.Add(15);
            _binaryTree.Add(44);
            _binaryTree.Add(85);
            _binaryTree.Add(71);
            _binaryTree.Add(2);
            _binaryTree.Add(60);
            _binaryTree.Add(30);
            _binaryTree.Add(45);
            _binaryTree.Add(18);
            _binaryTree.Add(4);
            _binaryTree.Add(16);
            _binaryTree.Add(99);
            _binaryTree.Add(86);
            _binaryTree.Add(100);
            _binaryTree.Add(55);
            _binaryTree.Add(65);
            
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Draw a stencil lines
            for (int i = 0; i < 10; ++i)
            {
                float[] dashValues = { 5, 2, 15, 4 };
                Pen blackPen = new Pen(Color.Gray, 1);
                blackPen.DashPattern = dashValues;

                e.Graphics.DrawLine(blackPen,
                    0,
                    _heightDepth * i + 10,
                    ClientSize.Width,
                    _heightDepth * i + 10
                );
            }

            _listValues = new List<int>();
            foreach (var val in _binaryTree)
            {
                _listValues.Add(val);
            }

            //Draw a binary tree
            _levelDepth = 0;
            DrawBinaryTree(e.Graphics);
        }

        private void DrawBinaryTree(Graphics gr)
        {
            DrawNodeAndRibs(@gr, _binaryTree.RootItem);
            ++_levelDepth;
            List<BinaryTreeItem<int>> list = new List<BinaryTreeItem<int>>();
            list.Add(_binaryTree.RootItem);
            int drawnNodes = 1;
            while (drawnNodes < _listValues.Count)
            {
                foreach (var item in list) //draw all nodes at this level
                {
                    if (item.Left != null)
                        DrawNodeAndRibs(gr, item.Left);
                    if (item.Rigth != null)
                        DrawNodeAndRibs(gr, item.Rigth);
                }

                int count = list.Count; //fix count of items at current level of Depth

                for (int i = 0; i < count; i++) //add to list all items of next level Depth
                {
                    if (list[i].Left != null)
                    {
                        list.Add(list[i].Left);
                        drawnNodes++;
                    }
                    if (list[i].Rigth != null)
                    {
                        list.Add(list[i].Rigth);
                        drawnNodes++;
                    }
                }
                list.RemoveRange(0, count); //remove from list all items current level of Depth
                ++_levelDepth;//increment level of Depth
            }
        }

        private void DrawNodeAndRibs(Graphics gr, BinaryTreeItem<int> node)
        {
            if (node.Left != null)
            {
                Pen myPen = new Pen(Color.Red, 4);
                gr.DrawLine(myPen,
                    _span * _listValues.IndexOf(node.Value) + 10,
                    _levelDepth * _heightDepth + 10,
                    _span * _listValues.IndexOf(node.Left.Value) + 10,
                    (_levelDepth + 1) * _heightDepth + 10
                );
            }

            if (node.Rigth != null)
            {
                Pen myPen = new Pen(Color.Red, 4);
                gr.DrawLine(myPen,
                    _span * _listValues.IndexOf(node.Value) + 10,
                    _levelDepth * _heightDepth + 10,
                    _span * _listValues.IndexOf(node.Rigth.Value) + 10,
                    (_levelDepth + 1) * _heightDepth + 10
                );
            }

            gr.FillEllipse(
                new SolidBrush(Color.Green),
                (_span * _listValues.IndexOf(node.Value)-10),
                _levelDepth * _heightDepth-10,
                40, 40
            );

            gr.DrawString(
                node.Value.ToString(),
                this.Font, 
                new SolidBrush(Color.White),
                (_span * _listValues.IndexOf(node.Value)+10F) - gr.MeasureString(node.Value.ToString(), this.Font).Width/2F,
                (_levelDepth * _heightDepth+10) - (int)gr.MeasureString(node.Value.ToString(), this.Font).Height/2
            );
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void btnAddValue_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(txbValue.Text, out int newValue))
            {
                _binaryTree.Add(newValue);
                Invalidate();
            }
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(txbValue.Text, out int value))
            {
                _binaryTree.RemoveByValue(value);
                Invalidate();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _span += 5;
            Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_span > 20)
                _span -= 5;
            Invalidate();
        }
    }
}
