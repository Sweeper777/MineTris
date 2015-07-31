using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MineTris {
    public class IShapedTetrimino : Tetrimino, IRotatable {

        /// <summary>
        /// Constructs a new I shaped tetrimino.
        /// </summary>
        /// <param name="matrix">Please pass the matrix field of the TetrisBoard object.</param>
        public IShapedTetrimino (int rotationIndex, TetrisBlock[,] matrix, TetrisWindow form) : base(matrix, form) {
            if (rotationIndex == 0) {
                blocks = new List<TetrisBlock> () {
                    new TetrisBlock (Color.Red, 7, 0, form),
                    new TetrisBlock (Color.Red, 7, 1, form),
                    new TetrisBlock (Color.Red, 7, 2, form),
                    new TetrisBlock (Color.Red, 7, 3, form)
                };
            } else if (rotationIndex == 1) {
                blocks = new List<TetrisBlock> () {
                    new TetrisBlock (Color.Red, 6, 0, form),
                    new TetrisBlock (Color.Red, 7, 0, form),
                    new TetrisBlock (Color.Red, 8, 0, form),
                    new TetrisBlock (Color.Red, 9, 0, form)
                };
            }
            RotationIndex = rotationIndex;
        }

        /// <summary>
        /// Indicates how many times the object has rotated.
        /// </summary>
        private int rotationIndex;

        /// <summary>
        /// Gets which side(s) of the tetrinimo can land on other tetrinimo or the ground.
        /// </summary>
        protected override TetrisBlock[] touchingSides {
            get {
                if (RotationIndex == 0) {
                    return new TetrisBlock[] { blocks[3] };
                } else {
                    return blocks.ToArray (); 
                }
            }
        }

        /// <summary>
        /// Rotates the object clockwise.
        /// </summary>
        public void Rotate () {
            if (RotationIndex == 1) {
                RotateTo0 ();
            } else {
                RotateTo1 ();
            }
        }

        /// <summary>
        /// Rotate to rotationIndex 0.
        /// </summary>
        private void RotateTo0 () {
            if (RotateTo00 ()) {
                RotationIndex = 0;
            } else if (RotateTo01 ()) {
                RotationIndex = 0;
            } else if (RotateTo02 ()) {
                RotationIndex = 0;
            } else if (RotateTo03 ()) {
                RotationIndex = 0;
            }
        }

        /// <summary>
        /// Rotate to RotationIndex 1.
        /// </summary>
        private void RotateTo1 () {
            if (RotateTo10 ()) {
                RotationIndex = 1;
            } else if (RotateTo11 ()) {
                RotationIndex = 1;
            } else if (RotateTo12 ()) {
                RotationIndex = 1;
            } else if (RotateTo13 ()) {
                RotationIndex = 1;
            }
        }

        #region a lot of methods...
        bool RotateTo00 () {
            bool a, b, c;
            a = CheckPositionValid (blocks[0].X, blocks[0].Y + 1);
            b = CheckPositionValid (blocks[0].X, blocks[0].Y + 2);
            c = CheckPositionValid (blocks[0].X, blocks[0].Y + 3);
            if (a && b && c) {
                for (int i = 1 ; i <= 3 ; i++) {
                    blocks[i].X = blocks[0].X;
                    blocks[i].Y = blocks[0].Y + i;
                }
                return true;
            }
            return false;
        }

        bool RotateTo01 () {
            bool a, b, c;
            a = CheckPositionValid (blocks[1].X, blocks[1].Y - 1);
            b = CheckPositionValid (blocks[1].X, blocks[1].Y + 1);
            c = CheckPositionValid (blocks[1].X, blocks[1].Y + 2);
            if (a && b && c) {
                blocks[0].X = blocks[1].X;
                blocks[0].Y = blocks[1].Y - 1;
                blocks[2].X = blocks[1].X;
                blocks[2].Y = blocks[1].Y + 1;
                blocks[3].X = blocks[1].X;
                blocks[3].Y = blocks[1].Y + 2;
                return true;
            }
            return false;
        }

        bool RotateTo02 () {
            bool a, b, c;
            a = CheckPositionValid (blocks[2].X, blocks[2].Y - 2);
            b = CheckPositionValid (blocks[2].X, blocks[2].Y - 1);
            c = CheckPositionValid (blocks[2].X, blocks[2].Y + 1);
            if (a && b && c) {
                blocks[0].X = blocks[2].X;
                blocks[0].Y = blocks[2].Y - 2;
                blocks[1].X = blocks[2].X;
                blocks[1].Y = blocks[2].Y - 1;
                blocks[3].X = blocks[2].X;
                blocks[3].Y = blocks[2].Y + 1;
                return true;
            }
            return false;
        }

        bool RotateTo03 () {
            bool a, b, c;
            a = CheckPositionValid (blocks[3].X, blocks[3].Y - 3);
            b = CheckPositionValid (blocks[3].X, blocks[3].Y - 2);
            c = CheckPositionValid (blocks[3].X, blocks[3].Y - 1);
            if (a && b && c) {
                blocks[0].X = blocks[3].X;
                blocks[0].Y = blocks[3].Y - 3;
                blocks[1].X = blocks[3].X;
                blocks[1].Y = blocks[3].Y - 2;
                blocks[2].X = blocks[3].X;
                blocks[2].Y = blocks[3].Y - 1;
                return true;
            }
            return false;
        }

        bool RotateTo10 () {
            bool a, b, c;
            a = CheckPositionValid (blocks[0].X + 1, blocks[0].Y);
            b = CheckPositionValid (blocks[0].X + 2, blocks[0].Y);
            c = CheckPositionValid (blocks[0].X + 3, blocks[0].Y);
            if (a && b && c) {
                blocks[1].X = blocks[0].X + 1;
                blocks[1].Y = blocks[0].Y;
                blocks[2].X = blocks[0].X + 2;
                blocks[2].Y = blocks[0].Y;
                blocks[3].X = blocks[0].X + 3;
                blocks[3].Y = blocks[0].Y;
                return true;
            }
            return false;
        }

        bool RotateTo11 () {
            bool a, b, c;
            a = CheckPositionValid (blocks[1].X - 1, blocks[1].Y);
            b = CheckPositionValid (blocks[1].X + 1, blocks[1].Y);
            c = CheckPositionValid (blocks[1].X + 2, blocks[1].Y);
            if (a && b && c) {
                blocks[0].X = blocks[1].X - 1;
                blocks[0].Y = blocks[1].Y;
                blocks[2].X = blocks[1].X + 1;
                blocks[2].Y = blocks[1].Y;
                blocks[3].X = blocks[1].X + 2;
                blocks[3].Y = blocks[1].Y;
                return true;
            }
            return false;
        }

        bool RotateTo12 () {
            bool a, b, c;
            a = CheckPositionValid (blocks[2].X - 2, blocks[2].Y);
            b = CheckPositionValid (blocks[2].X - 1, blocks[2].Y);
            c = CheckPositionValid (blocks[2].X + 1, blocks[2].Y);
            if (a && b && c) {
                blocks[0].X = blocks[2].X - 2;
                blocks[0].Y = blocks[2].Y;
                blocks[1].X = blocks[2].X - 1;
                blocks[1].Y = blocks[2].Y;
                blocks[3].X = blocks[2].X + 1;
                blocks[3].Y = blocks[2].Y;
                return true;
            }
            return false;
        }

        bool RotateTo13 () {
            bool a, b, c;
            a = CheckPositionValid (blocks[3].X - 3, blocks[3].Y);
            b = CheckPositionValid (blocks[3].X - 2, blocks[3].Y);
            c = CheckPositionValid (blocks[3].X - 1, blocks[3].Y);
            if (a && b && c) {
                blocks[0].X = blocks[3].X - 3;
                blocks[0].Y = blocks[3].Y;
                blocks[1].X = blocks[3].X - 2;
                blocks[1].Y = blocks[3].Y;
                blocks[2].X = blocks[3].X - 1;
                blocks[2].Y = blocks[3].Y;
                return true;
            }
            return false;
        }
        #endregion

        /// <summary>
        /// Indicates how many times the object has rotated.
        /// </summary>
        public int RotationIndex {
            get {
                return rotationIndex;
            }
            set {
                if (value == 0 || value == 1) {
                    rotationIndex = value;
                } else {
                    throw new ArgumentException ("RotationIndex must not be set to " + value);
                }
            }
        }
    }
}
