namespace B18_Ex05
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;
    using System.Drawing;
    using CheckersLogic;

    public class CheckersGameStarter
    {
        public static void RunGame()
        {
            FormInitializeGame formInitializeGame = new FormInitializeGame();

            if (formInitializeGame.ShowDialog() == DialogResult.OK)
            {
                if (formInitializeGame.FirstPlayerName.Length == 0 || (formInitializeGame.CheckBoxOfPlayer2IsChecked
                    && formInitializeGame.SecondPlayerName.Length == 0))
                {
                    if (MessageBox.Show(
                        "Invalid input",
                        "Please re-enter the name",
                        MessageBoxButtons.RetryCancel,
                        MessageBoxIcon.Error) == DialogResult.Retry)
                    {
                        RunGame();
                    }
                }
                else
                {
                    FormCheckersGame formCheckersGame = new FormCheckersGame(formInitializeGame);
                    formCheckersGame.ShowDialog();
                }
            }
        }
    }
}
