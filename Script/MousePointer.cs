using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 커서 이미지 바꾸기

public class MousePointer : MonoBehaviour
{
    public Texture2D cursorTexture_A;
    public Texture2D cursorTexture_B;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 mousePos = Vector2.zero;

    public UI_Manager ui_manager;

    void Start()
    {

    }

    void Update()
    {
        ui_manager = GameObject.FindObjectOfType<UI_Manager>();


        if (ui_manager.Mode[0].activeSelf || ui_manager.Mode[1].activeSelf) // 설치 + 편집모드일때
        {
            ChangeMouse_B();
        }

        else
        {
            ChangeMouse_A();
        }        
    }

    public void ChangeMouse_A() // 화살표
    {
        Cursor.SetCursor(cursorTexture_A, mousePos, cursorMode);
    }

    public void ChangeMouse_B() // 망치
    {
        Cursor.SetCursor(cursorTexture_B, mousePos, cursorMode);
    }

}
