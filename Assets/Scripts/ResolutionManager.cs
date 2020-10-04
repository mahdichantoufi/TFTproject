using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    public int width;
    public int height;

    public void SetWidth(int width)
    {
        this.width = width;
    }

    public void SetHeight(int height)
    {
        this.height = height;
    }

    public void SetRes()
    {
        UnityEngine.Debug.Log(this.width + " " + this.height);
        Screen.SetResolution(this.width, this.height, false);
        UnityEngine.Debug.Log(Screen.width + "   " + Screen.height);
    }

}
