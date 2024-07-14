using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class PuzzleScript : MonoBehaviour
{
    [SerializeField] GameObject _puzzleSlot;
    [SerializeField] Sprite _puzzleSprite;

    [SerializeField, Range(1, 10)] int _horizontalPieces = 3;
    [SerializeField, Range(1, 10)] int _verticalPieces = 3;

    RectTransform _puzzleRectTransform;

    private void Start()
    {
        _puzzleRectTransform = GetComponent<RectTransform>();
        SegmentAndPlace(_puzzleSprite, _horizontalPieces, _verticalPieces);
    }

    public void SegmentAndPlace(Sprite puzzleImage, int x, int y)
    {
        Transform parent = transform;
        Texture2D texture = puzzleImage.texture;

        Vector3 position = _puzzleRectTransform.localPosition;
        Vector3 dimensions = _puzzleRectTransform.sizeDelta;

        float sliceX = texture.width / x;
        float sliceY = texture.height / y;

        GameObject slots = new GameObject();
        slots.name = "Slots";
        GameObject pieces = new GameObject();
        pieces.name = "Pieces";
        
        slots.transform.parent = parent;
        slots.transform.localPosition = Vector3.zero;
        pieces.transform.parent = parent;
        pieces.transform.localPosition = Vector3.zero;

        for (int h = 0; h < x; h++)
        {
            for (int v = 0; v < y; v++)
            {
                GameObject newGameObject = new GameObject();
                newGameObject.name = $"Slice {h} {v}";
                newGameObject.transform.parent = pieces.transform;
                Image image = newGameObject.AddComponent<Image>();
                DraggablePuzzlePiece piece = newGameObject.AddComponent<DraggablePuzzlePiece>();
                piece.SetHV(h, v);

                Rect rect = new Rect(h * sliceX, v * sliceY, sliceX, sliceY);

                Sprite newSprite = Sprite.Create(texture, rect, Vector2.one / 2f);
                newSprite.name = $"Sprite {h} {v}";
                image.sprite = newSprite;
                
                RectTransform rectTransform = newGameObject.GetComponent<RectTransform>();
                rectTransform.localPosition = position + new Vector3(Random.Range(0, dimensions.x), Random.Range(0, dimensions.y), 0);
                rectTransform.sizeDelta = new Vector3(dimensions.x / x, dimensions.y / y, 1);

                GameObject newPuzzleSlot = Instantiate(_puzzleSlot);
                newPuzzleSlot.name = $"Slot {h} {v}";
                newPuzzleSlot.transform.parent = slots.transform;
                
                PuzzleSlot slot = newPuzzleSlot.AddComponent<PuzzleSlot>();
                slot.SetHV(h, v);

                rectTransform = newPuzzleSlot.GetComponent<RectTransform>();
                rectTransform.localPosition = position + new Vector3((h+0.5f) * dimensions.x / x - dimensions.x / 2, (v + 0.5f) * dimensions.y / y - dimensions.y/2, 0);
                rectTransform.sizeDelta = new Vector3(dimensions.x / x, dimensions.y / y, 1);
            }
        }
    }
}
