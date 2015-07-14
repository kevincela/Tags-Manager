using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{

    class Tagsmanager
    {
        string[] file;
        public string tags;
        int tagsindex;
        string p;
        int lastindex;

        public Tagsmanager(string path)
        {
            p = path;
            try
            {
                using (StreamReader reader = new StreamReader(p))
                    file = reader.ReadToEnd().Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            }
            catch
            {
                MessageBox.Show("pls");
            }
        }

        public void FindTags()
        {
            int i = 0;
            while (i < file.Length)
            {
                if (file[i].Contains("Tags:"))
                    break;
                else i++;
            }
            tagsindex = i;
            tags = file[i];
            tags = tags.Replace("Tags:", string.Empty);
        }

        public void FindLastIndexString()
        {
            int tempindex = file.Length - 1;
            while (true)
            {
                if (!file[tempindex].Equals(string.Empty))
                {
                    break;
                }
                else tempindex--;
            }
            lastindex = tempindex;
        }

        public void WriteNewTags(string tg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Tags:").Append(tg);
            file[tagsindex] = sb.ToString();
            using (StreamWriter writer = new StreamWriter(p, false))
            {
                for (int i = 0; i <= lastindex; i++)
                {
                    writer.WriteLine(file[i]);
                }
            }
        }
    }
}
