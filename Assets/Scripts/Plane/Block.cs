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
        new WorkEvent1(),
        new WorkEvent2(),
        new WorkEvent3(),
        new WorkEvent4(),
        new WorkEvent5(),
        new WorkEvent6(),
        new WorkEvent7(),
        new WorkEvent8(),
        new WorkEvent9(),
        new WorkEvent10(),
        new WorkEvent11(),
        new WorkEvent12(),
        new WorkEvent13(),
        new WorkEvent14(),
        new EntertainEvent1(),
        new EntertainEvent2(),
        new EntertainEvent3(),
        new EntertainEvent4(),
        new EntertainEvent5(),
        new EntertainEvent6(),
        new EntertainEvent7(),
        new EntertainEvent8(),
        new EntertainEvent9(),
        new EntertainEvent10(),
        new EntertainEvent11(),
        new EntertainEvent12(),
        new EntertainEvent13(),
        new RestEvent(),
        new InteractEvent1(),
        new InteractEvent2(),
        new InteractEvent3(),
        new InteractEvent4(),
        new InteractEvent5(),
        new InteractEvent6(),
        new InteractEvent7(),
        new InteractEvent8(),
        new InteractEvent9(),
        new InteractEvent10(),
        new SelfEvent1(),
        new SelfEvent2(),
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
    public void ChangePlayerhp(int i)
    {
        PlayerDataManager.Instance.playerData.playerhp += i;
    }
    public void ChangeRelationshiphp(int i)
    {
        PlayerDataManager.Instance.playerData.relationshiphp += i;
    }
}
public class WorkEvent1 : BlockEvent
{
    public WorkEvent1()
    {
        eventId = 1;
        choices = new Action[3] { () =>ChangePlayerhp(5), () => ChangePlayerhp(-1), () => ChangePlayerhp(-5) };
    }
}

public class WorkEvent2 : BlockEvent
{
    public WorkEvent2()
    {
        eventId = 2;
        choices = new Action[3] { () => ChangePlayerhp(5), () => ChangePlayerhp(-1), () => ChangePlayerhp(0) };

    }
 
}

public class WorkEvent3 : BlockEvent
{
    public WorkEvent3()
    {
        eventId = 3;
        choices = new Action[1] {Func1};

    }
    public void Func1()
    {
      ChangePlayerhp(3);
        //后一格变为空白格（日程安排格不变）
    }

}

public class WorkEvent4 : BlockEvent
{
    public WorkEvent4()
    {
        eventId = 4;
        choices = new Action[5] { () => ChangePlayerhp(0), () => ChangeRelationshiphp(-5), () => ChangePlayerhp(0), ChangeRelationshiphp(-3), () => ChangePlayerhp(2) };

    }
}

public class WorkEvent5 : BlockEvent
{
    public WorkEvent5()
    {
        eventId = 5;
        choices = new Action[4] { ()=>ChangeRelationshiphp(-1), ()=>ChangePlayerhp(-2), () => ChangeRelationshiphp(-3), ()=>ChangePlayerhp(5)};

    }

}

public class WorkEvent6 : BlockEvent
{
    public WorkEvent6()
    {
        eventId = 6;
        choices = new Action[1] { () => ChangePlayerhp(2) };

    }
}

public class WorkEvent7 : BlockEvent
{
    public WorkEvent7()
    {
        eventId = 7;
        choices = new Action[4] { () => ChangePlayerhp(2), () => ChangePlayerhp(-2), () => ChangePlayerhp(-1), () => ChangePlayerhp(4) };

    }
}

public class WorkEvent8 : BlockEvent
{
    public WorkEvent8()
    {
        eventId = 8;
        choices = new Action[4] { ()=>ChangeRelationshiphp(-5), () => ChangePlayerhp(10), () => ChangeRelationshiphp(-5), () => ChangeRelationshiphp(-15) };

    }
}

