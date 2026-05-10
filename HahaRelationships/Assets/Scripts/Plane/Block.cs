using System;
using TMPro;

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
}