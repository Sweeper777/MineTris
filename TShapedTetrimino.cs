using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineTris {
    public class TShapedTetrimino : Tetrimino, IRotatable {
        /// <summary>
        /// Indicates how many times the object has rotated.
        /// </summary>
        private int rotationIndex;

        /// <summary>
        /// Constructs a new T shaped tetrimino.
        /// </summary>
        /// <param name="matrix">Please pass the matrix field of the TetrisBoard object.</param>
        public TShapedTetrimino (int rotationIndex, TetrisBlock[,] matrix, TetrisWindow form)
            : base (matrix, form) {
            blocks = new List<TetrisBlock> () {
                    new TetrisBlock (Color.Olive, 6, 0, form),
                    new TetrisBlock (Color.Olive, 7, 0, form),
                    new TetrisBlock (Color.Olive, 8, 0, form),
                    new TetrisBlock (Color.Olive, 7, 1, form)
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
                        return new TetrisBlock[] { blocks[0], blocks[2], blocks[3] };
                    case 1:
                        return new TetrisBlock[] { blocks[3], blocks[2] };
                    case 2:
                        return new TetrisBlock[] { blocks[0], blocks[2], blocks[1] };
                    case 3:
                        return new TetrisBlock[] { blocks[3], blocks[0] };
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
                    throw new ArgumentException ("Rotation Index must be between 0 and 3 for a T shaped Tetrimino.", "rotationIndex");
                }
            }
        }
    }
}
