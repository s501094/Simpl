using System;
using System.IO;
using System.Windows.Forms;

namespace Simpl2
{
    public partial class Simpl : Form
    {
        // Main entry point for the application
        public Simpl()
        {
            InitializeComponent();
            // Additional initialization can go here

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
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, "");
                MessageBox.Show("New file created: " + saveFileDialog.FileName);
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
        private void LoadDirectoryIntoTreeView(string selectedPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(selectedPath);
            TreeNode rootNode = treeViewSidebar.Nodes.Add(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
            {
                TreeNode dirNode = rootNode.Nodes.Add(directory.Name);
                // Optionally, call a recursive function to load subdirectories
            }
            foreach (var file in directoryInfo.GetFiles())
            {
                rootNode.Nodes.Add(file.Name);
            }
            rootNode.Expand();
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