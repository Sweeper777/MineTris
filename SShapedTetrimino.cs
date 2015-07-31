using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineTris {
    public class SShapedTetrimino : Tetrimino, IRotatable{
        /// <summary>
        /// Indicates how many times the object has rotated.
        /// </summary>
        private int rotationIndex;

        /// <summary>
        /// Constructs a new S shaped tetrimino.
        /// </summary>
        /// <param name="matrix">Please pass the matrix field of the TetrisBoard object.</param>
        public SShapedTetrimino (int rotationIndex, TetrisBlock[,] matrix, TetrisWindow form) : base (matrix, form){
            if (rotationIndex == 0) {
                blocks = new List<TetrisBlock> () {
                    new TetrisBlock (Color.Lime, 6, 1, form),
                    new TetrisBlock (Color.Lime, 7, 1, form),
                    new TetrisBlock (Color.Lime, 7, 0, form),
                    new TetrisBlock (Color.Lime, 8, 0, form)
                };
            } else if (rotationIndex == 1) {
                blocks = new List<TetrisBlock> () {
                    new TetrisBlock (Color.Lime, 7, 0, form),
                    new TetrisBlock (Color.Lime, 7, 1, form),
                    new TetrisBlock (Color.Lime, 8, 1, form),
                    new TetrisBlock (Color.Lime, 8, 2, form)
                };
            }
            RotationIndex = rotationIndex;
        }

        /// <summary>
        /// Gets which side(s) of the tetrinimo can land on other tetrinimo or the ground.
        /// </summary>
        protected override TetrisBlock[] touchingSides {
            get {
                switch (RotationIndex) {
                    case 0:
                        return new TetrisBlock[] { blocks[0], blocks[1], blocks[3] };
                    case 1:
                        return new TetrisBlock[] { blocks[3], blocks[1] };
                    default:
                        throw new ArgumentException ("The tetrimino is not rotated properly.");
                }
            }
        }

        /// <summary>
        /// Rotates the object clockwise.
        /// </summary>
        public void Rotate () {
            if (RotationIndex == 0) {
                RotateTo1 ();
            } else {
                RotateTo0 ();
            }
        }

        /// <summary>
        /// Rotates the tetrimino to rotationIndex 0.
        /// </summary>
        private void RotateTo0 () {
            if (TetrisUtility.TryRotateTetriminoAnticlockwise (this, 0))
                rotationIndex = 0;
            else if (TetrisUtility.TryRotateTetriminoAnticlockwise (this, 1))
                rotationIndex = 0;
            else if (TetrisUtility.TryRotateTetriminoAnticlockwise (this, 2))
                rotationIndex = 0;
            else if (TetrisUtility.TryRotateTetriminoAnticlockwise (this, 3))
                rotationIndex = 0;
        }

        /// <summary>
        /// Rotates the tetrimino to rotationIndex 1.
        /// </summary>
        private void RotateTo1 () {
            if (TetrisUtility.TryRotateTetriminoClockwise (this, 0))
                rotationIndex = 1;
            else if (TetrisUtility.TryRotateTetriminoClockwise (this, 1))
                rotationIndex = 1;
            else if (TetrisUtility.TryRotateTetriminoClockwise (this, 2))
                rotationIndex = 1;
            else if (TetrisUtility.TryRotateTetriminoClockwise (this, 3))
                rotationIndex = 1;
        }
        /// <summary>
        /// Indicates how many times the object has rotated.
        /// </summary>
        public int RotationIndex {
            get {
                return rotationIndex;
            }
            set {
                if (value == 1 || value == 0) {
                    rotationIndex = value;
                } else {
                    throw new ArgumentException ("Rotation Index must be 1 or 0 for a S shaped Tetrimino.", "rotationIndex");
                }
            }
        }
    }
}
