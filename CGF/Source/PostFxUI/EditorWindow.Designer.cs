namespace PostFxUI
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
         this.SaveButton = new System.Windows.Forms.Button();
         this.LoadButton = new System.Windows.Forms.Button();
         this.CompileButton = new System.Windows.Forms.Button();
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
         this.showLabelsCheckBox.Location = new System.Drawing.Point(12, 519);
         this.showLabelsCheckBox.Name = "showLabelsCheckBox";
         this.showLabelsCheckBox.Size = new System.Drawing.Size(87, 17);
         this.showLabelsCheckBox.TabIndex = 7;
         this.showLabelsCheckBox.Text = "Show Labels";
         this.showLabelsCheckBox.UseVisualStyleBackColor = true;
         // 
         // flowLayoutPanel1
         // 
         this.flowLayoutPanel1.Location = new System.Drawing.Point(15, 28);
         this.flowLayoutPanel1.Name = "flowLayoutPanel1";
         this.flowLayoutPanel1.Size = new System.Drawing.Size(81, 485);
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
         this.graphControl.Size = new System.Drawing.Size(564, 524);
         this.graphControl.TabIndex = 0;
         this.graphControl.Text = "graphControl1";
         // 
         // SaveButton
         // 
         this.SaveButton.Location = new System.Drawing.Point(679, 12);
         this.SaveButton.Name = "SaveButton";
         this.SaveButton.Size = new System.Drawing.Size(75, 23);
         this.SaveButton.TabIndex = 10;
         this.SaveButton.Text = "Save";
         this.SaveButton.UseVisualStyleBackColor = true;
         this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
         // 
         // LoadButton
         // 
         this.LoadButton.Location = new System.Drawing.Point(679, 41);
         this.LoadButton.Name = "LoadButton";
         this.LoadButton.Size = new System.Drawing.Size(75, 23);
         this.LoadButton.TabIndex = 11;
         this.LoadButton.Text = "Load";
         this.LoadButton.UseVisualStyleBackColor = true;
         this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
         // 
         // CompileButton
         // 
         this.CompileButton.Location = new System.Drawing.Point(679, 70);
         this.CompileButton.Name = "CompileButton";
         this.CompileButton.Size = new System.Drawing.Size(75, 23);
         this.CompileButton.TabIndex = 12;
         this.CompileButton.Text = "Compile";
         this.CompileButton.UseVisualStyleBackColor = true;
         this.CompileButton.Click += new System.EventHandler(this.CompileButton_Click);
         // 
         // EditorWindow
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(764, 548);
         this.Controls.Add(this.CompileButton);
         this.Controls.Add(this.LoadButton);
         this.Controls.Add(this.SaveButton);
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
      private System.Windows.Forms.Button SaveButton;
      private System.Windows.Forms.Button LoadButton;
      private System.Windows.Forms.Button CompileButton;
   }
}

