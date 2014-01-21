using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PostFxUI
{
   public partial class FileDependencyWindow : Form
   {
      private string mRelFileName;

      public FileDependencyWindow(Dictionary<string, string> FileNames, string relFileName)
      {
         string AppPath = Path.GetDirectoryName(Application.ExecutablePath);
         mRelFileName = AppPath + "\\" + relFileName;
         InitializeComponent();
         foreach (var pair in FileNames)
         {
            DependencyGridView.Rows.Add(pair.Key, pair.Value);
         }
         openDependencyFileDialog.InitialDirectory = AppPath;
      }

      private void DependencyGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
      {
         string fileName = DependencyGridView[0, e.RowIndex].Value.ToString();
         openDependencyFileDialog.FileName = fileName;
         DialogResult result = openDependencyFileDialog.ShowDialog();
         if (result == DialogResult.OK)
         {
            System.Uri uri1 = new Uri(mRelFileName);
            System.Uri uri2 = new Uri(openDependencyFileDialog.FileName);

            Uri relativeUri = uri1.MakeRelativeUri(uri2);

            DependencyGridView[1, e.RowIndex].Value = relativeUri;
         }
      }

      public Dictionary<string, string> GetDependencyList()
      {
         Dictionary<string, string> retDict = new Dictionary<string, string>();
         foreach(DataGridViewRow row in DependencyGridView.Rows)
         {
            retDict.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString());
         }
         return retDict;
      }
   }
}
