# GridPathfinding

Grid based A* pathfinding implementation  

## Sample code

```C#
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
```  
  
When the `GetPath()` method is called providing a `size` parameter, only paths that are at least `size` units wide on the grid will be considered valid.
