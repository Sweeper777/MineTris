using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineTris {
    public class JShapedTetrimino : Tetrimino, IRotatable{
        /// <summary>
        /// Indicates how many times the object has rotated.
        /// </summary>
        private int rotationIndex;

        /// <summary>
        /// Constructs a new J shaped tetrimino.
        /// </summary>
        /// <param name="matrix">Please pass the matrix field of the TetrisBoard object.</param>
        public JShapedTetrimino (int rotationIndex, TetrisBlock[,] matrix, TetrisWindow form)
            : base (matrix, form) {
            blocks = new List<TetrisBlock> () {
                    new TetrisBlock (Color.Magenta, 8, 0, form),
                    new TetrisBlock (Color.Magenta, 8, 1, form),
                    new TetrisBlock (Color.Magenta, 8, 2, form),
                    new TetrisBlock (Color.Magenta, 7, 2, form)
                };
            RotationIndex = 0;
            for (int i = 0 ; i < rotationIndex ; i++) {
                Rotate ();
            }
        }

        /// <summary>
        /// Gets which side(s) of the tetrinimo can land on other tetrinimo or the ground.
        /// </summary>
        protected override TetrisBlock[] touchingSides {
            get {
                switch (RotationIndex) {
                    case 0:
                        return new TetrisBlock[] { blocks[2], blocks[3] };
                    case 1:
                        return new TetrisBlock[] { blocks[1], blocks[2], blocks[0] };
                    case 2:
                        return new TetrisBlock[] { blocks[0], blocks[3] };
                    case 3:
                        return new TetrisBlock[] { blocks[3], blocks[0], blocks[1] };
                    default:
                        throw new ArgumentException ("The tetrimino is not rotated properly.");
                }
            }
        }

        /// <summary>
        /// Rotates the object clockwise.
        /// </summary>
        public void Rotate () {
            if (TetrisUtility.TryRotateTetriminoClockwise (this, 0))
                TetrisUtility.AddRotationIndex (this);
            else if (TetrisUtility.TryRotateTetriminoClockwise (this, 1))
                TetrisUtility.AddRotationIndex (this);
            else if (TetrisUtility.TryRotateTetriminoClockwise (this, 2))
                TetrisUtility.AddRotationIndex (this);
            else if (TetrisUtility.TryRotateTetriminoClockwise (this, 3))
                TetrisUtility.AddRotationIndex (this);
        }

        /// <summary>
        /// Indicates how many times the object has rotated.
        /// </summary>
        public int RotationIndex {
            get {
                return rotationIndex;
            }
            set {
                if (value >= 0 && value <= 3) {
                    rotationIndex = value;
                } else {
                    throw new ArgumentException ("Rotation Index must be between 0 and 3 for a J shaped Tetrimino.", "rotationIndex");
                }
            }
        }
    }
}
