using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridPathfinding {
    class Program {
        static void Main(string[] args) {
            int[,] map = new int[,] { 
                { 1, 1, 1, 1 }, 
                { 1, 0, 1, 0 }, 
                { 1, 1, 1, 1 }
            };

            NavGrid navGrid = new NavGrid(map.GetLength(0), map.GetLength(1));
            for (int r = 0; r < map.GetLength(0); r++) {
                for (int c = 0; c < map.GetLength(1); c++) {
                    if (map[r, c] > 0)
                        navGrid.Set(new GridCoord(r, c), true);
                    else
                        navGrid.Set(new GridCoord(r, c), false);
                }
            }

            WritePath(navGrid.GetPath(new GridCoord(0, 0), new GridCoord(2, 3)));
            WritePath(navGrid.GetPath(new GridCoord(0, 0), new GridCoord(0, 3)));
            WritePath(navGrid.GetPath(new GridCoord(0, 3), new GridCoord(2, 3)));
            WritePath(navGrid.GetPath(new GridCoord(0, 0), new GridCoord(2, 3), 2));
            Console.Read();
        }

        static void WritePath(List<GridCoord> path) {
            if (path == null) {
                Console.WriteLine("Path is null");
                return;
            }

            foreach (GridCoord coord in path) 
                Console.Write("(" + coord.r + "," + coord.c + ") ");
            Console.Write("\n");
        }
    }
}
