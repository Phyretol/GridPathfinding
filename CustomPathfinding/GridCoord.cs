using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridPathfinding {
    public struct GridCoord {
        public int r;
        public int c;

        public GridCoord(int r, int c) {
            this.r = r;
            this.c = c;
        }

        public int Distance(GridCoord other) {
            return Math.Abs(r - other.r) + Math.Abs(c - other.c);
        }

        public List<GridCoord> GetAdjacent(int size) {
            List<GridCoord> result = new List<GridCoord>();
            for (int r = 0; r < size; r++) {
                for (int c = 0; c < size; c++)
                    result.Add(new GridCoord(this.r + r, this.c + c));
            }
            return result;
        }
    }
}
