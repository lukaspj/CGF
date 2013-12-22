namespace WLEShaderEditor
{
   partial class EditorWindow
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
         Graph.Compatibility.AlwaysCompatible alwaysCompatible1 = new Graph.Compatibility.AlwaysCompatible();
         this.label2 = new System.Windows.Forms.Label();
         this.showLabelsCheckBox = new System.Windows.Forms.CheckBox();
         this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
         this.graphControl = new Graph.GraphControl();
         this.SuspendLayout();
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.label2.Location = new System.Drawing.Point(12, 12);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(84, 13);
         this.label2.TabIndex = 8;
         this.label2.Text = "drag&drop nodes:";
         // 
         // showLabelsCheckBox
         // 
         this.showLabelsCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.showLabelsCheckBox.AutoSize = true;
         this.showLabelsCheckBox.Location = new System.Drawing.Point(12, 354);
         this.showLabelsCheckBox.Name = "showLabelsCheckBox";
         this.showLabelsCheckBox.Size = new System.Drawing.Size(87, 17);
         this.showLabelsCheckBox.TabIndex = 7;
         this.showLabelsCheckBox.Text = "Show Labels";
         this.showLabelsCheckBox.UseVisualStyleBackColor = true;
         this.showLabelsCheckBox.CheckedChanged += new System.EventHandler(this.showLabelsCheckBox_CheckedChanged);
         // 
         // flowLayoutPanel1
         // 
         this.flowLayoutPanel1.Location = new System.Drawing.Point(15, 28);
         this.flowLayoutPanel1.Name = "flowLayoutPanel1";
         this.flowLayoutPanel1.Size = new System.Drawing.Size(81, 320);
         this.flowLayoutPanel1.TabIndex = 9;
         // 
         // graphControl
         // 
         this.graphControl.AllowDrop = true;
         this.graphControl.CompatibilityStrategy = alwaysCompatible1;
         this.graphControl.FocusElement = null;
         this.graphControl.HighlightCompatible = true;
         this.graphControl.Location = new System.Drawing.Point(109, 12);
         this.graphControl.Name = "graphControl";
         this.graphControl.ShowLabels = false;
         this.graphControl.Size = new System.Drawing.Size(419, 359);
         this.graphControl.TabIndex = 0;
         this.graphControl.Text = "graphControl1";
         // 
         // EditorWindow
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(540, 383);
         this.Controls.Add(this.flowLayoutPanel1);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.showLabelsCheckBox);
         this.Controls.Add(this.graphControl);
         this.Name = "EditorWindow";
         this.Text = "Form1";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private Graph.GraphControl graphControl;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.CheckBox showLabelsCheckBox;
      private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
   }
}

