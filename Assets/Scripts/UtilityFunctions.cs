using System.Collections.Generic;

static class UtilityFunctions
{
    public static void ShuffleList<T>(List<T> list, int numberOfShuffles = -1)
    {
        if (numberOfShuffles < 0 || numberOfShuffles > list.Count)
        {
            numberOfShuffles = list.Count;
        }

        for (int i = numberOfShuffles - 1; i > 0; i--)
        {
            int randIndex = UnityEngine.Random.Range(0, i - 1);

            T swap = list[i];
            list[i] = list[randIndex];
            list[randIndex] = swap;
        }
    }
}