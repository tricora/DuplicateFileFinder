using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();

            folderBrowserDialog1.SelectedPath = Properties.Settings.Default.InputPath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ( folderBrowserDialog1.ShowDialog() == DialogResult.OK )
            {
                treeView.Nodes.Clear();

                string path = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.InputPath = path;
                Properties.Settings.Default.Save();

                FileComperator fc = new FileComperator(path);
                var res = fc.run();

                foreach( var list in res )
                {
                    var node = new TreeNode(string.Format("{0} Bytes", list[0].Length));
                    foreach ( var handle in list )
                    {
                        var fileNode = new TreeNode(Path.GetFileName(handle.Path));
                        fileNode.Tag = handle;
                        node.Nodes.Add(fileNode);
                    }
                    treeView.Nodes.Add(node);
                }
                

                Console.WriteLine("\ndone.");
            }
        }

        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var handle = (FileHandle) treeView.SelectedNode.Tag;
            if ( handle == null )
            {
                return;
            }
            if ( !File.Exists( handle.Path ))
            {
                MessageBox.Show("file not found!");
                return;
            }
            System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", handle.Path));
            
        }
    }
}
