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
        paramFront = "���Ѿ���";
        paramBehind = "sû�к�ˮ��Ŷ����ȱ�ˮ��";
    }
    protected override void Remind()
    {
        text.text = $"����{DateTime.Now.ToLongTimeString()}," + paramFront + waitTime.ToString() + paramBehind;
        pageController.RequireToShow(pageElement.index);
    }
}
