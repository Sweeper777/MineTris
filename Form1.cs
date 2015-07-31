using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MineTris.Properties;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using GameEssentials;

namespace MineTris {
    public partial class TetrisWindow : Form {
        private const int DEFAULT_TIMER_INTERVAL = 1500;
        public TetrisBoard board;
        public Tetrimino fallingTetrimino;
        public bool isPaused = false;
        int _totalScore;
        public int timerInterval = DEFAULT_TIMER_INTERVAL;

        int totalScore {
            get { return _totalScore; }
            set { 
                _totalScore = value;
                score.Text = totalScore.ToString ().PadLeft (8, '0');

                double doubleValue = value;
                timerInterval = (int)( DEFAULT_TIMER_INTERVAL - 
                    (Math.Floor (doubleValue / 150.0) ) * 50);
            }
        }
        int Highscore {
            get {
                try {
                    return GameDataSerializer<int>.Deserialize ("MineTris");
                } catch (IOException) {
                    return 0;
                }
            }
            set {
                GameDataSerializer<int>.Serialize (value, "MineTris");
                highscore.Text = value.ToString ().PadLeft (8, '0');
            }
        }

        public TetrisWindow () {
            InitializeComponent ();
            board = new TetrisBoard (this);
            pausedPic.Visible = false;
            highscore.Text = Highscore.ToString ().PadLeft (8, '0');
        }

        private void button1_Click (object sender, EventArgs e) {
            board.AddTetrimino ();
            button1.Enabled = false;
        }

        private void TetrisWindow_KeyDown (object sender, KeyEventArgs e) {

            if (button1.Enabled) {
                return;
            }

            if (e.KeyCode == Keys.D && !isPaused) {
                fallingTetrimino.Move (1);
            } else if (e.KeyCode == Keys.A && !isPaused) {
                fallingTetrimino.Move (-1);
            } else if (e.KeyCode == Keys.W && !isPaused) {
                if (fallingTetrimino is IRotatable) {
                    ( (IRotatable)fallingTetrimino ).Rotate ();
                }
            } else if (e.KeyCode == Keys.S && !isPaused) {
                fallingTetrimino.MoveDown ();
            }
        }

        public void ResetGame () {
            for (int i = 0 ; i < 16 ; i++) {
                for (int k = 0 ; k < 22 ; k++) {
                    board.tetrisBlockMatrix[i, k] = null;
                }
            }

            List<Control> controlsToRemove = new List<Control> ();
            foreach (Control c in panel1.Controls) {
                if (c.Name == "TetrisBlock") {
                    controlsToRemove.Add (c);
                }
            }
            foreach (Control c in controlsToRemove) {
                panel1.Controls.Remove (c);
            }

            fallingTetrimino.Dispose ();
            fallingTetrimino = null;
            button1.Enabled = true;
            if (totalScore > Highscore) {
                Highscore = totalScore;
            }
            totalScore = 0;
        }

        public void CheckScore () {
            int scoreCount = 0;
            List<int> yCoords;
            List<PictureBox> picturesToRemove = new List<PictureBox> ();

            yCoords = FindYCoords (out scoreCount, picturesToRemove);
            totalScore += CountScore (scoreCount, yCoords);
            RemovePictureBoxes (yCoords);
            MoveOtherTerisBlocksDown (yCoords);
        }
        
        private int CountScore (int scoreCount, List<int> yCoords) {
            int score = 2 * scoreCount * scoreCount;
            int bonus = 0;

            for (int i = 0 ; i < yCoords.Count ; i++) {
                bonus = 0;
                for (int j = 0 ; j < 16 ; j++) {
                    TetrisBlock currentBlock = board.tetrisBlockMatrix[j, yCoords[i]];
                    if (!currentBlock.HasMine && currentBlock.HasFlag) {
                        bonus = 0;
                        break;
                    }

                    if (currentBlock.HasMine && currentBlock.HasFlag) {
                        bonus += 20;
                    }
                }

                score += bonus;
            }
            
            return score;
        }

        private void MoveOtherTerisBlocksDown (List<int> yCoords) {
            for (int i = 0 ; i < yCoords.Count ; i++) {
                List<TetrisBlock> blocksToCustomTetrimino = new List<TetrisBlock> ();
                foreach (var block in board.tetrisBlockMatrix) {
                    if (block == null) {
                        continue;
                    }
                    if (block.Y < yCoords[i]) {
                        blocksToCustomTetrimino.Add (block);
                    }
                }
                CustomTetrimino custom = new CustomTetrimino (blocksToCustomTetrimino, board.tetrisBlockMatrix);
                custom.MoveDown ();
                custom.AddTetrisBlocksToMatrix (board.tetrisBlockMatrix);
            }
        }

        private void RemovePictureBoxes (List<int> yCoords) {
            foreach (int i in yCoords) {
                for (int k = 0 ; k < 16 ; k++) {
                    panel1.Controls.Remove (board.tetrisBlockMatrix[k, i].picture);
                    board.tetrisBlockMatrix[k, i] = null;
                }
            }
        }

        private List<int> FindYCoords (out int scoreCount, List<PictureBox> picturesToRemove) {
            scoreCount = 0;
            List<int> yCoords = new List<int> ();

            for (int i = 0, k = 0 ; i < 22 ; i++) {
                for (k = 0 ; k < 16 ; k++) {
                    if (board.tetrisBlockMatrix[k, i] == null || 
                        !board.tetrisBlockMatrix[k, i].IsCorrect) {
                        break;
                    } else {
                        picturesToRemove.Add (board.tetrisBlockMatrix[k, i].picture);
                    }
                }

                if (k == 16) {
                    scoreCount++;
                    yCoords.Add (i);
                }
            }

            return yCoords;
        }

        private void button2_Click (object sender, EventArgs e) {
            if (!isPaused && fallingTetrimino != null) {
                fallingTetrimino.StopTimer ();
                button2.BackgroundImage = Resources.resume;
                pausedPic.Visible = true;
                isPaused = true;
            } else if (isPaused && fallingTetrimino != null) {
                fallingTetrimino.StartTimer ();
                button2.BackgroundImage = Resources.pause;
                pausedPic.Visible = false;
                isPaused = false;
            }
        }

        private void TetrisWindow_Deactivate (object sender, EventArgs e) {
            if (!isPaused) {
                button2_Click (sender, e);
            }
        }

        private void button3_Click (object sender, EventArgs e) {
            Help help = new Help ();
            help.Show ();
        }
    }
}
