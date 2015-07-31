using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineTris.Properties;

namespace MineTris {
    public class TetrisBoard {
        /// <summary>
        /// Records all the stationary tetris blocks on the tetris board.
        /// </summary>
        public TetrisBlock[,] tetrisBlockMatrix;

        /// <summary>
        /// A Tetris Window instance used to manipulate the "fallingTetrimino" field.
        /// </summary>
        private TetrisWindow form;

        /// <summary>
        /// Creates a new instance of TetrisBoard.
        /// </summary>
        public TetrisBoard (TetrisWindow form) {
            tetrisBlockMatrix = new TetrisBlock[16, 22];
            tetrisBlockMatrix.Initialize ();
            r = new Random ();
            this.form = form;
        }

        /// <summary>
        /// A Random object.
        /// </summary>
        Random r;

        /// <summary>
        /// Adds a tetrimino to the form.
        /// </summary>
        public void AddTetrimino () {
            Tetrimino tetrimino = null;
            int randomNumber = r.Next (140);
            if (randomNumber < 20) {
                tetrimino = new OShapedTetrimino (tetrisBlockMatrix, form);
            } else if (randomNumber < 40) {
                tetrimino = new IShapedTetrimino (r.Next (2), tetrisBlockMatrix,
                    form);
            } else if (randomNumber < 60) {
                tetrimino = new ZShapedTetrimino (r.Next (2), tetrisBlockMatrix,
                    form);
            } else if (randomNumber < 80) {
                tetrimino = new SShapedTetrimino (r.Next (2), tetrisBlockMatrix,
                    form);
            } else if (randomNumber < 100) {
                tetrimino = new TShapedTetrimino (r.Next (4), tetrisBlockMatrix,
                    form);
            } else if (randomNumber < 120) {
                tetrimino = new JShapedTetrimino (r.Next (4), tetrisBlockMatrix,
                    form);
            } else {
                tetrimino = new LShapedTetrimino (r.Next (4), tetrisBlockMatrix,
                    form);
            }

            AddMinesTo (tetrimino);

            tetrimino.Landed += tetrimino_Landed;
            tetrimino.SetTimer (form.timerInterval);
            form.fallingTetrimino = tetrimino;
        }

        private void AddMinesTo (Tetrimino tetrimino) {
            for (int i = 0, chance = 70 ; i < 4 ; i++,
                            chance = chance / 6) {
                if (r.Next (100) < chance)
                    tetrimino.blocks[i].HasMine = true;
            }
        }


        /// <summary>
        /// An event handler for the tetrimino's landed event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tetrimino_Landed (object sender, EventArgs e) {
            if (sender is Tetrimino) {
                Tetrimino _sender = (Tetrimino)sender;
                try {
                    _sender.AddTetrisBlocksToMatrix (tetrisBlockMatrix);
                    form.CheckScore ();
                    AddTetrimino ();
                } catch (ArgumentException) {
                    form.ResetGame ();
                    System.Windows.Forms.MessageBox.Show ("Game Over!", "MineTris");
                }
            } else {
                throw new ArgumentException ("The tetrimino landed with an error!");
            }
        }
    }
}
