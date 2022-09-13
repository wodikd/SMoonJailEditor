// Author : hellozzi@gmail.com
// github : https://github.com/Helloezzi
// site : https://helloezzi.tistory.com/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MyGrid : MonoBehaviour
{
    public enum Direction
    {
        DirX,
        DirY,
        DirZ,
    }


    public int row;

    public int col;

    public int highlight;

    public Vector3 rowSize;
    public Vector3 colSize;
    public Vector3 gridSize;

    public Color LineColor;

    public Direction MyDirection;

    static Material lineMaterial;
    private readonly string[] includeCametaArray = { "InGameCamera", };
    private Camera curCamera;
    static void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            lineMaterial.SetInt("_ZWrite", 0);
        }
    }

    public void OnDrawGizmos()
    {
        // Draw Scene
        CreateLineMaterial();
        lineMaterial.SetPass(0);

        GL.PushMatrix();
        DrawGrid();
        GL.PopMatrix();
    }

    private void OnRenderObject()
    {
        for (int i = 0; i < includeCametaArray.Length; i++)
        {
            curCamera = Camera.current;
            if (curCamera.name != includeCametaArray[i])
            {
                return;
            }
        }

        CreateLineMaterial();
        lineMaterial.SetPass(0);

        GL.PushMatrix();

        DrawGrid();

        GL.PopMatrix();
    }

    private Color GetAlphaDistance(float i)
    {
        double alpha = 1f;
        if (i < 0)
            i = i * -1;

        if (i == 0)
            alpha = 1f;

        double temp = (double)i / (double)row;
        alpha = temp * 100f;
        alpha = 1 - (alpha / 100);

        if (alpha > 0.8)
            alpha = 0.8f;

        return new Color(LineColor.r, LineColor.g, LineColor.b, (float)alpha);
    }

    public void DrawGrid()
    {
        GL.Begin(GL.LINES);
        GL.Color(LineColor);

        for (int i = -row; i <= row; i++)
        {
            GL.Color(GetColor(i));
            var y = i * rowSize.y;
            GL.Vertex3(-rowSize.x, y, 0);
            GL.Vertex3(rowSize.x, y, 0);
        }

        // col
        for (int i = -col; i <= col; i++)
        {
            GL.Color(GetColor(i));
            var x = i * colSize.x;
            GL.Vertex3(x, -colSize.y, 0);
            GL.Vertex3(x, colSize.y, 0);
        }

        #region 
        // (변수) = 선 길이
        // i = 오프셋

        //if (direction == Direction.DirX)
        //{
        //    // row
        //    for (int i = -row; i <= row; i++)
        //    {
        //        GL.Color(GetAlphaDistance(i));
        //        GL.Vertex3((float)-row * gridSize.x, 0, (float)i * gridSize.z);
        //        GL.Vertex3((float)row * gridSize.x, 0, (float)i * gridSize.z);
        //    }

        //    // col
        //    for (int i = -col; i <= col; i++)
        //    {
        //        GL.Color(GetAlphaDistance(i));
        //        GL.Vertex3((float)i * gridSize.x, 0, (float)-col * gridSize.z);
        //        GL.Vertex3((float)i * gridSize.x, 0, (float)col * gridSize.z);
        //    }
        //}
        //else if (direction == Direction.DirY)
        //{
        //    // row
        //    for (int i = -row; i <= row; i++)
        //    {
        //        GL.Color(GetColor(i, 5));
        //        GL.Vertex3((float)-row, (float)i * gridSize.y, 0);
        //        GL.Vertex3((float)row, (float)i * gridSize.y, 0);
        //    }

        //    // col
        //    for (int i = -col; i <= col; i++)
        //    {
        //        GL.Color(GetColor(i, 5));
        //        GL.Vertex3((float)i * gridSize.x, (float)-col, 0);
        //        GL.Vertex3((float)i * gridSize.x, (float)col, 0);
        //    }
        //}
        //else if (direction == Direction.DirZ)
        //{
        //    // row
        //    for (int i = -row; i <= row; i++)
        //    {
        //        GL.Color(GetAlphaDistance(i));
        //        GL.Vertex3(0, (float)-row, (float)i * gridSize.z);
        //        GL.Vertex3(0, (float)row, (float)i * gridSize.z);
        //    }

        //    // col
        //    for (int i = -col; i <= col; i++)
        //    {
        //        GL.Color(GetAlphaDistance(i));
        //        GL.Vertex3(0, (float)i * gridSize.y, (float)-col);
        //        GL.Vertex3(0, (float)i * gridSize.y, (float)col);
        //    }
        //}
        #endregion

        GL.End();

        Color GetColor(int num)
        {
            float alpha = 0;
            curCamera = Camera.current;

            //float posZ = camera.transform.position.z;

            //float dis = curCamera.orthographicSize / highlight;

            alpha = num % 5 != 0 ? 0.5f : 1f;

            return new(LineColor.r, LineColor.g, LineColor.b, alpha);
        }
    }
}
