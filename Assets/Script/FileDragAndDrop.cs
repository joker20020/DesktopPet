using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using B83.Win32;
using Unity.VisualScripting;


public class FileDragAndDrop : MonoBehaviour
{
    private static string file = "no file";
    // dialog所属对象
    public DeleteDialog dialog;

    private static bool warning = false;
    private static bool open = false;

    void OnEnable ()
    {
        // must be installed on the main thread to get the right thread id.
        UnityDragAndDropHook.InstallHook();
        UnityDragAndDropHook.OnDroppedFiles += OnFiles;
    }
    void OnDisable()
    {
        UnityDragAndDropHook.UninstallHook();
    }

    void Update()
    {
        if (warning)
        {
            warning = false;
            dialog.warning("一次只能拖拽一个文件哦！");
        }
        else if (open)
        {
            open = false;
            dialog.open(file);
        }
    }

    void OnFiles(List<string> aFiles, POINT aPos)
    {
        //string str = "Dropped " + aFiles.Count + " files at: " + aPos + "\n\t" +
        //    aFiles.Aggregate((a, b) => a + "\n\t" + b);
        //Debug.Log(str);
        if (aFiles.Count > 1) 
        {
            warning = true;
            return;
        }
        file = aFiles[0];
        // 打开删除文件对话框
        open = true;
    }

    public string getFile()
    {
        return file;
    }
}
