using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DrawTool : EditorWindow
{
    private GameObject obj;
    private static int size = 12;
    private Color firstColor;
    private Color secondColor;
    private Color currentColor;
    private Vector2[,] textures = new Vector2[size,size];
    private Color[,] colors = new Color[size, size];
    private int boxX;
    private int boxY;
    public Texture _texture;
    public Texture2D outTexture;
    private Event event_Mouse;



    [MenuItem("Tools/Draw tool")]
    private static void OpenDrawToolWindow()
    {
        GetWindow<DrawTool>(focusedWindow);
    }

    private void OnEnable()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                colors[i,j] = Color.white;
                textures[i, j] = new Vector2(boxX, boxY);
            }
        }
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal(); 
        { 
            GUILayout.BeginVertical(); 
            {
                GUILayout.BeginArea(new Rect(0,0,360,500));
                GUILayout.Label("First Color");
                firstColor = EditorGUILayout.ColorField(firstColor);
                Debug.Log(firstColor);
                GUILayout.Label("Second Color");
                secondColor = EditorGUILayout.ColorField(secondColor);
                Debug.Log(secondColor);
                obj = (GameObject)EditorGUILayout.ObjectField(obj,typeof(GameObject));
                if (GUILayout.Button("Save texture"))
                {
                    MeshRenderer MeshRend = obj.GetComponent<MeshRenderer>();
                    outTexture = new Texture2D(size,size);
                    for (int i = 0; i < size; i++)
                    {
         
                        for (int j = 0; j < size; j++)
                        {
                            outTexture.SetPixel(i,j,colors[i,j]);
                        }
         
                    }
                    outTexture.Apply();
                    MeshRend.sharedMaterial.mainTexture = outTexture;

                }
                if (GUILayout.Button( "Fill")) 
                {
                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            colors[i,j] = firstColor;
                        }
                    }

                }
                GUILayout.EndArea();
            }
            GUILayout.EndVertical();
            GUI.color = Color.white;
            GUI.Box(new Rect(360, 0,590,590), GUIContent.none);
            currentColor = GUI.color;
            for (int i = 0; i < size; i++)
            {
                {
                    for (int j = 0; j < size; j++) 
                    {
                        boxX = i * (40 + 10) + 360;
                        boxY = j * (40 + 10);
                        GUI.color = colors[i,j];
                        { 
                            GUI.color = colors[i,j];
                        }
                        textures[i, j] = new Vector2(boxX, boxY);
                        GUI.DrawTexture(new Rect(boxX, boxY, 40,40), _texture);
                    }
                }
            }
        }
        GUILayout.EndHorizontal();
        
        GUI.color = currentColor;
        Event event_mouse = Event.current;
        if (event_mouse.type == EventType.MouseDown)
        {
            switch (event_mouse.button)
            {
                case (0):
                {
                    ChangeColor(firstColor, event_mouse.mousePosition);
                    break;
                }
                case (1):
                {
                    ChangeColor(secondColor, event_mouse.mousePosition);
                    break;
                }
            }

        }
        
        
        /*Event event_mouse = Event.current;
        
        if (event_mouse.type == EventType.MouseDown)
        {
            switch (event_mouse.button)
            {
                case (0):
                {
                    ChangeColor(firstColor, event_mouse.mousePosition);
                    //Debug.Log(firstColor);
                    break;
                }
                case (1):
                {
                    ChangeColor(secondColor, event_mouse.mousePosition);
                    //Debug.Log(secondColor);
                    break;
                }
            }

        }*/


    }

    private void ChangeColor(Color color, Vector2 coord)
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (textures[i, j].x < coord.x && textures[i, j].x + 40 > coord.x && textures[i,j].y < coord.y && textures[i,j].y + 40 > coord.y )
                {
                    colors[i, j] = color;
                }
            }
        }
    }
}
