using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

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

    public static void EnsureVisibility(this ScrollRect scrollRect, RectTransform child, float padding = 0)
    {
        Debug.Assert(child.parent == scrollRect.content,
            "EnsureVisibility assumes that 'child' is directly nested in the content of 'scrollRect'");

        float viewportHeight = scrollRect.viewport.rect.height;
        Vector2 scrollPosition = scrollRect.content.anchoredPosition;

        float elementTop = child.anchoredPosition.y;
        float elementBottom = elementTop - child.rect.height;

        float visibleContentTop = -scrollPosition.y - padding;
        float visibleContentBottom = -scrollPosition.y - viewportHeight + padding;

        float scrollDelta =
            elementTop > visibleContentTop ? visibleContentTop - elementTop :
            elementBottom < visibleContentBottom ? visibleContentBottom - elementBottom :
            0f;

        scrollPosition.y += scrollDelta;
        scrollRect.content.anchoredPosition = scrollPosition;
    }
}