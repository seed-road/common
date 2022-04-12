namespace SeedRoad.Common.System;

public static class ListExtensions
{
    
    public static T[,] ToRectangularArray<T>(this List<List<T>> source)
    {
        if (!CouldBeConvertedToRectangularArray(source))
        {
            throw new InvalidCastException("List shall possess regular lengths to be converted");
        }

        if (source.Count == 0) return new T[0, 0]{ };
            
        var rectangularArray = new T[source.Count, source[0].Count];
        for (var i = 0; i < source.Count; i++)
        {
            for (int j = 0; j < source[i].Count; j++)
            {
                rectangularArray[i, j] = source[i][j];
            }
        }

        return rectangularArray;
    }
    
    private static bool CouldBeConvertedToRectangularArray<T>(List<List<T>> listOfLists)
    {
        if (listOfLists.Count == 0) return true;
        return listOfLists.All(list => list.Count == listOfLists[0].Count);
    }
}