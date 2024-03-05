using ScintillaNET;
using System;
using System.IO;
using System.Windows.Forms;

namespace Simpl2
{
    public partial class Simpl : Form
    {
        private Scintilla scintilla = new Scintilla();
        // Main entry point for the application
        public Simpl()
        {
            //initialize the form
            InitializeComponent();

            //initialize the ScintillaNET control
            InitializeScintillaNET();

            treeViewSidebar.BeforeExpand += treeViewSidebar_BeforeExpand;

            // Wire up the Click event handlers for menu items
            this.newFileToolStripMenuItem.Click += new EventHandler(this.newFileToolStripMenuItem_Click);
            this.openFileToolStripMenuItem.Click += new EventHandler(this.openFileToolStripMenuItem_Click);
            this.openFolderToolStripMenuItem.Click += new EventHandler(this.openFolderToolStripMenuItem_Click);
            this.exitToolStripMenuItem.Click += new EventHandler(this.exitToolStripMenuItem_Click);


            // Set form colors
            ForeColor = System.Drawing.Color.Black;
            BackColor = System.Drawing.Color.White;

        }

        // Event handler for creating a new file
        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File (*.txt)|*.txt|All files (*.*)|*.*";

            // Show the Save File dialog. If the user clicks OK, create a new file.
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Create and open the file for writing. If it already exists, it will be overwritten.
                using (StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName, false))
                {
                    // You can initialize the file with some content if needed, e.g., streamWriter.Write("");
                }

                // Open the file for editing in richTextBoxMain.
                richTextBoxMain.Text = File.ReadAllText(saveFileDialog.FileName);
                richTextBoxMain.Tag = saveFileDialog.FileName;  // Store the file path in Tag for later use.

                // Optionally, set the form's Text property to the name of the file.
                this.Text = $"Editing {Path.GetFileName(saveFileDialog.FileName)}";
            }
        }

        // Event handler for opening an existing file
        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string content = File.ReadAllText(openFileDialog.FileName);
                // Assuming you have a TextBox or some control to display file content
                // textBoxFileContent.Text = content;
                MessageBox.Show("File opened: " + openFileDialog.FileName);
            }
        }

        // Event handler for opening a folder
        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    treeViewSidebar.Nodes.Clear(); // Clear existing items
                    LoadDirectoryIntoTreeView(fbd.SelectedPath);
                }
            }
        }

        // Event handler for closing the application
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Method to populate TreeView with directory contents
        //private void LoadDirectoryIntoTreeView(string selectedPath)
        //{
        //    DirectoryInfo directoryInfo = new DirectoryInfo(selectedPath);
        //    TreeNode rootNode = treeViewSidebar.Nodes.Add(directoryInfo.Name);
        //    foreach (var directory in directoryInfo.GetDirectories())
        //    {
        //        TreeNode dirNode = rootNode.Nodes.Add(directory.Name);
        //        // Optionally, call a recursive function to load subdirectories
        //    }
        //    foreach (var file in directoryInfo.GetFiles())
        //    {
        //        rootNode.Nodes.Add(file.Name);
        //    }
        //    rootNode.Expand();
        //}

        private void LoadDirectoryIntoTreeView(string selectedPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(selectedPath);
            TreeNode rootNode = treeViewSidebar.Nodes.Add(directoryInfo.Name);
            rootNode.Tag = directoryInfo; // Tag the node with the directory info
            LoadSubDirectories(rootNode); // Add the subdirectories
        }

        private void LoadSubDirectories(TreeNode node)
        {
            DirectoryInfo directoryInfo = (DirectoryInfo)node.Tag;
            foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
            {
                TreeNode dirNode = node.Nodes.Add(directory.Name);
                dirNode.Tag = directory; // Tag the node with the directory info
                dirNode.Nodes.Add("dummy"); // Add a dummy node so expand sign is shown
            }
        }

        private void treeViewSidebar_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode expandedNode = e.Node;

            if (expandedNode.Nodes.Count == 1 && expandedNode.Nodes[0].Text == "dummy")
            {
                // Remove the dummy node that was added earlier
                expandedNode.Nodes.Clear();

                // Load the subdirectories and files for the expanded node
                LoadSubDirectories(expandedNode);
                LoadFiles(expandedNode);
            }
        }

        private void LoadFiles(TreeNode node)
        {
            DirectoryInfo directoryInfo = (DirectoryInfo)node.Tag;
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                TreeNode fileNode = node.Nodes.Add(file.Name);
                fileNode.Tag = file; // Tag the node with the file info
            }
        }

        private void treeViewSidebar_AfterSelect(object sender, TreeViewEventArgs e)
        {
            treeViewSidebar.SelectedNode = null;

        }


        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("File Clicked");
        }


        // Event handler for the 'Edit' menu item click
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Logic for when 'Edit' is clicked
        }

        // Event handler for the 'View' menu item click
        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Logic for when 'View' is clicked
        }

        // Event handler for the 'Help' menu item click
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Logic for when 'Help' is clicked
        }


    }
}