using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    public Gradient gradient;

    public float time;
    public float speed;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        time += speed;
        time %= 1;

        spriteRenderer.color = gradient.Evaluate(time);
    }
}
