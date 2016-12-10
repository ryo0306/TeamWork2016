using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections;
using UnityEngine.UI;

public class EventManager : SingletonMonoBehaviour<EventManager> {

    enum EventName
    {
        StartEvent,
        PositionEvent
    }

    enum Command
    {
        EventStart,
        Message,
        EventEnd,
    }

    struct EventData
    {
        public EventData(EventName name_, List<string> commands_, List<string> messages_, float activePosition_)
        {
            name = name_;
            commands = commands_;
            messages = messages_;
            messageProgres = 0;
            activePosition = activePosition_;
        }

        public EventName name;
        public List<string> commands;
        public List<string> messages;
        public float activePosition;
        public int messageProgres;
    }

    [SerializeField]
    public bool isEvent{ get; set;}

    [SerializeField, Tooltip("EventMessage")]
    GameObject messageOrigin = null;


    [SerializeField, Tooltip("Player")]
    GameObject Player = null;

    List<EventData> events = new List<EventData>();

    void Start()
    {
        isEvent = false;
        LoadEvent("test4.csv");
        //StartCoroutine(Check());
        // こう書ける
    }

    void FixedUpdate()
    {
        Check();
    }


    void LoadEvent(string filePath_)
    {
        List<string[]> data = CSVReader.Instance.Read(filePath_);


        //ここの部分は修正の必要あり
        //読み込み後のデータの仕分け
        for (int i = 0; i < data.Count; i++)
        {
            List<string> commands = new List<string>();
            if (data[i][0] == "PositionEvent")
            {
                List<string> messages = new List<string>();

                commands.Add(data[i][0]);
                float activePos = float.Parse(data[i][1]);
                i++;
                while (data[i][0] != "EventEnd")
                {
                    if (data[i][0] == "Message")
                    {
                        commands.Add(data[i][0]);
                        messages.Add(data[i][2]);
                    }
                    i++;
                }
                commands.Add(data[i][0]);
                events.Add(new EventData(EventName.PositionEvent, commands, messages, activePos));
            }

            if (data[i][0] == "StartEvent")
            {
                List<string> messages = new List<string>();

                commands.Add(data[i][0]);
                i++;
                while (data[i][0] != "EventEnd")
                {
                    if (data[i][0] == "Message")
                    {
                        commands.Add(data[i][0]);
                        messages.Add(data[i][2]);
                    }
                    i++;
                }
                commands.Add(data[i][0]);
                events.Add(new EventData(EventName.StartEvent, commands, messages, 0));
            }
        }
            Debug.Log(events.Count);
        foreach (var t in events)
        {
            foreach (var c in t.commands)
            {
                Debug.Log(c);
            }
            foreach (var m in t.messages)
            {
                Debug.Log(m);
            }
        }
    }


    void Check()
    {

        if (isEvent) return;
        for (int i = 0; i < events.Count; i++)
        {
            if (events[i].activePosition < Player.transform.position.x)
            {
                isEvent = true;
                Debug.Log("イベントが起こったよ");
                StartCoroutine(Eventnumerator(i));
            }
        }
    }

    IEnumerator Eventnumerator(int num)
    {
        Debug.Log("イベント開始");
        int eventProgres = 0;
        int messageProgres = 0;
        while (isEvent)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (events[num].commands[eventProgres] == "StartEvent" || events[num].commands[eventProgres] == "PositionEvent")
                {
                    eventProgres++;
                }
                else
                if (events[num].commands[eventProgres] == "Message")
                {
                    Speak(events[num].messages[messageProgres]);
                    messageProgres++;
                    eventProgres++;
                }
                else
                if (events[num].commands[eventProgres] == "EventEnd")
                {
                    events.RemoveAt(num);
                    Debug.Log("イベントが終わったよ");
                    Destroy(GameObject.Find("Message(Clone)"));
                    isEvent = false;
                }
                Debug.Log("イベント中" + num + ":" + eventProgres);
            }
                yield return null;
        }
    }

    void Speak(string message_)
    {
        GameObject messageObj = GameObject.Find("Message(Clone)");
        if (messageObj == null)
        {
            GameObject child = messageOrigin.transform.FindChild("Text").gameObject;
            child.GetComponent<Text>().text = message_;
            Instantiate(messageOrigin, Player.transform.position, new Quaternion());
            return;
        }

        GameObject child_ = messageObj.transform.FindChild("Text").gameObject;
        child_.GetComponent<Text>().text = message_;
    }
}
