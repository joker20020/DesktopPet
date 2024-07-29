using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.XR;

public class WindowController : MonoBehaviour
{
    [DllImport("user32.dll")]
    public static extern IntPtr SetCapture(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();

    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out POINT lpPoint);

    [DllImport("user32.dll")]
    public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

    [DllImport("user32.dll")]
    private static extern int SetWindowLongPtrA(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll")]
    static extern int GetWindowLongPtrA(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    static extern int SetLayeredWindowAttributes(IntPtr hwnd, int crKey, int bAlpha, int dwFlags);

    [DllImport("Dwmapi.dll")]
    static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

    public struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    public struct POINT
    {
        public int X;
        public int Y;
    }

    // setWindowPos 常量
    private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    private const uint SWP_NOSIZE = 0x0001;
    private const uint SWP_NOMOVE = 0x0002;
    private const uint TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

    // sendMessage用于拖动窗口的常量
    const int WM_SYSCOMMAND = 0x112;
    const int WM_NCLBUTTONDOWN = 0x00A1;
    const int SC_MOVE = 0xF010;
    const int HTCAPTION = 0x0002;

    // 窗口风格设置常量
    private const int GWL_EXSTYLE = -20;
    private const int GWL_STYLE = -16;
    private const int WS_EX_LAYERED = 0x00080000;
    private const int WS_EX_TOOLWINDOW = 0x00000080;
    private const int WS_BORDER = 0x00800000;
    private const int WS_CAPTION = 0x00C00000;
    private const int LWA_COLORKEY = 0x00000001;
    private const int LWA_ALPHA = 0x00000002;
    private const int WS_EX_TRANSPARENT = 0x20;

    // 窗口拖放文件设置
    private const int WS_EX_ACCEPTFILES = 0x00000010;

    // 过程参数
    private bool isDragging = false;
    private int sendResult = 0;
    private Vector2 initialMousePosition;
    private Vector2 initialWindowPosition;
    private IntPtr hwnd;

    void Start()
    {
        Application.runInBackground = true;
        hwnd = FindWindow(null, Application.productName); // 使用应用程序名称获取窗口句柄
        if (hwnd == IntPtr.Zero)
        {
            // Debug.LogError("窗口句柄未找到");
            return;
        }
        //int width = Screen.resolutions[Screen.resolutions.Length - 1].width / 5;
        //int height = Screen.resolutions[Screen.resolutions.Length - 1].height / 5;
        //Screen.SetResolution(width, height, false);
#if !UNITY_EDITOR

        // 窗口置顶
        SetWindowPos(hwnd, HWND_TOPMOST, Screen.mainWindowPosition.x, Screen.mainWindowPosition.y, Screen.width, Screen.height, TOPMOST_FLAGS);
        // Debug.Log("Window Handle: " + hwnd);

        // 设置为透明、无边框
        int intExTemp = GetWindowLongPtrA(hwnd, GWL_EXSTYLE);
        int intTemp = GetWindowLongPtrA(hwnd, GWL_STYLE);
        SetWindowLongPtrA(hwnd, GWL_EXSTYLE, (uint)intExTemp | WS_EX_LAYERED | WS_EX_ACCEPTFILES ); //| WS_EX_TOOLWINDOW
        SetWindowLongPtrA(hwnd, GWL_STYLE, (uint)(intTemp & ~WS_BORDER & ~WS_CAPTION));

        // 全屏模式下扩展窗口到客户端区域 -> 为了透明
        var margins = new MARGINS() { cxLeftWidth = -1 }; // 边距内嵌值确定在窗口四侧扩展框架的距离 -1为没有窗口边框
        DwmExtendFrameIntoClientArea(hwnd, ref margins);

        SetLayeredWindowAttributes(hwnd, 0x010101, 255, LWA_COLORKEY);
#endif
    }

    // 以下代码在小窗模式下使用Win32 api移动窗口
    void Update()
    {
        //if (Input.GetMouseButton(0) && !isDragging)
        //{
        //    StartDragging();
        //}
        //else if (!Input.GetMouseButton(0) && isDragging)
        //{
        //    StopDragging();
        //}

        //if (isDragging)
        //{
        //    DragWindowToMousePosition();
        //}
    }

    void StartDragging()
    {
        isDragging = true;
        initialMousePosition = GetMousePosition();
        //initialWindowPosition = new Vector2(Screen.mainWindowPosition.x + Screen.width / 2f, Screen.mainWindowPosition.y + Screen.height / 2f); // 初始窗口位置，可根据需要调整
        initialWindowPosition = new Vector2(Screen.mainWindowPosition.x, Screen.mainWindowPosition.y);
        ReleaseCapture();
        
    }

    void StopDragging()
    {
        isDragging = false;
        
    }

    void DragWindowToMousePosition()
    {
        Vector2 currentMousePosition = GetMousePosition();
        Vector2 delta = currentMousePosition - initialMousePosition;
        Vector2 targetPosition = initialWindowPosition + delta;
        POINT point = new POINT();
        point.X = (int)targetPosition.x;
        point.Y = (int)targetPosition.y;
        SetWindowPos(hwnd, HWND_TOPMOST, point.X, point.Y, Screen.width, Screen.height, SWP_NOSIZE);
        //SetWindowPos(hwnd, HWND_TOPMOST, point.X - Screen.width / 2, point.Y - Screen.height / 2, 0, 0, TOPMOST_FLAGS);
        //sendResult = SendMessage(hwnd, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, (point.Y << 16) | (point.X & 0xFFFF));
        SetCapture(hwnd);
    }

    public Vector2 getPoint()
    {
        Vector2 currentMousePosition = GetMousePosition();
        Vector2 delta = currentMousePosition - initialMousePosition;
        Vector2 targetPosition = initialWindowPosition + delta;
        return targetPosition;
    }

    public bool getDraging()
    {
        return isDragging;
    }
    public int getSend()
    {
        return sendResult;
    }

    Vector2 GetMousePosition()
    {
        POINT point;
        GetCursorPos(out point);
        return new Vector2(point.X, point.Y);
    }

}
