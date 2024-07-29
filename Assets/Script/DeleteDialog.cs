using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class DeleteDialog : MonoBehaviour
{
    public GameObject g;
    public PageController pageController;
    private TextMeshProUGUI text;
    string file = "";
    // Start is called before the first frame update
    void Start()
    {
        text = g.GetComponent<TextMeshProUGUI>();
        close();
    }

    public void deleteFile()
    {
        if (File.Exists(file))
        {
            File.Delete(file);
            close();
        }
        else if (Directory.Exists(file))
        {
            Directory.Delete(file);
            close();
        }
        else
        {
            text.text = $"{file}²»´æÔÚ";
        }
    }

    public void open(string fileName)
    {
        file = fileName;
        text.text = $"ÊÇ·ñÉ¾³ý{file}";
        pageController.RequireToShow(this.gameObject.GetComponent<PageElement>().index);
    }

    public void warning(string message)
    {
        text.text = $"{message}";
        pageController.RequireToShow(this.gameObject.GetComponent<PageElement>().index);
    }

    public void close()
    {
        pageController.RequireToClose();
    }
}
