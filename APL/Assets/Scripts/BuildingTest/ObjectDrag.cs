using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 pos;
    private Vector3 setpos;

    private float yPos;
    private void Start()
    {
        yPos = transform.position.y;
    }
    private void OnMouseDown()
    {
        offset = transform.position - BuildingSystem.GetMouseWorldPosition();
        
       
    }

    private void OnMouseDrag()
    {
        pos = BuildingSystem.GetMouseWorldPosition() /*+ offset*/;

        Vector3 mouseCellPos3d = BuildingSystem.current.SnapCoordinateToGrid(pos);
        Vector2 mouseCellPos2d = new Vector2(mouseCellPos3d.x, mouseCellPos3d.z);
        Debug.Log("Mouse cell pos 2d: " + mouseCellPos2d);

        Vector3 objectCellPos3d = BuildingSystem.current.SnapCoordinateToGrid(transform.position);
        Vector2 objectCellPos2d = new Vector2(objectCellPos3d.x, objectCellPos3d.z);

        Debug.Log("Object cell pos 2d:" + objectCellPos2d);

        if (mouseCellPos2d == objectCellPos2d)
        {
            return;
        }
        setpos = new Vector3(mouseCellPos3d.x, yPos, mouseCellPos3d.z);
        transform.position = setpos; 
        Debug.Log("On mouse drag");
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, offset);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.1f);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(offset, 0.1f);
    }
}
