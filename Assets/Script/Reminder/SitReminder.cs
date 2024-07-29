using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SitReminder : BaseReminder
{
    private new string paramFront;
    private new string paramBehind;
    SitReminder()
    {
        paramFront = "你已经有";
        paramBehind = "s没有活动了哦，快起来活动一下吧";
    }
    protected override void Remind()
    {
        text.text = $"截至{DateTime.Now.ToLongTimeString()}," + paramFront + waitTime.ToString() + paramBehind;
        pageController.RequireToShow(pageElement.index);
    }
}
