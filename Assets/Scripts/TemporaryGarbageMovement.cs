using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryGarbageMovement : MonoBehaviour {

    float distance = 10;

    void OnMouseDrag()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = objPos;
    }

}
