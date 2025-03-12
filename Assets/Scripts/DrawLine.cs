using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;
using UnityEngine.EventSystems;

public class DrawLine : MonoBehaviour
{
    LineRenderer lr;
    Vector3 flagPos;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.startWidth = 0.03f;
        lr.endWidth = 0.03f;
        lr.startColor = Color.white;
        lr.endColor = Color.white;
        lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
    }

    void Draw()
    {  
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 2f;
        Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
        if (Input.GetMouseButtonDown(0))
        {
            lr.positionCount = 2;
            lr.SetPosition(0, flagPos);
        }
        if (Input.GetMouseButton(0))
        {
            lr.SetPosition(1, objectPos);
        }
        if (Input.GetMouseButtonUp(0))
        {
            lr.positionCount = 0;
        }

    }

    void OnMouseDown()
    {
        flagPos = this.transform.position;
        flagPos = new Vector3(flagPos.x - 0.25f, flagPos.y - 0.25f, flagPos.z);
        Draw();
    }
}
