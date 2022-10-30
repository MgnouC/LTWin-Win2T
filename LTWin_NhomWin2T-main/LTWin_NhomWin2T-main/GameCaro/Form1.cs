using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCaro
{
    public partial class Form1 : Form
    {
        #region Propertises
        ChessBoardManager ChessBoard;
        #endregion
        public Form1()
        {
            InitializeComponent();
            ChessBoard = new ChessBoardManager(pnlChessBoard, txtPlayerName, pctbMark);
            ChessBoard.EndedGame += ChessBoard_EndedGame;
            ChessBoard.PlayerMarked += ChessBoard_PlayerMarked;

            prcbCoolDown.Step = Cons.COOL_DOWN_STEP;
            prcbCoolDown.Maximum = Cons.COOL_DOWN_TIME;
            prcbCoolDown.Value = 0;
            
            tmCoolDown.Interval = Cons.COOL_DOWN_INTERVAL;

            // ChessBoard.DrawChessBoard();
            NewGame();
        }
        #region Methods
        void EndGame()
        {
            tmCoolDown.Stop();
            undoToolStripMenuItem.Enabled = false;
            pnlChessBoard.Enabled = false;
            MessageBox.Show("Kết thúc" + "\n" + "Người chiến thắng: " + ChessBoard.Player[ChessBoard.CurrentPlayer].Name);
        }
        
       void NewGame()
        {
            prcbCoolDown.Value = 0;
            tmCoolDown.Stop();
            undoToolStripMenuItem.Enabled = true;
            ChessBoard.DrawChessBoard();

        }
        void Quit()
        {
            Application.Exit();

            
        }
        void Undo()
        {
            ChessBoard.Undo();
        }

        void ChessBoard_PlayerMarked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            tmCoolDown.Start();
            prcbCoolDown.Value = 0;
        }

        void ChessBoard_EndedGame(object sender, EventArgs e)
        {
            EndGame();
        }
        private void tmCoolDown_Tick(object sender, EventArgs e)
        {
            prcbCoolDown.PerformStep(); 
            if(prcbCoolDown.Value>= prcbCoolDown.Maximum)
            {
                ChessBoard.CurrentPlayer = ChessBoard.CurrentPlayer == 1 ? 0 : 1;
                EndGame();
            }
            
        }


        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
                e.Cancel = true;
        }

        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
