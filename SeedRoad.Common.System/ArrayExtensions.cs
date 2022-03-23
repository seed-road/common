namespace SeedRoad.Common.System;

public static class ArrayExtensions
{
    public static TDestination[,] Map<TSource, TDestination>(this TSource[,] source, Func<TSource, TDestination> mapper)
    {
        var rows = source.GetLength(0);
        var cols = source.GetLength(1);
        
        var destination = new TDestination[rows, cols];
        for (var i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                destination[i, j] = mapper.Invoke(source[i, j]);
            }
        }

        return destination;
    }
}