using System;
using System.Drawing;
using static MineTris.Properties.Resources;

namespace MineTris {
    public static class TetrisUtility {

        /// <summary>
        /// Rotate a set of coordinates clockwise.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void RotateClockwise (ref int x, ref int y) {
            int temp;
            temp = y;
            y = x;
            x = -temp;
        }

        /// <summary>
        /// Rotate a set of coordinates anti-clockwise.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void RotateAnticlockwise (ref int x, ref int y) {
            int temp;
            temp = x;
            x = y;
            y = -temp;
        }

        /// <summary>
        /// Rotates a tetrimino clockwise if it can.
        /// </summary>
        /// <param name="tetrimino">A tetrimino</param>
        /// <param name="rotationPointBlockIndex">The index of the tetris block that is the centre of rotation in the tetrimino's blocks field.</param>
        /// <returns>true if it rotates successfully.</returns>
        public static bool TryRotateTetriminoClockwise (Tetrimino tetrimino, int rotationPointBlockIndex) {
            //Local variable declarations
            bool a, b, c, d;
            TetrisBlock rotationBlock = tetrimino.blocks[rotationPointBlockIndex];
            int X1, Y1, X2, Y2, X3, Y3, X4, Y4;

            //Assign coordinates relative to the centre of rotation.
            X1 = tetrimino.blocks[0].X - rotationBlock.X;
            Y1 = tetrimino.blocks[0].Y - rotationBlock.Y;
            X2 = tetrimino.blocks[1].X - rotationBlock.X;
            Y2 = tetrimino.blocks[1].Y - rotationBlock.Y;
            X3 = tetrimino.blocks[2].X - rotationBlock.X;
            Y3 = tetrimino.blocks[2].Y - rotationBlock.Y;
            X4 = tetrimino.blocks[3].X - rotationBlock.X;
            Y4 = tetrimino.blocks[3].Y - rotationBlock.Y;

            //Rotate the coordinates.
            TetrisUtility.RotateClockwise (ref X1, ref Y1);
            TetrisUtility.RotateClockwise (ref X2, ref Y2);
            TetrisUtility.RotateClockwise (ref X3, ref Y3);
            TetrisUtility.RotateClockwise (ref X4, ref Y4);

            //Declares two arrays of absolute x and y coordinates.
            int[] xArray = new int[] { rotationBlock.X + X1, rotationBlock.X + X2, rotationBlock.X + X3, rotationBlock.X + X4 };
            int[] yArray = new int[] { rotationBlock.Y + Y1, rotationBlock.Y + Y2, rotationBlock.Y + Y3, rotationBlock.Y + Y4 };

            //Check if the coordinates are valid
            a = tetrimino.CheckPositionValid (xArray[0], yArray[0]);
            b = tetrimino.CheckPositionValid (xArray[1], yArray[1]);
            c = tetrimino.CheckPositionValid (xArray[2], yArray[2]);
            d = tetrimino.CheckPositionValid (xArray[3], yArray[3]);

            //If the coordinates are valid, assign them to each of the blocks.
            if (a && b && c && d) {
                for (int i = 0 ; i < 4 ; i++) {
                    tetrimino.blocks[i].X = xArray[i];
                    tetrimino.blocks[i].Y = yArray[i];
                }
                return true;
            }
            return false;
        }


        /// <summary>
        /// Rotates a tetrimino anti-clockwise if it can.
        /// </summary>
        /// <param name="tetrimino">A tetrimino</param>
        /// <param name="rotationPointBlockIndex">The index of the tetris block that is the centre of rotation in the tetrimino's blocks field.</param>
        /// <returns>true if it rotates successfully.</returns>
        public static bool TryRotateTetriminoAnticlockwise (Tetrimino tetrimino, int rotationPointBlockIndex) {
            //Local variable declarations
            bool a, b, c, d;
            TetrisBlock rotationBlock = tetrimino.blocks[rotationPointBlockIndex];
            int X1, Y1, X2, Y2, X3, Y3, X4, Y4;

            //Assign coordinates relative to the centre of rotation.
            X1 = tetrimino.blocks[0].X - rotationBlock.X;
            Y1 = tetrimino.blocks[0].Y - rotationBlock.Y;
            X2 = tetrimino.blocks[1].X - rotationBlock.X;
            Y2 = tetrimino.blocks[1].Y - rotationBlock.Y;
            X3 = tetrimino.blocks[2].X - rotationBlock.X;
            Y3 = tetrimino.blocks[2].Y - rotationBlock.Y;
            X4 = tetrimino.blocks[3].X - rotationBlock.X;
            Y4 = tetrimino.blocks[3].Y - rotationBlock.Y;

            //Rotate the coordinates.
            TetrisUtility.RotateAnticlockwise (ref X1, ref Y1);
            TetrisUtility.RotateAnticlockwise (ref X2, ref Y2);
            TetrisUtility.RotateAnticlockwise (ref X3, ref Y3);
            TetrisUtility.RotateAnticlockwise (ref X4, ref Y4);

            //Declares two arrays of absolute x and y coordinates.
            int[] xArray = new int[] { rotationBlock.X + X1, rotationBlock.X + X2, rotationBlock.X + X3, rotationBlock.X + X4 };
            int[] yArray = new int[] { rotationBlock.Y + Y1, rotationBlock.Y + Y2, rotationBlock.Y + Y3, rotationBlock.Y + Y4 };

            //Check if the coordinates are valid
            a = tetrimino.CheckPositionValid (xArray[0], yArray[0]);
            b = tetrimino.CheckPositionValid (xArray[1], yArray[1]);
            c = tetrimino.CheckPositionValid (xArray[2], yArray[2]);
            d = tetrimino.CheckPositionValid (xArray[3], yArray[3]);

            //If the coordinates are valid, assign them to each of the blocks.
            if (a && b && c && d) {
                for (int i = 0 ; i < 4 ; i++) {
                    tetrimino.blocks[i].X = xArray[i];
                    tetrimino.blocks[i].Y = yArray[i];
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds the rotation index by one.
        /// </summary>
        public static void AddRotationIndex (IRotatable tetrimino) {
            try {
                tetrimino.RotationIndex++;
            } catch (ArgumentException) {
                tetrimino.RotationIndex = 0;
            }
        }

        /// <summary>
        /// Gets a array containing all the tile images.
        /// </summary>
        public static Image[] GetNumberImages () {
            return new Image[] { tile0, tile1, tile2, tile3, tile4, tile5, tile6,
                                tile7, tile8 };
        }
    }
}
