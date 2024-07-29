using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public List<BaseReminder> reminders;
    // public GameObject menu;
    public PageController pageController;
    public PageElement pageElement;
    // Start is called before the first frame update
    void Start()
    {
        ShowConfigure();
        InitReminder();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(1))
        {
#if UNITY_EDITOR
            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collider2d = Physics2D.OverlapPoint(clickPos);
            
            if (collider2d != null)
            {
                pageController.RequireToShow(pageElement.index);
            }
#else
            pageController.RequireToShow(pageElement.index);
#endif

        }
        else if (Input.GetMouseButtonDown(0)) 
        {
            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collider2d = Physics2D.OverlapPoint(clickPos);
            
            if (collider2d == null)
            {
                pageController.RequireToClose();
            }
        }
    }

    // ������չʾ��UI
    public void ShowConfigure()
    {
        string filePath = Application.dataPath + "/Config/config.json";
        Config config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(filePath));
        int i = 0;
        foreach (ReminderConfig reminderConfig in config.reminders)
        {
            reminders[i].label.text = $"ʱ��:{reminderConfig.time}s\n����:{reminderConfig.active}";
            i++;
        }
    }

    // ��������
    public void SetConfigure()
    {
        string filePath = Application.dataPath + "/Config/config.json";
        //Debug.Log(filePath);
        Config config = new Config();
        config.reminders = new List<ReminderConfig>();
        foreach(BaseReminder reminder in reminders) 
        {
            // ��ȡ����
            ReminderConfig reminderConfig = new ReminderConfig();
            reminderConfig.active = reminder.btn.isOn;
            string timeToSet = reminder.inputField.text;
            if(timeToSet.Length > 0)
            {
                reminderConfig.time = float.Parse(timeToSet);
            }
            else
            {
                reminderConfig.time = reminder.waitTime;
            }
            reminder.SetConfig(reminderConfig);
            config.reminders.Add(reminderConfig);
        }
        // д���ļ�
        string file = JsonConvert.SerializeObject(config);
        File.WriteAllText(filePath, file);
        ShowConfigure();
    }

    public void InitReminder()
    {
        string filePath = Application.dataPath + "/Config/config.json";
        Config config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(filePath));
        int i = 0;
        foreach (ReminderConfig reminderConfig in config.reminders)
        {
            reminders[i].SetConfig(reminderConfig);
            reminders[i].StartReminder();
            i++;
        }
    }

    public void QuitGame()
    {
        //Debug.Log("quit");
        Application.Quit();
    }
}
