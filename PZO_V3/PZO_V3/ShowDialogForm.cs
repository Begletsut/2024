using System.Windows.Forms;

namespace PZO_V3
{
    public partial class ShowDialogForm : MetroFramework.Forms.MetroForm
    {
        public override string Text { get; set; }
        public string LeftButtonText { get; set; }
        public string RightButtonText { get; set; }
        public bool ShowTextBox { get; set; }
        public ShowDialogForm()
        {
            InitializeComponent();
        }

        public ShowDialogForm(string text, string leftButtonText, string rightButtonText, bool showTextBox)
        {
            InitializeComponent();
            Text = text;
            LeftButtonText = leftButtonText;
            RightButtonText = rightButtonText;
            ShowTextBox = showTextBox; 
        }

        private void btnLeft_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnRight_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void ShowDialogForm_Shown(object sender, System.EventArgs e)
        {
            if (ShowTextBox)
            {
                textBoxInput.Visible = true;
                textBoxInput.Focus();
            }
            labelText.Text = Text;
            btnLeft.Text = LeftButtonText;
            btnRight.Text = RightButtonText;
        }
    }
}
