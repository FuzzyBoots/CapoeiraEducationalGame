using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleSlot : MonoBehaviour, IDropHandler
{
    private int _h, _v;

    public void SetHV(int h, int v)
    {
        _h = h;
        _v = v;
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject _droppedObject = eventData.pointerDrag;

        if (_droppedObject.TryGetComponent<DraggablePuzzlePiece>(out DraggablePuzzlePiece piece))
        {
            Vector2 pieceHV = piece.GetHV();
            if (pieceHV.x == _h && pieceHV.y == _v)
            {
                // Locked in
                Debug.Log("Match");
                // Eventually, we'll want to communicate this
                // Should we then disable the puzzle piece?

                piece.SetPosition(GetComponent<RectTransform>().localPosition);
            }
        }        
    }
}
