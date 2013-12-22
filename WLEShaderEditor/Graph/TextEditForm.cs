using System.Windows.Forms;

namespace Graph
{
	sealed partial class TextEditForm : Form
	{
		public TextEditForm()
		{
			InitializeComponent();
		}

		public string InputText { get { return TextTextBox.Text; } set { TextTextBox.Text = value; } }
	}
}
