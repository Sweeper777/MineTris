using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineTris {
    /// <summary>
    /// Represents a group of tetris blocks that will fall at the same time.
    /// </summary>
    public abstract class Tetrimino : IDisposable {
        /// <summary>
        /// A list of all the tetris blocks that this object holds.
        /// </summary>
        public List<TetrisBlock> blocks;

        /// <summary>
        /// A timer that will tick every second.
        /// </summary>
        protected Timer timer;

        /// <summary>
        /// This field is used to check if the tetrinimo has landed and raise the Landed event.
        /// </summary>
        protected TetrisBlock[,] tetrisBlockMatrix;

        /// <summary>
        /// Gets which side(s) of the tetrinimo can land on other tetrinimo or the ground.
        /// </summary>
        protected abstract TetrisBlock[] touchingSides {
            get;
        }

        /// <summary>
        /// Moves the whole group of tetris blocks down .
        /// </summary>
        public void MoveDown () {
            if (!CheckLanded ()) {
                foreach (TetrisBlock block in blocks) {
                    block.MoveDown ();
                }
            } else {
                timer.Stop ();
                Landed (this, new EventArgs ());
                
                foreach (TetrisBlock block in blocks) {
                    block.SubscribeToClickEvent ();
                }
            }
        }

        /// <summary>
        /// Moves the group of tetris blocks horizontally.
        /// </summary>
        /// <param name="side">Pass 1 for the right and -1 for the left.</param>
        public void Move (int side) {
            if (side == -1 || side == 1) {
                foreach (TetrisBlock block in blocks) {
                    block.X += side;
                }

                foreach (TetrisBlock block in blocks) {
                    if (!CheckPositionValid (block.X, block.Y)) {
                        if (side == -1) {
                            Move (1);
                        } else {
                            Move (-1);
                        }
                    }    
                }
            } else {
                throw new ArgumentException ($"Parameter \"{nameof (side)}\" must not be {side}.", nameof (side));
            }
        }

        /// <summary>
        /// Check if the tetrinimo has landed.
        /// </summary>
        /// <returns>True if the tetrimino has landed.</returns>
        public bool CheckLanded () {
            foreach (var block in touchingSides) {
                if (block.Y >= 21) {
                    return true;
                }
                if (tetrisBlockMatrix[block.X, block.Y + 1] != null) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the given position is valid for a tetris block.
        /// </summary>
        /// <param name="x">The x coordinate of the position.</param>
        /// <param name="y">The y coordinate of the position.</param>
        /// <returns>True if the position is not out of bounds or is null.</returns>
        public bool CheckPositionValid (int x, int y) {
            if (!( x >= 0 && y >= 0 && x <= tetrisBlockMatrix.GetUpperBound (0) && y <= tetrisBlockMatrix.GetUpperBound (1) )) {
                return false;
            }
            if (tetrisBlockMatrix[x, y] == null) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds all the tetris blocks of this tetrmino to a matrix.
        /// </summary>
        /// <param name="matrix">Where to add the tetris blocks</param>
        public void AddTetrisBlocksToMatrix (TetrisBlock[,] matrix) {
            foreach (var block in blocks) {
                if (matrix[block.X, block.Y] == null) {
                    matrix[block.X, block.Y] = block;
                } else {
                    throw new ArgumentException ("The Tetrimino has landed incorrectly.");
                }
            }
        }


        /// <summary>
        /// Called when the timer elapses.
        /// </summary>
        protected void timer_Elapsed (object sender, EventArgs e) {
            MoveDown ();
        }

        /// <summary>
        /// Starts the timer of the tetrimino.
        /// </summary>
        public void StartTimer () {
            timer.Start ();
        }

        /// <summary>
        /// Stops the timer of the tetrimino.
        /// </summary>
        public void StopTimer () {
            timer.Stop ();
        }

        /// <summary>
        /// Sets the interval of the timer of this tetrimino.
        /// </summary>
        /// <param name="interval"></param>
        public void SetTimer (int interval) {
            timer.Interval = interval;
        }

        public void Dispose () {
            ( (IDisposable)timer ).Dispose ();
        }

        protected Tetrimino (TetrisBlock[,] matrix, TetrisWindow form) {
            tetrisBlockMatrix = matrix;
            timer = new Timer () { Interval = 1000, Enabled = true };
            timer.Tick += timer_Elapsed;
        }

        /// <summary>
        /// Raised when the tetrinimo landed on another tetrinimo or the ground.
        /// </summary>
        public event EventHandler Landed;
    }
}
