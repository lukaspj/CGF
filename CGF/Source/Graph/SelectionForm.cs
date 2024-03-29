﻿using System;
using System.Linq;
using System.Windows.Forms;

namespace Graph
{
	sealed partial class SelectionForm : Form
	{
		public SelectionForm()
		{
			InitializeComponent();
		}


		public int		SelectedIndex { get { return TextComboBox.SelectedIndex; } set { TextComboBox.SelectedIndex = value; } }
		public string[] Items 
		{ 
			get 
			{ 
				return (from item in TextComboBox.Items.OfType<string>() select item).ToArray();
			} 
			set 
			{
				TextComboBox.Items.Clear();
				if (value == null || 
					value.Length == 0)
					return;
				foreach (var item in value)
					TextComboBox.Items.Add(item);
			} 
		}

		private void OnSelectionFormLoad(object sender, EventArgs e)
		{
			TextComboBox.Focus();
		}
	}
}
