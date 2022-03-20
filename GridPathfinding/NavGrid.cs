using SimplePriorityQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridPathfinding {
    public class NavGrid {
        private int cols;
        private int rows;

        private const int maxSize = 3;

        private bool[][] temp;
        private PathNode[][] graph;
        private PriorityQueue<PathNode> queue;

        public NavGrid(int rows, int cols) {
            this.rows = rows;
            this.cols = cols;
            graph = new PathNode[rows][];
            for (int r = 0; r < rows; r++) {
                graph[r] = new PathNode[cols];
                for (int c = 0; c < cols; c++)
                    graph[r][c] = new PathNode(new GridCoord(r, c));
            }
            temp = new bool[maxSize][];
            for (int i = 0; i < maxSize; i++)
                temp[i] = new bool[maxSize];
            queue = new PriorityQueue<PathNode>();
        }

        public void Set(GridCoord coord, bool walkable) {
            graph[coord.r][coord.c].walkable = walkable;
        }

        public List<GridCoord> GetPath(GridCoord start, GridCoord end, int sz = 1) {
            GridCoord s = start;
            GridCoord e = end;

            queue.Clear();
            ClearPathData();

            MaskGrid(start, sz);
            var startNode = graph[s.r][s.c];
            startNode.f = s.Distance(end);
            startNode.g = 0;
            queue.Add(startNode);
            PathNode node;
            do {
                node = queue.Poll();
                node.closed = true;
                Discover(node, sz, e);
            } while (!node.coord.Equals(e) && queue.Count > 0);
            bool reachable = graph[e.r][e.c].reachable;
            List<GridCoord> path;
            if (!reachable)
                path = null;
            else {
                path = new List<GridCoord>();
                do {
                    path.Add(node.coord);
                    node = node.previous;
                } while (node != null);
                path.Reverse();
            }
            UnMaskGrid(start, sz);
            return path;
        }

        private void Discover(PathNode node, int sz, GridCoord destination) {
            int r = node.coord.r;
            int c = node.coord.c;

            TestAdd(node, new GridCoord(r - 1, c), destination, sz);
            TestAdd(node, new GridCoord(r + 1, c), destination, sz);
            TestAdd(node, new GridCoord(r, c - 1), destination, sz);
            TestAdd(node, new GridCoord(r, c + 1), destination, sz);
        }

        private void TestAdd(PathNode node, GridCoord next, GridCoord destination, int sz) {
            if (next.r < 0 || next.r + sz > rows || next.c < 0 || next.c + sz > cols)
                return;
            var nextNode = graph[next.r][next.c];
            if (nextNode.closed)
                return;
            var coords = next.GetAdjacent(sz);
            if (coords.Any(coord => !graph[coord.r][coord.c].walkable))
                return;
            float g = node.g + 1;
            if (g < nextNode.g) {
                float h = next.Distance(destination);
                nextNode.g = node.g + 1;
                nextNode.f = nextNode.g + h;
                nextNode.previous = node;
                nextNode.reachable = true;
                if (queue.Contains(nextNode))
                    queue.Update(nextNode);
                else
                    queue.Add(nextNode);
            }
        }

        private void MaskGrid(GridCoord coord, int sz) {
            for (int r = 0; r < sz; r++) {
                for (int c = 0; c < sz; c++)
                    graph[coord.r + r][coord.c + c].walkable = true;
            }
        }

        private void UnMaskGrid(GridCoord coord, int sz) {
            for (int r = 0; r < sz; r++) {
                for (int c = 0; c < sz; c++)
                    graph[coord.r + r][coord.c + c].walkable = false;
            }
        }

        private void ClearPathData() {
            for (int r = 0; r < rows; r++) {
                for (int c = 0; c < cols; c++)
                    graph[r][c].ClearPathData();
            }
        }
    }
}
