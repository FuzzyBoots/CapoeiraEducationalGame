using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggablePuzzlePiece : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Image _image;

    private int _h, _v;

    public void SetHV(int h, int v)
    {
        _h = h;
        _v = v;
    }

    public Vector2 GetHV()
    {
        return new Vector2(_h, _v);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Color temp = _image.color;
        temp.a = 0.5f;
        _image.color = temp;
        _image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Color temp = _image.color;
        temp.a = 1f;
        _image.color = temp;
        _image.raycastTarget = true;
    }

    public void SetPosition(Vector3 position)
    {
        GetComponent<RectTransform>().localPosition = position;
    }

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
    }
}
