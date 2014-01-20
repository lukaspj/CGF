namespace WLEShaderEditor
{
   partial class FileDependencyWindow
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.DependencyGridView = new System.Windows.Forms.DataGridView();
         this.openDependencyFileDialog = new System.Windows.Forms.OpenFileDialog();
         this.FileNameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.PathCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
         ((System.ComponentModel.ISupportInitialize)(this.DependencyGridView)).BeginInit();
         this.SuspendLayout();
         // 
         // DependencyGridView
         // 
         this.DependencyGridView.AllowUserToAddRows = false;
         this.DependencyGridView.AllowUserToDeleteRows = false;
         this.DependencyGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.DependencyGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileNameCol,
            this.PathCol});
         this.DependencyGridView.Dock = System.Windows.Forms.DockStyle.Fill;
         this.DependencyGridView.Location = new System.Drawing.Point(0, 0);
         this.DependencyGridView.Name = "DependencyGridView";
         this.DependencyGridView.ReadOnly = true;
         this.DependencyGridView.Size = new System.Drawing.Size(398, 375);
         this.DependencyGridView.TabIndex = 0;
         this.DependencyGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DependencyGridView_CellDoubleClick);
         // 
         // FileNameCol
         // 
         this.FileNameCol.HeaderText = "FileName";
         this.FileNameCol.Name = "FileNameCol";
         this.FileNameCol.ReadOnly = true;
         // 
         // PathCol
         // 
         this.PathCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
         this.PathCol.HeaderText = "Path";
         this.PathCol.Name = "PathCol";
         this.PathCol.ReadOnly = true;
         // 
         // FileDependencyWindow
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(398, 375);
         this.Controls.Add(this.DependencyGridView);
         this.Name = "FileDependencyWindow";
         this.Text = "FileDependencyWindow";
         ((System.ComponentModel.ISupportInitialize)(this.DependencyGridView)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.DataGridView DependencyGridView;
      private System.Windows.Forms.OpenFileDialog openDependencyFileDialog;
      private System.Windows.Forms.DataGridViewTextBoxColumn FileNameCol;
      private System.Windows.Forms.DataGridViewTextBoxColumn PathCol;


   }
}