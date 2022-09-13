using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLine : MonoBehaviour
{
    public new Camera camera;
    public float radius;
    public int smoot;

    public float divideLength;

    private LineRenderer lineX;
    private LineRenderer lineY;
    private LineRenderer lineAngle;
    private LineRenderer lineCircle;

    // Start is called before the first frame update
    void Awake()
    {
        lineX = transform.Find("LineX").GetComponent<LineRenderer>();
        lineY = transform.Find("LineY").GetComponent<LineRenderer>();
        lineAngle = transform.Find("LineAngle").GetComponent<LineRenderer>();
        lineCircle = transform.Find("LineCircle").GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var cursorPos = (Vector2)camera.ScreenToWorldPoint(Input.mousePosition);
        DrawLine(cursorPos.normalized * radius);

        double degree = 360.0 / (smoot - 1);

        float radian = (float)(Mathf.PI / 180.0 * degree);

        List<Vector3> posList = new List<Vector3>();

        for (int i = 0; i < smoot + 1; i++)
        {
            var angle = new Vector2(Mathf.Cos(radian * i), Mathf.Sin(radian * i)) * radius;
            posList.Add(angle);
        }

        lineCircle.positionCount = smoot;
        lineCircle.SetPositions(posList.ToArray());
    }

    private void DrawLine(Vector2 pos)
    {
        float width = lineX.startWidth;
        lineX.material.mainTextureScale = new Vector2(1f / width, 1.0f);
        lineX.SetPositions(new Vector3[3] { Vector2.zero, Vector2.right * pos, pos });
        lineY.SetPositions(new Vector3[3] { Vector2.zero, Vector2.up * pos, pos });
        lineAngle.SetPosition(index: 1, pos);
    }
}