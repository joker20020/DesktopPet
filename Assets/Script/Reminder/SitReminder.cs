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
        paramFront = "���Ѿ���";
        paramBehind = "sû�л��Ŷ���������һ�°�";
    }
    protected override void Remind()
    {
        text.text = $"����{DateTime.Now.ToLongTimeString()}," + paramFront + waitTime.ToString() + paramBehind;
        pageController.RequireToShow(pageElement.index);
    }
}
