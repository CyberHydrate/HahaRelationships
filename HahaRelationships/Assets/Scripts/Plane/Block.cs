using System;
using System.Diagnostics;

public enum E_BlockType
{
    Empty,
    Event,
    Important,
    Plan,
    Unknown
}
public enum E_EventType
{
    Work,
    Entertainment,
    Rest,
    Interact,
    Self_Improvement,
}
public class Block
{
    public E_BlockType blockType;
    public BlockEvent blockEvent;


    public Block(E_BlockType type)
    {
        this.blockType = type;
    }
    public Block(E_BlockType blockType,BlockEvent blockEvent)
    {
        this.blockType = blockType;
        this.blockEvent = blockEvent;
    }
}
public class BlockEvent
{
    public int eventId;
    public E_EventType eventType;
    public string eventName;
    public string eventDesc;
    public Choice[] choices;//写五个选项，不足五个就写null
    public int choiceCount;
    public Func<bool> generateCheck;
    public BlockEvent() { }
    public BlockEvent(E_EventType eventType)
    {
        this.eventType = eventType;
    }

}
public class Choice
{
    public string choiceName;
    public string choiceDesc;
    public Action choiceFunc;
    public Choice(string choiceName, string choiceDesc, Action choiceFunc) 
    {
        this.choiceName = choiceName;
        this.choiceDesc = choiceDesc;
        this.choiceFunc = choiceFunc;
    }
}
public class TestWorkEvent : BlockEvent
{
    public TestWorkEvent()
    {
        eventId = 1;
        eventType = E_EventType.Work;
        eventName = "测试_工作事件";
        eventDesc = "测试_工作事件描述";
        choiceCount = 1;
        choices = new Choice[] { new Choice("测试选项名", "你按下了测试选项", TestFunc) };

    }
    public void TestFunc() 
    {
        UnityEngine.Debug.Log("工作事件发生了");
    }
    
}
public class TestRestEvent : BlockEvent
{
public class TestEntertainmentEvent : BlockEvent
{
    public TestEntertainmentEvent()
    {
        eventId = 1;
        eventType = E_EventType.Work;
        eventName = "测试_娱乐事件";
        eventDesc = "测试_娱乐事件描述";
        choiceCount = 1;
        choices = new Choice[] { new Choice("测试选项名", "你按下了测试选项", TestFunc) };

    }
    public void TestFunc() 
        {
            UnityEngine.Debug.Log("娱乐事件发生了");
        }
}
    public TestRestEvent()
    {
        eventId = 1;
        eventType = E_EventType.Work;
        eventName = "测试_休息事件";
        eventDesc = "测试_休息事件描述";
        choiceCount = 1;
        choices = new Choice[] { new Choice("测试选项名", "测试选项描述", TestFunc) };

    }
    public void TestFunc() 
    {
        UnityEngine.Debug.Log("休息事件发生了");
    }
}
public class TestEntertainmentEvent : BlockEvent
{
    public TestEntertainmentEvent()
    {
        eventId = 1;
        eventType = E_EventType.Work;
        eventName = "测试_娱乐事件";
        eventDesc = "测试_娱乐事件描述";
        choiceCount = 1;
        choices = new Choice[] { new Choice("测试选项名", "测试选项描述", TestFunc) };

    }
    public void TestFunc() 
    {
        UnityEngine.Debug.Log("娱乐事件发生了");
    }
}
public class TestSelfEvent : BlockEvent
{
    public TestSelfEvent()
    {
        eventId = 1;
        eventType = E_EventType.Work;
        eventName = "测试_自我提升事件";
        eventDesc = "测试_自我提升事件描述";
        choiceCount = 1;
        choices = new Choice[] { new Choice("测试选项名", "测试选项描述", TestFunc) };

    }
    public void TestFunc() 
    {
        UnityEngine.Debug.Log("自我提升事件发生了");
    }
}
public class TestInteractEvent : BlockEvent
{
    public TestInteractEvent()
    {
        eventId = 1;
        eventType = E_EventType.Work;
        eventName = "测试_互动事件";
        eventDesc = "测试_互动事件描述";
        choiceCount = 1;
        choices = new Choice[] { new Choice("测试选项名", "测试选项描述", TestFunc) };

    }
    public void TestFunc()
    {
        UnityEngine.Debug.Log("互动事件发生了");
    }
}