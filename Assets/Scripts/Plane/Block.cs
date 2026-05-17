using System;
using System.Diagnostics;

public enum E_BlockType
{
    空,
    事件,
    重要事件,
    计划,
    未知
}
public enum E_EventType
{
    工作,
    娱乐,
    休息,
    和ta互动,
    自我提升,
}
public enum E_ChoiceType
{
    积极,
    正常,
    极端,
    混沌,
    保守,
}
public static class Events
{
    public static BlockEvent[] events = new BlockEvent[] {
        new TestWorkEvent(),
        new TestEntertainmentEvent(),
        new TestRestEvent(),
        new TestInteractEvent(),
        new TestSelfEvent(),
        };
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
    public Action[] choices;
    public Func<bool> generateCheck;
}
public class TestWorkEvent : BlockEvent
{
    public TestWorkEvent()
    {
        eventId = 1;
        choices = new Action[5] { TestFunc, TestFunc, TestFunc, TestFunc, TestFunc };

    }
    public void TestFunc()
    {
        PlayerDataManager.Instance.playerData.playerhp -= 1;

    }
}
public class TestEntertainmentEvent : BlockEvent
{
    public TestEntertainmentEvent()
    {
        eventId = 2;
        choices = new Action[5] { TestFunc, TestFunc, TestFunc, TestFunc, TestFunc };

    }
    public void TestFunc()
    {
        PlayerDataManager.Instance.playerData.playerhp -= 1;

    }
}
public class TestRestEvent : BlockEvent
{
    public TestRestEvent()
    {
        eventId = 3;
        choices = new Action[5] { TestFunc, TestFunc, TestFunc, TestFunc, TestFunc };

    }
    public void TestFunc()
    {
        PlayerDataManager.Instance.playerData.playerhp -= 1;

    }
}
public class TestSelfEvent : BlockEvent
{
    public TestSelfEvent()
    {
        eventId = 5;
        choices = new Action[5] { TestFunc, TestFunc, TestFunc, TestFunc, TestFunc };

    }
    public void TestFunc()
    {
        PlayerDataManager.Instance.playerData.playerhp -= 1;

    }
}
public class TestInteractEvent : BlockEvent
{
    public TestInteractEvent()
    {
        eventId = 4;
        choices = new Action[5] { TestFunc, TestFunc, TestFunc, TestFunc, TestFunc };

    }
    public void TestFunc()
    {
        PlayerDataManager.Instance.playerData.playerhp -= 1;

    }
}