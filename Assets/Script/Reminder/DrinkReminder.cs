using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DrinkReminder : BaseReminder
{
    private new string paramFront;
    private new string paramBehind;
    DrinkReminder()
    {
        paramFront = "你已经有";
        paramBehind = "s没有喝水了哦，快喝杯水吧";
    }
    protected override void Remind()
    {
        text.text = $"截至{DateTime.Now.ToLongTimeString()}," + paramFront + waitTime.ToString() + paramBehind;
        pageController.RequireToShow(pageElement.index);
    }
}
