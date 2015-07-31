using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineTris {
    public class CustomTetrimino {
        /// <summary>
        /// The blocks that belong to the Tetrimino.
        /// </summary>
        public List<TetrisBlock> blocks;

        /// <summary>
        /// Moves the Tetrimino down by one block.
        /// </summary>
        public void MoveDown () {
            foreach (TetrisBlock block in blocks) {
                block.MoveDown ();
            }
        }

        /// <summary>
        /// Adds all the tetris blocks that belong to this tetrimio to a matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        public void AddTetrisBlocksToMatrix (TetrisBlock[,] matrix) {
            foreach (TetrisBlock block in blocks) {
                matrix[block.X, block.Y] = block;
            }
        }

        /// <summary>
        /// Creates a new custom tetrimino.
        /// </summary>
        /// <param name="blocks">What block(s) does the tetrimino has initially.</param>
        /// <param name="matrix"></param>
        public CustomTetrimino (List<TetrisBlock> blocks, TetrisBlock[,] matrix) {
            this.blocks = blocks;
            foreach (TetrisBlock block in blocks) {
                matrix[block.X, block.Y] = null;
            }
        }
    }
}
