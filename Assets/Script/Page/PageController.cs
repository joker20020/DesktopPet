using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour
{
    public List<PageElement> pages;
    private OrderedPriorityQueue<int> indexQueue = new OrderedPriorityQueue<int>();
    private int currentPageIndex = -1;

    private void Start()
    {
        RegisterPages();
    }

    // 页面请求显示
    public void RequireToShow(int pageIndex)
    {
        if (pageIndex == currentPageIndex) return;
        // 无激活页面直接显示
        if(currentPageIndex == -1)
        {
            pages[pageIndex].gameObject.SetActive(true);
            currentPageIndex = pageIndex;
        }
        else
        {
            // 高优先级中断
            if (pages[pageIndex].priority < pages[currentPageIndex].priority)
            {
                pages[currentPageIndex].gameObject.SetActive(false);
                currentPageIndex = pageIndex;
                pages[currentPageIndex].gameObject.SetActive(true);
            }
            else
            {
                indexQueue.Enqueue(pageIndex, pages[pageIndex].priority);
            }
        }
    }

    public void RequireToClose()
    {
        if (currentPageIndex == -1) { return; }
        pages[currentPageIndex].gameObject.SetActive(false);
        if (indexQueue.Count == 0)
        {
            currentPageIndex = -1;
        }
        else
        {
            currentPageIndex = indexQueue.Dequeue();
            pages[currentPageIndex].gameObject.SetActive(true);
        }
    }

    public void RegisterPages()
    {
        int i = 0;
        foreach (PageElement page in pages)
        {
            page.index = i;
            i++;
        }
    }

}

public class OrderedPriorityQueue<T>
{
    private List<int> priorities = new List<int>();
    private List<T> values = new List<T>();
    public int Count { get { return values.Count; } }

    public void Enqueue(T item, int priority)
    {
        if(priorities.Count == 0)
        {
            priorities.Add(priority);
            values.Add(item);
        }
        else
        {
            if(priority > priorities[priorities.Count-1])
            {
                priorities.Add(priority);
                values.Add(item);
            }
            else if(priority < priorities[0])
            {
                priorities.Insert(0, priority);
                values.Insert(0, item);
            }
            else
            {
                int inserIndex = priorities.LastIndexOf(priority);
                priorities.Insert(inserIndex + 1, priority);
                values.Insert(inserIndex + 1, item);
            }
        }
        
    }

    public T Dequeue() 
    { 
        T item = values[0];
        values.RemoveAt(0);
        priorities.RemoveAt(0);
        return item;
    }

    public void Clear() { priorities.Clear(); values.Clear(); }

    
}
