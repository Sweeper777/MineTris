using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineTris {
    public class OShapedTetrimino : Tetrimino {
        /// <summary>
        /// Construct a new O Shaped Tetrinimo.
        /// </summary>
        /// <param name="matrix">Please pass the matrix field of the TetrisBoard object.</param>
        public OShapedTetrimino (TetrisBlock[,] matrix, TetrisWindow form) : base (matrix, form) {
            blocks = new List<TetrisBlock> () {
                new TetrisBlock (Color.Cyan, 7, 0, form),
                new TetrisBlock (Color.Cyan, 7, 1,form),
                new TetrisBlock (Color.Cyan, 8, 0, form),
                new TetrisBlock (Color.Cyan, 8, 1, form)
            };
        }

        /// <summary>
        /// Gets which side(s) of the tetrinimo can land on other tetrinimo or the ground.
        /// </summary>
        protected override TetrisBlock[] touchingSides {
            get { return new TetrisBlock[] { blocks[1], blocks[3] }; }
        }
    }
}
