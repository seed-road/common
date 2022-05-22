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

    public static List<List<T>> ToListOfList<T>(this T[,] source)
    {
        var returnedList = new List<List<T>>();
        for (var i = 0; i <= source.GetUpperBound(0); i++)
        {
            var row = new List<T>();
            returnedList.Add(row);
            for (var j = 0; j <= source.GetUpperBound(1); j++)
            {
                row.Add(source[i, j]);
            }
        }

        return returnedList;
    }

    public static Boolean Contains<T>(this T[,] source, T element)
    {
        if (element == null) return false;
        for (var i = 0; i <= source.GetUpperBound(0); i++)
        {
            for (var j = 0; j <= source.GetUpperBound(1); j++)
            {
                if (element.Equals(source[i, j])) return true;
            }
        }

        return false;
    }
}