public class WorkEvent9 : BlockEvent
{
    public WorkEvent9()
    {
        eventId = 9;
        choices = new Action[1] { () => ChangePlayerhp(-2) };
    }
}

public class WorkEvent10 : BlockEvent
{
    public WorkEvent10()
    {
        eventId = 10;
        choices = new Action[4] { () => ChangePlayerhp(-10), () => ChangePlayerhp(-5), () => ChangePlayerhp(3), () => ChangePlayerhp(0) };

    }

}

public class WorkEvent11 : BlockEvent
{
    public WorkEvent11()
    {
        eventId = 11;
        choices = new Action[5] { () => ChangePlayerhp(-5), () => ChangePlayerhp(-5), () => ChangePlayerhp(5), () => ChangePlayerhp(5), () => ChangePlayerhp(0) };

    }

}

public class WorkEvent12 : BlockEvent
{
    public WorkEvent12()
    {
        eventId = 12;
        choices = new Action[4] { () => ChangePlayerhp(20), () => ChangePlayerhp(10), () => ChangePlayerhp(-10), () => ChangePlayerhp(-5) };

    }

}

public class WorkEvent13 : BlockEvent
{
    public WorkEvent13()
    {
        eventId = 13;
        choices = new Action[5] { Func1 () => ChangePlayerhp(5), () => ChangePlayerhp(0), () => ChangePlayerhp(-20), () => ChangePlayerhp(10), () => ChangePlayerhp(-5) };

    }

}

public class WorkEvent14 : BlockEvent
{
    public WorkEvent14()
    {
        eventId = 14;
        choices = new Action[4] { () => ChangePlayerhp(1), () => ChangePlayerhp(3), () => ChangePlayerhp(2), () => ChangePlayerhp(-20) };

    }
}

public class EntertainEvent1 : BlockEvent
{
    public EntertainEvent1()
    {
        eventId = 15;
        choices = new Action[4] { () => ChangePlayerhp(-5), () => ChangePlayerhp(-3), () => ChangePlayerhp(-1), () => ChangePlayerhp(0) };

    }
}

public class EntertainEvent2 : BlockEvent
{
    public EntertainEvent2()
    {
        eventId = 16;
        choices = new Action[4] { () => ChangePlayerhp(-5), () => ChangePlayerhp(-), () => ChangePlayerhp(1), () => ChangePlayerhp(0) };

    }
}

public class EntertainEvent3 : BlockEvent
{
    public EntertainEvent3()
    {
        eventId = 17;
        choices = new Action[4] { () => ChangePlayerhp(-5), () => ChangePlayerhp(-3), () => ChangePlayerhp(3), () => ChangePlayerhp(0) };

    }
}

public class EntertainEvent4 : BlockEvent
{
    public EntertainEvent4()
    {
        eventId = 18;
        choices = new Action[4] { () => ChangePlayerhp(2), () => ChangePlayerhp(-1), () => ChangePlayerhp(-2), () => ChangePlayerhp(1) };

    }
}

public class EntertainEvent5 : BlockEvent
{
    public EntertainEvent5()
    {
        eventId = 19;
        choices = new Action[4] { () => ChangePlayerhp(2), () => ChangePlayerhp(), () => ChangePlayerhp(0), () => ChangePlayerhp(0) };

    }
}
public class EntertainEvent6 : BlockEvent
{
    public EntertainEvent6()
    {
        eventId = 20;
        choices = new Action[4] { () => ChangePlayerhp(1), () => ChangeRelationshiphp(-1), () => ChangePlayerhp(-1), () => ChangePlayerhp(1) };

    }
}

public class EntertainEvent7 : BlockEvent
{
    public EntertainEvent7()
    {
        eventId = 21;
        choices = new Action[5] { () => ChangePlayerhp(-1), () => ChangePlayerhp(1), () => ChangePlayerhp(-2), () => ChangePlayerhp(1),()=>ChangePlayerhp(1) };

    }
}

