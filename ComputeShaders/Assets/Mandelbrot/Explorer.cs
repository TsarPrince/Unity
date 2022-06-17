using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : MonoBehaviour
{
    public Material mat;
    public Vector2 pos;
    public float scale, angle;
    private Vector2 smoothPos;
    private float smoothScale, smoothAngle;

    void FixedUpdate()
    {
        HandleInputs();
        UpdateShader();
    }

    private void UpdateShader()
    {
        smoothPos = Vector2.Lerp(smoothPos, pos, 0.1f);
        smoothScale = Mathf.Lerp(smoothScale, scale, 0.1f);
        smoothAngle = Mathf.Lerp(smoothAngle, angle, 0.1f);

        float aspect = (float)Screen.width / (float)Screen.height;
        float scaleX = smoothScale;
        float scaleY = smoothScale;
        if (aspect > 1)
        {
            scaleY /= aspect;
        }
        else
        {
            scaleX *= aspect;
        }
        mat.SetVector("_Area", new Vector4(smoothPos.x, smoothPos.y, scaleX, scaleY));
        mat.SetFloat("_Angle", smoothAngle);
    }

    private void HandleInputs ()
    {
        // Adjust scale via +/-
        if (Input.GetKey(KeyCode.Equals))
        {
            scale *= 0.99f;
            angle += 0.01f;
        }
        if (Input.GetKey(KeyCode.Minus))
        {
            scale *= 1.01f;
            angle -= 0.01f;
        }

        // Adjust angle via Q/E
        if (Input.GetKey(KeyCode.Q))
        {
            angle -= 0.01f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            angle += 0.01f;
        }

        Vector2 dir = new Vector2(-.01f * scale, 0);
        float s = Mathf.Sin(angle);
        float c = Mathf.Cos(angle);
        dir = new Vector2(dir.x * c - dir.y * s, dir.x * s + dir.y * c);

        // Adjust position by W/A/S/D
        if (Input.GetKey(KeyCode.A))
        {
            pos += dir;
        }
        if (Input.GetKey(KeyCode.D))
        {
            pos -= dir;
        }
        dir = new Vector2(-dir.y, dir.x);
        if (Input.GetKey(KeyCode.W))
        {
            pos -= dir;
        }
        if (Input.GetKey(KeyCode.S))
        {
            pos += dir;
        }

    }
}
