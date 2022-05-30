namespace SeedRoad.Common.System;

public static class ArrayExtensions
{
    public record Index(int X, int Y)
    {
        public IEnumerable<ArrayExtensions.Index> GetNeighbors()
        {
            return new[]
            {
                new Index(X, Y - 1),
                new Index(X, Y + 1),
                
                new Index(X - 1, Y - 1),
                new Index(X - 1, Y),
                new Index(X - 1, Y + 1),
                
                new Index(X + 1, Y - 1),
                new Index(X + 1, Y),
                new Index(X + 1, Y + 1),
            };
        }
    }

    public static bool IsIn<T>(this T[,] source, Index index)
    {
        if (index.X > source.GetUpperBound(1) || index.X < 0) return false;
        if (index.Y > source.GetUpperBound(0) || index.Y < 0) return false;

        return true;
    }
    
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

    public static void ForEach<T>(this T[,] source, Action<T> callback)
    {
        
        for (var i = 0; i <= source.GetUpperBound(0); i++)
        {
            for (var j = 0; j <= source.GetUpperBound(1); j++)
            {
                callback(source[i, j]);
            }
        }
    }
    
    public static void ForEach<T>(this T[,] source, Action<T, Index> callback)
    {
        
        for (var i = 0; i <= source.GetUpperBound(0); i++)
        {
            for (var j = 0; j <= source.GetUpperBound(1); j++)
            {
                callback(source[i, j], new Index(j, i));
            }
        }
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

    public static ISet<T> GetUniques<T>(this T[,] source)
    {
        var set =  new HashSet<T>();
        for (var i = 0; i <= source.GetUpperBound(0); i++)
        {
            for (var j = 0; j <= source.GetUpperBound(1); j++)
            {
                set.Add(source[i, j]);
            }
        }

        return set;
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