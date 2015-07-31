using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineTris {
    public class ZShapedTetrimino : Tetrimino, IRotatable{
        /// <summary>
        /// Indicates how many times the object has rotated.
        /// </summary>
        private int rotationIndex;

        /// <summary>
        /// Constructs a new Z shaped tetrimino.
        /// </summary>
        /// <param name="matrix">Please pass the matrix field of the TetrisBoard object.</param>
        public ZShapedTetrimino (int rotationIndex, TetrisBlock[,] matrix, TetrisWindow form) : base (matrix, form){
            if (rotationIndex == 0) {
                blocks = new List<TetrisBlock> () {
                    new TetrisBlock (Color.Blue, 6, 0, form),
                    new TetrisBlock (Color.Blue, 7, 0, form),
                    new TetrisBlock (Color.Blue, 7, 1, form),
                    new TetrisBlock (Color.Blue, 8, 1, form)
                };
            } else if (rotationIndex == 1) {
                blocks = new List<TetrisBlock> () {
                    new TetrisBlock (Color.Blue, 8, 0, form),
                    new TetrisBlock (Color.Blue, 8, 1, form),
                    new TetrisBlock (Color.Blue, 7, 1, form),
                    new TetrisBlock (Color.Blue, 7, 2, form)
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
                        return new TetrisBlock[] { blocks[0], blocks[2], blocks[3] };
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
            if (TryRotateTo0 (0))
                rotationIndex = 0;
            else if (TryRotateTo0 (1))
                rotationIndex = 0;
            else if (TryRotateTo0 (2))
                rotationIndex = 0;
            else if (TryRotateTo0 (3))
                rotationIndex = 0;
        }

        /// <summary>
        /// Rotates the tetrimino to rotationIndex 1.
        /// </summary>
        private void RotateTo1 () {
            if (TryRotateTo1 (0))
                rotationIndex = 1;
            else if (TryRotateTo1 (1))
                rotationIndex = 1;
            else if (TryRotateTo1 (2))
                rotationIndex = 1;
            else if (TryRotateTo1 (3))
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
                    throw new ArgumentException ("Rotation Index must be 1 or 0 for a Z shaped Tetrimino.", "rotationIndex");
                }
            }
        }

        private bool TryRotateTo0 (int rotationPointBlockIndex) {
            bool a, b, c, d;
            TetrisBlock rotationBlock = blocks[rotationPointBlockIndex];
            int X1, Y1, X2, Y2, X3, Y3, X4, Y4;
            X1 = blocks[0].X - rotationBlock.X;
            Y1 = blocks[0].Y - rotationBlock.Y;
            X2 = blocks[1].X - rotationBlock.X;
            Y2 = blocks[1].Y - rotationBlock.Y;
            X3 = blocks[2].X - rotationBlock.X;
            Y3 = blocks[2].Y - rotationBlock.Y;
            X4 = blocks[3].X - rotationBlock.X;
            Y4 = blocks[3].Y - rotationBlock.Y;
            
            TetrisUtility.RotateAnticlockwise (ref X1, ref Y1);
            TetrisUtility.RotateAnticlockwise (ref X2, ref Y2);
            TetrisUtility.RotateAnticlockwise (ref X3, ref Y3);
            TetrisUtility.RotateAnticlockwise (ref X4, ref Y4);

            int[] xArray = new int[] { rotationBlock.X + X1, rotationBlock.X + X2, rotationBlock.X + X3, rotationBlock.X + X4 };
            int[] yArray = new int[] { rotationBlock.Y + Y1, rotationBlock.Y + Y2, rotationBlock.Y + Y3, rotationBlock.Y + Y4 };

            a = CheckPositionValid (xArray[0], yArray[0]);
            b = CheckPositionValid (xArray[1], yArray[1]);
            c = CheckPositionValid (xArray[2], yArray[2]);
            d = CheckPositionValid (xArray[3], yArray[3]);
            if (a && b && c && d) {
                for (int i = 0 ; i < 4 ; i++) {
                    blocks[i].X = xArray[i];
                    blocks[i].Y = yArray[i];
                }
                return true;
            }
            return false;
        }

        private bool TryRotateTo1 (int rotationPointBlockIndex) {
            //Local variable declarations
            bool a, b, c, d;
            TetrisBlock rotationBlock = blocks[rotationPointBlockIndex];
            int X1, Y1, X2, Y2, X3, Y3, X4, Y4;

            //Assign coordinates relative to the centre of rotation.
            X1 = blocks[0].X - rotationBlock.X;
            Y1 = blocks[0].Y - rotationBlock.Y;
            X2 = blocks[1].X - rotationBlock.X;
            Y2 = blocks[1].Y - rotationBlock.Y;
            X3 = blocks[2].X - rotationBlock.X;
            Y3 = blocks[2].Y - rotationBlock.Y;
            X4 = blocks[3].X - rotationBlock.X;
            Y4 = blocks[3].Y - rotationBlock.Y;

            //Rotate the coordinates.
            TetrisUtility.RotateClockwise (ref X1, ref Y1);
            TetrisUtility.RotateClockwise (ref X2, ref Y2);
            TetrisUtility.RotateClockwise (ref X3, ref Y3);
            TetrisUtility.RotateClockwise (ref X4, ref Y4);

            //Declares two arrays of absolute x and y coordinates.
            int[] xArray = new int[] { rotationBlock.X + X1, rotationBlock.X + X2, rotationBlock.X + X3, rotationBlock.X + X4 };
            int[] yArray = new int[] { rotationBlock.Y + Y1, rotationBlock.Y + Y2, rotationBlock.Y + Y3, rotationBlock.Y + Y4 };

            //Check if the coordinates are valid
            a = CheckPositionValid (xArray[0], yArray[0]);
            b = CheckPositionValid (xArray[1], yArray[1]);
            c = CheckPositionValid (xArray[2], yArray[2]);
            d = CheckPositionValid (xArray[3], yArray[3]);

            //If the coordinates are valid, assign them to each of the blocks.
            if (a && b && c && d) {
                for (int i = 0 ; i < 4 ; i++) {
                    blocks[i].X = xArray[i];
                    blocks[i].Y = yArray[i];
                }
                return true;
            }
            return false;
        }

    }
}
