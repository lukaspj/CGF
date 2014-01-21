namespace PostFxUI
{
   partial class PostFxUIForm
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PostFxUIForm));
         Graph.Compatibility.AlwaysCompatible alwaysCompatible3 = new Graph.Compatibility.AlwaysCompatible();
         this.splitContainer1 = new System.Windows.Forms.SplitContainer();
         this.splitContainer2 = new System.Windows.Forms.SplitContainer();
         this.categoryLabel = new System.Windows.Forms.Label();
         this.NodeTreeView = new System.Windows.Forms.TreeView();
         this.toolStrip1 = new System.Windows.Forms.ToolStrip();
         this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
         this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.compileToolStripButton = new System.Windows.Forms.ToolStripButton();
         this.graphControl = new Graph.GraphControl();
         this.dependencyToolStripButton = new System.Windows.Forms.ToolStripButton();
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
         this.splitContainer1.Panel1.SuspendLayout();
         this.splitContainer1.Panel2.SuspendLayout();
         this.splitContainer1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
         this.splitContainer2.Panel1.SuspendLayout();
         this.splitContainer2.Panel2.SuspendLayout();
         this.splitContainer2.SuspendLayout();
         this.toolStrip1.SuspendLayout();
         this.SuspendLayout();
         // 
         // splitContainer1
         // 
         this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.splitContainer1.Location = new System.Drawing.Point(0, 0);
         this.splitContainer1.Name = "splitContainer1";
         // 
         // splitContainer1.Panel1
         // 
         this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
         // 
         // splitContainer1.Panel2
         // 
         this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
         this.splitContainer1.Panel2.Controls.Add(this.graphControl);
         this.splitContainer1.Size = new System.Drawing.Size(1075, 694);
         this.splitContainer1.SplitterDistance = 177;
         this.splitContainer1.TabIndex = 0;
         // 
         // splitContainer2
         // 
         this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
         this.splitContainer2.Location = new System.Drawing.Point(0, 0);
         this.splitContainer2.Name = "splitContainer2";
         this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
         // 
         // splitContainer2.Panel1
         // 
         this.splitContainer2.Panel1.Controls.Add(this.categoryLabel);
         // 
         // splitContainer2.Panel2
         // 
         this.splitContainer2.Panel2.Controls.Add(this.NodeTreeView);
         this.splitContainer2.Size = new System.Drawing.Size(177, 694);
         this.splitContainer2.SplitterDistance = 33;
         this.splitContainer2.TabIndex = 1;
         // 
         // categoryLabel
         // 
         this.categoryLabel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.categoryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.categoryLabel.Location = new System.Drawing.Point(0, 0);
         this.categoryLabel.Name = "categoryLabel";
         this.categoryLabel.Size = new System.Drawing.Size(177, 33);
         this.categoryLabel.TabIndex = 0;
         this.categoryLabel.Text = "Nodes";
         this.categoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // NodeTreeView
         // 
         this.NodeTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
         this.NodeTreeView.Location = new System.Drawing.Point(0, 0);
         this.NodeTreeView.Name = "NodeTreeView";
         this.NodeTreeView.Size = new System.Drawing.Size(177, 657);
         this.NodeTreeView.TabIndex = 0;
         this.NodeTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NodeTreeView_MouseDown);
         // 
         // toolStrip1
         // 
         this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.compileToolStripButton,
            this.dependencyToolStripButton});
         this.toolStrip1.Location = new System.Drawing.Point(0, 0);
         this.toolStrip1.Name = "toolStrip1";
         this.toolStrip1.Size = new System.Drawing.Size(894, 25);
         this.toolStrip1.TabIndex = 1;
         this.toolStrip1.Text = "toolStrip1";
         // 
         // toolStripDropDownButton1
         // 
         this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem});
         this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
         this.toolStripDropDownButton1.Size = new System.Drawing.Size(38, 22);
         this.toolStripDropDownButton1.Text = "File";
         this.toolStripDropDownButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
         // 
         // saveToolStripMenuItem
         // 
         this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
         this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
         this.saveToolStripMenuItem.Text = "Save";
         this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
         // 
         // loadToolStripMenuItem
         // 
         this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
         this.loadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
         this.loadToolStripMenuItem.Text = "Load";
         this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
         // 
         // compileToolStripButton
         // 
         this.compileToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("compileToolStripButton.Image")));
         this.compileToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.compileToolStripButton.Name = "compileToolStripButton";
         this.compileToolStripButton.Size = new System.Drawing.Size(72, 22);
         this.compileToolStripButton.Text = "Compile";
         this.compileToolStripButton.Click += new System.EventHandler(this.compileToolStripButton_Click);
         // 
         // graphControl
         // 
         this.graphControl.AllowDrop = true;
         this.graphControl.CompatibilityStrategy = alwaysCompatible3;
         this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.graphControl.FocusElement = null;
         this.graphControl.HighlightCompatible = true;
         this.graphControl.Location = new System.Drawing.Point(0, 0);
         this.graphControl.Name = "graphControl";
         this.graphControl.ShowLabels = false;
         this.graphControl.Size = new System.Drawing.Size(894, 694);
         this.graphControl.TabIndex = 0;
         this.graphControl.Text = "graphControl";
         // 
         // dependencyToolStripButton
         // 
         this.dependencyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.dependencyToolStripButton.ForeColor = System.Drawing.Color.ForestGreen;
         this.dependencyToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("dependencyToolStripButton.Image")));
         this.dependencyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.dependencyToolStripButton.Name = "dependencyToolStripButton";
         this.dependencyToolStripButton.Size = new System.Drawing.Size(85, 22);
         this.dependencyToolStripButton.Text = "Dependencies";
         this.dependencyToolStripButton.Click += new System.EventHandler(this.dependencyToolStripButton_Click);
         // 
         // PostFxUI
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(1075, 694);
         this.Controls.Add(this.splitContainer1);
         this.Name = "PostFxUI";
         this.Text = "PostFxUI";
         this.splitContainer1.Panel1.ResumeLayout(false);
         this.splitContainer1.Panel2.ResumeLayout(false);
         this.splitContainer1.Panel2.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
         this.splitContainer1.ResumeLayout(false);
         this.splitContainer2.Panel1.ResumeLayout(false);
         this.splitContainer2.Panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
         this.splitContainer2.ResumeLayout(false);
         this.toolStrip1.ResumeLayout(false);
         this.toolStrip1.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.SplitContainer splitContainer1;
      private System.Windows.Forms.SplitContainer splitContainer2;
      private System.Windows.Forms.Label categoryLabel;
      private System.Windows.Forms.TreeView NodeTreeView;
      private System.Windows.Forms.ToolStrip toolStrip1;
      private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
      private Graph.GraphControl graphControl;
      private System.Windows.Forms.ToolStripButton compileToolStripButton;
      private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
      private System.Windows.Forms.ToolStripButton dependencyToolStripButton;
   }
}