public class EntertainEvent8 : BlockEvent
{
    public EntertainEvent8()
    {
        eventId = 22;
        choices = new Action[1] { () => ChangePlayerhp(5) };

    }
}

public class EntertainEvent9 : BlockEvent
{
    public EntertainEvent9()
    {
        eventId = 23;
        choices = new Action[1] { () => ChangePlayerhp(2) };

    }
}

public class EntertainEvent10 : BlockEvent
{
    public EntertainEvent10()
    {
        eventId = 24;
        choices = new Action[1] { () => ChangePlayerhp(1) };

    }
}

public class EntertainEvent11 : BlockEvent
{
    public EntertainEvent11()
    {
        eventId = 25;
        choices = new Action[1] { () => ChangePlayerhp(3) };

    }
}

public class EntertainEvent12 : BlockEvent
{
    public EntertainEvent12()
    {
        eventId = 26;
        choices = new Action[1] { () => ChangePlayerhp(5) };

    }
}

public class EntertainEvent13 : BlockEvent
{
    public EntertainEvent13()
    {
        eventId = 27;
        choices = new Action[1] { () => { ChangePlayerhp(-1); ChangeRelationshiphp(1); } };

    }
}

public class RestEvent : BlockEvent
{
    public RestEvent()
    {
        eventId = 28;
        choices = new Action[1] { () => ChangePlayerhp(5) };

    }
}

public class InteractEvent1 : BlockEvent
{
    public InteractEvent1()
    {
        eventId = 29;
        choices = new Action[3] { () => { ChangeRelationshiphp(1); ChangePlayerhp(-1); },()=>ChangeRelationshiphp(1),()=>ChangeRelationshiphp(-3) };

    }
}

public class InteractEvent2 : BlockEvent
{
    public InteractEvent2()
    {
        eventId = 30;
        choices = new Action[4]{ () =>ChangeRelationshiphp(-5),()=>ChangeRelationshiphp(-10),()=>ChangePlayerhp(-1),()=>ChangePlayerhp(-2) };

    }
}

public class InteractEvent3 : BlockEvent
{
    public InteractEvent3()
    {
        eventId = 31;
        choices = new Action[1] { () =>ChangePlayerhp(2) };

    }
}

public class InteractEvent4 : BlockEvent
{
    public InteractEvent4()
    {
        eventId = 32;
        choices = new Action[4] { () =>ChangeRelationshiphp(10),()=>ChangeRelationshiphp(5),()=>ChangeRelationshiphp(3),()=>ChangeRelationshiphp(-10) };

    }
}

public class InteractEvent5 : BlockEvent
{
    public InteractEvent5()
    {
        eventId = 33;
        choices = new Action[1] { () =>ChangePlayerhp(-5) };

    }
}

public class InteractEvent6 : BlockEvent
{
    public InteractEvent6()
    {
        eventId = 34;
        choices = new Action[1] { () => ChangePlayerhp(5) };

    }
}

public class InteractEvent7 : BlockEvent
{
    public InteractEvent7()
    {
        eventId = 35;
        choices = new Action[1] { () =>ChangeRelationshiphp(5) };

    }
}

public class InteractEvent8 : BlockEvent
{
    public InteractEvent8()
    {
        eventId = 36;
        choices = new Action[1] { () =>ChangeRelationshiphp(10) };

    }
}

public class InteractEvent9 : BlockEvent
{
    public InteractEvent9()
    {
        eventId = 37;
        choices = new Action[1] { () =>ChangeRelationshiphp(5)};

    }
}

public class InteractEvent10 : BlockEvent
{
    public InteractEvent10()
    {
        eventId = 38;
        choices = new Action[1] { () =>ChangePlayerhp(2) };

    }
}

public class SelfEvent1 : BlockEvent
{
    public SelfEvent1()
    {
        eventId = 39;
        choices = new Action[] { };

    }
}

public class SelfEvent2 : BlockEvent
{
    public SelfEvent2()
    {
        eventId = 40;
        choices = new Action[] { };

    }
}
