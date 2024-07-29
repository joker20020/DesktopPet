using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MovableObj : MonoBehaviour
{
    private bool isDragging = false;
    private Collider2D collider2d;

    // Update is called once per frame
    void Update()
    {
        
        // ÍÏ¶¯¼ì²â
        if (Input.GetMouseButton(0) && !isDragging)
        {
            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            collider2d = Physics2D.OverlapPoint(clickPos);
            if (collider2d != null && collider2d.transform == transform)
            {
                StartDragging();
            }
        }
        else if (!Input.GetMouseButton(0) && isDragging)
        {
            StopDragging();
        }

        if (isDragging)
        {
            DragToMousePosition();
        }
    }
    // ÍÏ×§º¯Êý
    void StartDragging()
    {
        isDragging = true;

    }

    void StopDragging()
    {
        isDragging = false;
    }

    void DragToMousePosition()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        collider2d.transform.position = position;
        //Debug.Log(position);
    }
    public bool GetDraggingState() 
    {
        return isDragging;
    }
}
