using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageElement : MonoBehaviour
{
    public int priority;
    public int index;

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    PageElement()
    {
        priority = 0;
        index = 0;
    }
}
