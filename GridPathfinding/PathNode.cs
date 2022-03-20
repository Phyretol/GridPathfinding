using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridPathfinding {
    public class PathNode : IComparable {
        public PathNode previous;
        public GridCoord coord;
        public float g;
        public float f;
        public bool walkable;
        public bool closed;
        public bool reachable;

        public PathNode(GridCoord coord) {
            this.coord = coord;
            previous = null;
            f = g = 0;
            walkable = false;
            closed = false;
            reachable = false;
        }

        public void ClearPathData() {
            g = float.PositiveInfinity;
            previous = null;
            closed = false;
            reachable = false;
        }

        public int CompareTo(object obj) {
            PathNode pathNode = (PathNode)obj;
            return -(f.CompareTo(pathNode.f));
        }

        public static bool operator ==(PathNode n1, PathNode n2) {
            if (n1 is null)
                return n2 is null;
            if (n2 is null)
                return false;
            return n1.coord.Equals(n2.coord);
        }

        public static bool operator !=(PathNode n1, PathNode n2) {
            if (n1 is null)
                return !(n2 is null);
            if (n2 is null)
                return true;
            return !n1.coord.Equals(n2.coord);
        }

        public override int GetHashCode() {
            return coord.GetHashCode();
        }

        public override bool Equals(object obj) {
            PathNode node = (PathNode)obj;
            return coord.Equals(node.coord);
        }
    }
}
