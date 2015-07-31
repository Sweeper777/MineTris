//#define DEBUG

using MineTris.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MineTris {

    /// <summary>
    /// Represents a single tetris block.
    /// </summary>
    public class TetrisBlock {
        /// <summary>
        /// A tetris window instane used to access the tetris board.
        /// </summary>
        private TetrisWindow form;

        /// <summary>
        /// The picture box corresponding to the tetris block.
        /// </summary>
        public PictureBox picture;

        /// <summary>
        /// The color of the tetris block.
        /// </summary>
        protected Color color;

        /// <summary>
        /// The x position of the block.
        /// </summary>
        private int x;

        /// <summary>
        /// The y position of the block.
        /// </summary>
        private int y;

        /// <summary>
        /// Indicates whether the tetris block contains a mine.
        /// </summary>
        private bool hasMine;

        /// <summary>
        /// Indicates whether the player has put a flag on the block.
        /// </summary>
        private bool hasFlag;

        /// <summary>
        /// The number on the tile. If the tile hasn't been uncovered or has 
        /// a flag, number is null.
        /// </summary>
        private int? number;

        /// <summary>
        /// A timer used to update the number on the block.
        /// </summary>
        private System.Windows.Forms.Timer timer;

        /// <summary>
        /// The x position of the block.
        /// </summary>
        public int X {
            get { return x; }
            set {
                x = value;
                picture.Location = new Point (value * 20, y * 20);
            }
        }

        /// <summary>
        /// The y position of the block;
        /// </summary>
        public int Y {
            get { return y; }
            set {
                y = value;
                picture.Location = new Point (x * 20, value * 20);
            }
        }

        /// <summary>
        /// The color of the tetris block.
        /// </summary>
        public Color Color {
            get { return color; }
            set {
                color = value;
                picture.BackColor = value;
            }
        }

        /// <summary>
        /// Gets or sets whether the teris block contains a mine.
        /// </summary>
        public bool HasMine {
            get { return hasMine; }
            set {
                hasMine = value;
#if DEBUG
                if (value) {
                    picture.Image = Resources.tileMine;
                }
#endif
            }
        }

        /// <summary>
        /// Gets or sets whether the tetris block has a flag.
        /// </summary>
        public bool HasFlag {
            get { return hasFlag; }
            set { hasFlag = value; }
        }

        /// <summary>
        /// Gets oor sets whether the block is uncovered.
        /// </summary>
        public bool Uncovered {
            get;
            set;
        }

        /// <summary>
        /// Gets whether the flag placed on the block is correctly placed.
        /// </summary>
        public bool IsCorrect {
            get { return HasFlag == HasMine; }
        }

        /// <summary>
        /// Creates a new instance of TetrisBlock.
        /// </summary>
        /// <param name="color">The color of the block.</param>
        /// <param name="picture">The picture box corresponding to the block.</param>
        public TetrisBlock (Color color, int x, int y, TetrisWindow form) {
            picture = new PictureBox ();
            timer = new System.Windows.Forms.Timer () { Interval = 1 };
            timer.Tick += OnTimerTick;
            Color = color;

            X = x;
            Y = y;

            picture.Name = "TetrisBlock";
            picture.Size = new Size (20, 20);
            picture.SizeMode = PictureBoxSizeMode.Zoom;
            picture.Image = Resources.tetrisBlock;
            picture.Visible = true;
            form.panel1.Controls.Add (picture);
            this.form = form;

            timer.Start ();
        }

        private void OnTimerTick (object sender, EventArgs e) {
            if (!Uncovered || HasFlag)
                return;

            List<TetrisBlock> adjBlocks;
            FindAdjacentBlocks (form.board.tetrisBlockMatrix, out adjBlocks);

            int numberOfMines = CountMines (adjBlocks);
            if (numberOfMines != number) {
                picture.Image = TetrisUtility.GetNumberImages ()[numberOfMines];
            }
        }


        /// <summary>
        /// Moves the tetris block 1 unit downwards.
        /// </summary>
        public void MoveDown () {
            Y++;
        }

        /// <summary>
        /// Subscribe to the click event of the picture box of this tetris block.
        /// </summary>
        public void SubscribeToClickEvent () {
            picture.Click += OnPictureClick;
        }

        /// <summary>
        /// Is called when the picture box of this teris block is clicked.
        /// </summary>
        private void OnPictureClick (object sender, EventArgs e) {
            if (!( e is MouseEventArgs ) || form.isPaused)
                return;

            MouseEventArgs eventArgs = (MouseEventArgs)e;

            if (HasFlag) {
                HasFlag = false;
                picture.Image = Resources.tetrisBlock;
                Uncovered = false;
                form.CheckScore ();
                return;
            }

            if (eventArgs.Button == MouseButtons.Left) {
                if (HasMine) {
                    picture.Image = Resources.tileMine;
                    form.ResetGame ();
                    MessageBox.Show ("It's a mine!\nGame Over!", "MineTris");
                    return;
                } else {
                    Uncover ();
                }
            } else if (eventArgs.Button == MouseButtons.Right && !Uncovered) {
                HasFlag = true;
                picture.Image = Resources.tileFlag;
                Uncovered = true;
                form.CheckScore ();
            }
        }

        /// <summary>
        /// Counts how many mines there are around the block and change the 
        /// block's image.
        /// </summary>
        private void Uncover () {
            TetrisBlock[,] matrix = form.board.tetrisBlockMatrix;
            List<TetrisBlock> adjBlocks;
            int numberOfMines = 0;

            FindAdjacentBlocks (matrix, out adjBlocks);

            numberOfMines = CountMines (adjBlocks);

            picture.Image = TetrisUtility.GetNumberImages ()[numberOfMines];
            Uncovered = true;
            number = numberOfMines;

            if (numberOfMines == 0) {
                foreach (TetrisBlock block in adjBlocks) {
                    if (block?.Uncovered == false)
                        block?.Uncover ();
                }
            }
        }

        private static int CountMines (List<TetrisBlock> adjBlocks) {
            int numberOfMines = 0;
            foreach (TetrisBlock block in adjBlocks) {
                if (block != null && block.HasMine)
                    numberOfMines++;
            }

            return numberOfMines;
        }

        private void FindAdjacentBlocks (TetrisBlock[,] matrix, out List<TetrisBlock> adjBlocks) {
            adjBlocks = new List<TetrisBlock> ();

            try {
                adjBlocks.Add (matrix[X - 1, Y - 1]);
            } catch (IndexOutOfRangeException) { }

            try {
                adjBlocks.Add (matrix[X, Y - 1]);
            } catch (IndexOutOfRangeException) { }

            try {
                adjBlocks.Add (matrix[X + 1, Y - 1]);
            } catch (IndexOutOfRangeException) { }

            try {
                adjBlocks.Add (matrix[X - 1, Y]);
            } catch (IndexOutOfRangeException) { }

            try {
                adjBlocks.Add (matrix[X + 1, Y]);
            } catch (IndexOutOfRangeException) { }

            try {
                adjBlocks.Add (matrix[X - 1, Y + 1]);
            } catch (IndexOutOfRangeException) { }

            try {
                adjBlocks.Add (matrix[X, Y + 1]);
            } catch (IndexOutOfRangeException) { }

            try {
                adjBlocks.Add (matrix[X + 1, Y + 1]);
            } catch (IndexOutOfRangeException) { }
        }
    }
}
