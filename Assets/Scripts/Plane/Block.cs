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
        eventName = "逆天同事";
        eventDesc = "同事把咖啡洒在电脑上，你这几天做的工作全部白费，老板当时把你训斥。";
        choiceCount = 2;
        choices = new Choice[] { 
            new Choice("战战战我杀杀杀", "同事吓哭了，但也不敢再招惹你，答应帮你把工作重新做完。", TestFunc1),
            new Choice("和同事谈判","同事最终赔偿了你修电脑的钱，但是失去的工作你只能重新再做一遍。",TestFunc2)                                                                 
        };

    }
    public void TestFunc1() 
    {
        PlayerDataManager.Instance.playerData.playerhp += 5;
    }
    public void TestFunc2()
    {
        PlayerDataManager.Instance.playerData.playerhp -= 1;
    }
    
}
public class TestEntertainmentEvent : BlockEvent
{
    public TestEntertainmentEvent()
    {
        eventId = 2;
        eventType = E_EventType.Entertainment;
        eventName = "恋爱脑朋友倾诉1";
        eventDesc = "“他说他工作很忙不能陪我，结果半夜三点还在双排打游戏。” 朋友再一次向你倾诉自己的恋爱苦恼，你...";
        choiceCount = 2;
        choices = new Choice[] { 
            new Choice("分手吧，他不爱你！", "朋友不解地看着你：“可是我爱ta呀！”", TestFunc1),
            new Choice("你可以和他好好沟通", "岂料朋友的恋爱对象是一个无法沟通者，两人鏖战许久，朋友无功而返。”", TestFunc2)
        };

    }
    public void TestFunc1() 
        {
            PlayerDataManager.Instance.playerData.playerhp -= 5;
        }
        public void TestFunc2()
        {
            PlayerDataManager.Instance.playerData.playerhp -= 3;
        }
}
public class TestRestEvent : BlockEvent
{
    public TestRestEvent()
    {
        eventId = 3;
        eventType = E_EventType.Rest;
        eventName = "休息";
        eventDesc = "";
        choiceCount = 1;
        choices = new Choice[] { new Choice("好好休息", "你好好休息了一天", TestFunc) };

    }
    public void TestFunc() 
    {
        PlayerDataManager.Instance.playerData.playerhp += 5;
    }
}
public class TestSelfEvent : BlockEvent
{
    public TestSelfEvent()
    {
        eventId = 5;
        eventType = E_EventType.Self_Improvement;
        eventName = "阅读";
        eventDesc = " ";
        choiceCount = 1;
        choices = new Choice[] { new Choice("看会书吧", "真好看", TestFunc) };

    }
    public void TestFunc() 
    {
        PlayerDataManager.Instance.playerData.playerhp += 5;
    }
}
public class TestInteractEvent : BlockEvent
{
    public TestInteractEvent()
    {
        eventId = 4;
        eventType = E_EventType.Interact;
        eventName = "逛商场";
        eventDesc = "你打算给对方买一件合身的衣服";
        choiceCount = 2;
        choices = new Choice[] { new Choice("狠狠消费", " ", TestFunc1),
        new Choice("价格实惠就好","",TestFunc2)};

    }
    public void TestFunc1()
    {
        PlayerDataManager.Instance.playerData.relationship += 1;
        PlayerDataManager.Instance.playerData.playerhp -= 1;

    }
    public void TestFunc2()
    {
        PlayerDataManager.Instance.playerData.relationship += 1;
    }
}
public class Event6 : BlockEvent
{
    public Event6()
    {
        eventId = 6;
        eventType = E_EventType.Entertainment;
        eventName = "买谷";
        eventDesc = "心情悠闲地逛谷子店，要买哪个呢？";
        choiceCount = 1;
        choices = new Choice[] { new Choice("爽抽盲盒", "抽到了你最喜欢的角色！", TestFunc1),
        };

    }
    public void TestFunc1()
    {
        PlayerDataManager.Instance.playerData.relationship += 1;
        PlayerDataManager.Instance.playerData.playerhp -= 1;

    }
}
public class Event7 : BlockEvent
{
    public Event7()
    {
        eventId = 7;
        eventType = E_EventType.Work;
        eventName = "迟到";
        eventDesc = "你和同事通宵打游戏导致起晚，匆匆忙忙连滚带爬地赶到公司已经迟到。突然你定睛一看，同事早已坐在工位上，哦，她比你先到！？";
        choiceCount = 1;
        choices = new Choice[] { new Choice("看向你的同事", "我去咋这样", TestFunc1),
        };

    }
    public void TestFunc1()
    {
        PlayerDataManager.Instance.playerData.relationship += 1;
        PlayerDataManager.Instance.playerData.playerhp -= 1;

    }
}
public class Event8 : BlockEvent
{
    public Event8()
    {
        eventId = 8;
        eventType = E_EventType.Entertainment;
        eventName = "看电视剧";
        eventDesc = "男主说：你就是瞎了眼看上我这条狗，我就是瞎了狗眼没看上你，太痛苦了！";
        choiceCount = 1;
        choices = new Choice[] { new Choice("关掉", " ", TestFunc1),
        };

    }
    public void TestFunc1()
    {
        PlayerDataManager.Instance.playerData.relationship += 1;
        PlayerDataManager.Instance.playerData.playerhp += 1;

    }
}
public class Event9 : BlockEvent
{
    public Event9()
    {
        eventId = 9;
        eventType = E_EventType.Work;
        eventName = "拖欠工资";
        eventDesc = "你的老板已经两个月没发过工资了，同事们议论纷纷，不少人去问老板原因，却始终得不到明确的答复。你决定...";
        choiceCount = 1;
        choices = new Choice[] { new Choice("直接起诉", "面对你拟好的诉状和带来的律师，老板流下了动感的眼泪，哭诉公司经营不善才导致最近没钱发工资。为了平息你的怒火，老板偷偷给你转了一笔钱，但你的同事们什么也没得到。", TestFunc1),
            new Choice("（扑通跪下）求老板发发善心", "虽然老板鸟都不鸟你，同事们却很感动，认为你勇气可嘉，请你吃了一个月午饭。", TestFunc2),
        };

    }
    public void TestFunc1()
    {
        PlayerDataManager.Instance.playerData.relationship += 0;
        PlayerDataManager.Instance.playerData.playerhp += 1;
    }
    public void TestFunc2()
    {
        PlayerDataManager.Instance.playerData.relationship += 0;
        PlayerDataManager.Instance.playerData.playerhp += 3;
    }
}
public class Event10 : BlockEvent
{
    public Event10()
    {
        eventId = 10;
        eventType = E_EventType.Work;
        eventName = "公司团建";
        eventDesc = "今天是团建日，老板问大家有没有什么想玩的小游戏，你想了想，说...";
        choiceCount = 1;
        choices = new Choice[] { new Choice("你的感情极其幽默", "老板以为你在骂人，扣了你半个月工资。", TestFunc1),
            new Choice("不要做挑战", "老板欣然同意，你们爽玩一下午，从此所有人都爱上了玩不要做挑战，你们公司成为了靠录不要做挑战视频起家的MCN公司。", TestFunc2),
        };

    }
    public void TestFunc1()
    {
        PlayerDataManager.Instance.playerData.relationship += 0;
        PlayerDataManager.Instance.playerData.playerhp += 20;

    }
    public void TestFunc2()
    {
        PlayerDataManager.Instance.playerData.relationship += 0;
        PlayerDataManager.Instance.playerData.playerhp += 10;

    }
}