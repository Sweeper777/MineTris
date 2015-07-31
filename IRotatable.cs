using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineTris {
    public interface IRotatable {
        /// <summary>
        /// Rotates the object clockwise.
        /// </summary>
        void Rotate ();

        /// <summary>
        /// Indicates how many times the object has rotated.
        /// </summary>
        int RotationIndex {
            get;
            set;
        }
    }
}
