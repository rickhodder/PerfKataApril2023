namespace PerfKataApril2023;

public static class StringExtensions
{
    public static int LargestProduct(this string grid)
    {
        return CalculateMaxProduct(grid);
    }

    public static int[,] ParseGrid(this string grid)
    {
        var tempGrid = grid.Split(new char[] { '\r', '\n' }, options: StringSplitOptions.RemoveEmptyEntries);

        var rows = tempGrid.Length;
        var columns = tempGrid[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;

        var result = new int[rows, columns];

        for (int row = 0; row < tempGrid.Length; row++)
        {
            var rowContents = tempGrid[row].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int column = 0; column < rowContents.Length; column++)
            {
                result[row, column] = int.Parse(rowContents[column]);
            }
        }

        return result;
    }


    public static int CalculateMaxProduct(this string gridContents, int GROUP_SIZE = 4)
    {
        var grid = ParseGrid(gridContents);
        var result = 0;
        var rows=grid.GetLength(0);
        var columns=grid.GetLength(1);

        for (int row = 0; row < rows; row++)
            for (int column = 0; column < columns; column++)
            {          
                var enoughColumns = column < (columns - GROUP_SIZE + 1);
                var enoughRows = row < (rows - GROUP_SIZE + 1);

                if (enoughColumns)
                    result = Math.Max(result, grid.CalculateHorizontalProduct(row, column, GROUP_SIZE));

                if (enoughRows) 
                    result = Math.Max(result, grid.CalculateVerticalProduct(row, column, GROUP_SIZE));

                if (enoughColumns && enoughRows) 
                {
                    result = Math.Max(result, grid.CalculateDiagonalRightProduct(row, column, GROUP_SIZE));
                    result = Math.Max(result, grid.CalculateDiagonalLeftProduct( row, column, GROUP_SIZE));
                }
            }
        return result;
    }


    public static int CalculateHorizontalProduct(this int[,] grid, int row, int column, int groupSize)
    {
        var result = 1;

        for (int i = column; i < column + groupSize; i++)
        {
            result *= grid[row, i];
        }
        return result;
    }

    public static int CalculateVerticalProduct(this int[,] grid, int row, int column, int groupSize)
    {
        var result = 1;

        for (int i = row; i < row + groupSize; i++)
        {
            result *= grid[i, column];
        }
        return result;
    }

    public static int CalculateDiagonalRightProduct(this int[,] grid, int row, int column, int groupSize)
    {
        var result = 1;

        for (int i = 0; i < groupSize; i++)
        {
            result *= grid[row + i, column + i];

        }

        return result;
    }

    public static int CalculateDiagonalLeftProduct(this int[,] grid, int row, int column, int groupSize)
    {
        var result = 1;

        for (int i = 0; i < groupSize; i++)
        {
            result *= grid[row + i, column + groupSize - i - 1];
        }

        return result;
    }

}