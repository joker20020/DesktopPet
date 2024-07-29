using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class ReminderConfig
{
    public bool active;
    public float time;
}
public class Config
{
    public List<ReminderConfig> reminders;
}
