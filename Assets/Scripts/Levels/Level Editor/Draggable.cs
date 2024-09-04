using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject tile;
    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
	{
        this.transform.position = eventData.position;
	}
    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(eventData.position);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
        if (hit)
        {
            if (hit.collider.tag == "EmptyTile")
            {
                Debug.Log("JIJIJIJIJ");
            }
        }
    }

}
