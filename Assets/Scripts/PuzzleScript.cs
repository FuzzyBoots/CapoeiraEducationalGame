using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleScript : MonoBehaviour
{
    private void Start()
    {
        SegmentAndPlace(GetComponent<Image>(), 10, 10);
    }

    public void SegmentAndPlace(Image wholeImage, int x, int y)
    {
        if (wholeImage == null) return;

        Transform parent = wholeImage.transform.parent;
        Texture2D texture = wholeImage.sprite.texture;

        Vector3 position = wholeImage.GetComponent<RectTransform>().localPosition;
        Vector3 dimensions = wholeImage.GetComponent<RectTransform>().sizeDelta;
        Debug.Log($"{position} - {dimensions}");

        float sliceX = texture.width / x;
        float sliceY = texture.height / y;

        for (int h = 0; h < x; h++)
        {
            for (int v = 0; v < y; v++)
            {
                GameObject newGameObject = new GameObject();
                newGameObject.name = $"Slice {h} {v}";
                newGameObject.transform.parent = parent;
                Image image = newGameObject.AddComponent<Image>();

                Rect rect = new Rect(h * sliceX, v * sliceY, sliceX, sliceY);

                Sprite newSprite = Sprite.Create(texture, rect, Vector2.one / 2f);
                newSprite.name = $"Sprite {h} {v}";
                image.sprite = newSprite;
                RectTransform rectTransform = newGameObject.GetComponent<RectTransform>();
                rectTransform.localPosition = position + new Vector3(h * dimensions.x / x - dimensions.x / 2f, v * dimensions.y / y - dimensions.x / 2f, 0);
                rectTransform.sizeDelta = new Vector3(dimensions.x / x, dimensions.y / y, 1);
            }
        }

        wholeImage.enabled = false;
    }
}
