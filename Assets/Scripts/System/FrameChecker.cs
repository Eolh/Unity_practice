using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameChecker : MonoBehaviour
{
    float deltaTime = 0.0f;
    GUIStyle style;
    Rect rect;
    float msec;
    float fps;
    float worstFps = 100f;
    string text;

    void Awake()
    {
        Application.targetFrameRate = 60; // frame fixed when don't use +Vsyn! 
        int w = Screen.width, h = Screen.height;
        rect = new Rect(0, 0, w, h * 4 / 100);
        style = new GUIStyle();
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 4 / 100;
        style.normal.textColor = Color.cyan;
        StartCoroutine("WorstReset");
    }

    IEnumerator WorstReset()// reset minimum frame per 15sec 
    {
        while (true)
        {
            yield return new WaitForSeconds(15f);
            worstFps = 100f;
        }
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }

    void OnGUI() // show GUI
    {
        msec = deltaTime * 1000.0f;
        fps = 1.0f / deltaTime; // frame -1 sec per sec

        if (fps < worstFps) // change worst fps to new minimun fps 
            worstFps = fps;

        text = msec.ToString("F1") + "ms (" + fps.ToString("F1") + ") // worst : " + worstFps.ToString("F1");
        GUI.Label(rect, text, style);

    }
}
