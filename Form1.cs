using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Dictionary<String, Tagsmanager> dict;
        KeyValuePair<String, Tagsmanager> selectedEntry;

        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
            button4.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        public void button3_Click(object sender, EventArgs e)
        {
            dict = new Dictionary<String, Tagsmanager>();
            string[] txtFiles = Directory.EnumerateFiles(textBox1.Text, "*.osu").ToArray();
            foreach (string path in txtFiles)
            {
                dict.Add(Path.GetFileName(path), new Tagsmanager(path));
            }
            comboBox1.DataSource = new BindingSource(dict, null);
            comboBox1.DisplayMember = "Key";
            comboBox1.ValueMember = "Value";
            button2.Enabled = true;
            button4.Enabled = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedEntry = (KeyValuePair<String, Tagsmanager>)comboBox1.SelectedItem;
            selectedEntry.Value.FindTags();
            textBox2.Text = selectedEntry.Value.tags;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            selectedEntry.Value.FindLastIndexString();
            selectedEntry.Value.WriteNewTags(textBox2.Text);
            MessageBox.Show("Tags changed!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach(KeyValuePair<String, Tagsmanager> current in dict)
            {
                current.Value.FindTags();
                current.Value.FindLastIndexString();
                current.Value.WriteNewTags(textBox2.Text);
            }
            MessageBox.Show("Tags changed for all the difficulties!");
        }
    }
}
