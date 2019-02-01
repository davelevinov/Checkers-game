namespace B18_Ex05
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;
    using System.Drawing;

    public class FormInitializeGame : Form
    {
        private TextBox m_TextBoxPlayer1Name = new TextBox();
        private TextBox m_TextBoxPlayer2Name = new TextBox();
        private Label m_BoardSizeLabel = new Label();
        private Label m_LabelPlayers = new Label();
        private Label m_LabelPlayer1 = new Label();
        private Label m_LabelPlayer2 = new Label();
        private Button m_ButtonDone = new Button();
        private RadioButton m_RadioButtonBoardSize6X6 = new RadioButton();
        private RadioButton m_RadioButtonBoardSize8X8 = new RadioButton();
        private RadioButton m_RadioButtonBoardSize10X10 = new RadioButton();
        private CheckBox m_CheckBoxEnablePlayer2 = new CheckBox();

        public FormInitializeGame()
        {
            this.Size = new Size(300, 250);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Game Settings";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            initializeContols();
        }

        private void initializeContols()
        {
            m_BoardSizeLabel.Text = "Board Size:";
            m_BoardSizeLabel.Location = new Point(25, 20);
            m_RadioButtonBoardSize6X6.Text = "6 x 6";
            m_RadioButtonBoardSize6X6.Location = new Point(m_BoardSizeLabel.Left, m_BoardSizeLabel.Height + 15);
            m_RadioButtonBoardSize8X8.Text = "8 x 8";
            m_RadioButtonBoardSize8X8.Location = new Point(m_RadioButtonBoardSize6X6.Right, m_BoardSizeLabel.Height + 15);
            m_RadioButtonBoardSize6X6.Checked = true;
            m_RadioButtonBoardSize10X10.Text = "10 x 10";
            m_RadioButtonBoardSize10X10.Location = new Point(m_RadioButtonBoardSize8X8.Right, m_BoardSizeLabel.Height + 15);
            m_LabelPlayers.Text = "Players:";
            m_LabelPlayers.Location = new Point(m_BoardSizeLabel.Left, m_RadioButtonBoardSize6X6.Height + 40);
            m_LabelPlayer1.Text = "Player1:";
            m_LabelPlayer1.Location = new Point(m_LabelPlayers.Left + 10, m_LabelPlayers.Height + 65);
            m_TextBoxPlayer1Name.Location = new Point(m_LabelPlayer1.Right, m_LabelPlayers.Height + 65);
            m_CheckBoxEnablePlayer2.Checked = false;
            m_CheckBoxEnablePlayer2.Location = new Point(m_LabelPlayer1.Left, m_LabelPlayer1.Height + 90);
            m_CheckBoxEnablePlayer2.Size = new Size(20, 20);
            m_LabelPlayer2.Text = "Player2:";
            m_LabelPlayer2.Location = new Point(m_CheckBoxEnablePlayer2.Right, m_LabelPlayer1.Height + 90);
            m_LabelPlayer2.Size = new Size(60, 20);
            m_TextBoxPlayer2Name.Location = new Point(m_TextBoxPlayer1Name.Left, m_LabelPlayer1.Height + 90);
            m_TextBoxPlayer2Name.Enabled = false;
            m_TextBoxPlayer2Name.Text = "[Computer]";
            m_TextBoxPlayer1Name.MaxLength = 8;
            m_TextBoxPlayer2Name.MaxLength = 8;
            m_ButtonDone.Location = new Point(this.ClientSize.Width - 100, this.ClientSize.Height - 50);
            m_ButtonDone.Text = "Done";
            Controls.AddRange(
                new Control[] 
                {
                m_BoardSizeLabel,
                m_RadioButtonBoardSize6X6,
                m_RadioButtonBoardSize8X8, 
                m_RadioButtonBoardSize10X10,
                m_LabelPlayers,
                m_LabelPlayer1,
                m_TextBoxPlayer1Name,   
                m_CheckBoxEnablePlayer2,
                m_LabelPlayer2, 
                m_TextBoxPlayer2Name,
                m_ButtonDone 
            });
            m_ButtonDone.Click += new EventHandler(m_ButtonDone_Click);
            m_CheckBoxEnablePlayer2.Click += new EventHandler(m_CheckBox_Click);
        }

        private void m_ButtonDone_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void m_CheckBox_Click(object sender, EventArgs e)
        {
            m_TextBoxPlayer2Name.Enabled = true;
            m_TextBoxPlayer2Name.Text = string.Empty;
        }

        public string FirstPlayerName
        {
            get { return m_TextBoxPlayer1Name.Text; }
            set { m_TextBoxPlayer1Name.Text = value; }
        }

        public string SecondPlayerName
        {
            get
            {
                return m_TextBoxPlayer2Name.Text;
            }

            set
            {
                m_TextBoxPlayer2Name.Text = value;
            }
        }

        public bool CheckBoxOfPlayer2IsChecked
        {
            get
            {
                return m_CheckBoxEnablePlayer2.Checked;
            }
        }

        public bool RadioButtonBoardSize6X6IsChecked
        {
            get
            {
                return m_RadioButtonBoardSize6X6.Checked;
            }
        }

        public bool RadioButtonBoardSize8X8IsChecked
        {
            get
            {
                return m_RadioButtonBoardSize8X8.Checked;
            }
        }

        public bool RadioButtonBoardSize10X10IsChecked
        {
            get
            {
                return m_RadioButtonBoardSize10X10.Checked;
            }
        }

        public bool CheckBoxOfSecondPlayerIsChecked
        {
            get
            {
                return m_CheckBoxEnablePlayer2.Checked;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // FormInitializeGame
            this.ClientSize = new System.Drawing.Size(292, 212);
            this.Name = "FormInitializeGame";
            this.ShowInTaskbar = false;
            this.UseWaitCursor = true;
            this.ResumeLayout(false);
        }
    }
}
