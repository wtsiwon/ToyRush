using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReSolution : MonoBehaviour
{
    private void Awake()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;

        float scaleheight = ((float)Screen.width / Screen.height) / (18.5f / 9);
        float scalewidth = 1f / scaleheight;

        if(scaleheight < 1)
        {
            rect.height = scaleheight;
            rect.y = (1f - scaleheight) / 2f;
        }
        else
        {
            rect.width = scalewidth;
            rect.x = (1f - scalewidth) / 2f;
        }

        camera.rect = rect;
    }
}
