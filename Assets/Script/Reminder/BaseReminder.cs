using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseReminder : MonoBehaviour
{
    public virtual string paramFront { get; set; }
    public virtual string paramBehind { get; set; }
    public float waitTime = 10;
    public bool active = false;
    public PageController pageController;
    public PageElement pageElement;
    public TextMeshProUGUI text;
    public TextMeshProUGUI label;
    public InputField inputField;
    public Toggle btn;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    protected virtual void Remind()
    {
        text.text = paramFront + waitTime.ToString() + paramBehind;
        pageController.RequireToShow(pageElement.index);
    }
    public void StartReminder()
    {
        if (!active) return;
        InvokeRepeating("Remind", waitTime, waitTime);
    }
    public void RestartRemind()
    {
        CancelInvoke("Remind");
        if (!active) return;
        InvokeRepeating("Remind", waitTime, waitTime);
    }

    public void StopRemind()
    {
        CancelInvoke("Remind");
    }

    public void SetConfig(ReminderConfig config)
    {
        waitTime = config.time;
        active = config.active;
        RestartRemind();
    }

}
