using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapManager : MonoBehaviour
{
    public LineRenderer line;
    public new BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        if (line == null)
        {
            line = GetComponent<LineRenderer>();
        }
        if (collider == null)
        {
            collider = GetComponent<BoxCollider2D>();
        }
    }

    public void SetBoxSize(Vector2 vector)
    {
        
    }
}
