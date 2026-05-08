using System;
using System.Collections.Generic;
public enum E_MapThingType
{
    Unknown,
    Empty,
    Event,
    ImportantEvent,
    Plan,
}
public abstract class MapThing
{   ///<summary>
    ///获取格子的类型
    ///</summary>
    public abstract E_MapThingType Type { get;}
}
public enum E_EventType
{
    Work,
    Entertainment,
    Rest,
    Interact,
    Self_Improvement,
}
public enum E_EventProperty
{
    Good,
    Bad,
    Neutral,
}
public enum E_ChoiceType
{
    Positive,
    Negative,
    Neutral,
    unknown
}
public interface IIgnore
{
    //让unity的序列化系统忽略这个类，不显示在Inspector中
}
public abstract class MapEvent :MapThing,IIgnore
{
    public abstract int EventID { get; }
    public abstract string EventName { get; }
    public abstract string EventDescription { get; }
    public abstract E_EventType EventType { get; }
    public abstract int EventWeight { get; }
    public abstract int SpawnLimit { get; }
    public abstract int offset { get; set; }
    public abstract E_EventProperty Property { get; }
    public abstract List<EventChoice> Choices { get; }
    public abstract bool SpawnCondition();
}
public abstract class EventChoice:IIgnore
{
    public abstract E_ChoiceType Type { get; }
    public abstract string ChoiceName { get; }
    public abstract string ChoiceDescription { get; }
    public abstract void ExecuteChoice();
}
public class Unknown:MapThing
{
    public override E_MapThingType Type => E_MapThingType.Unknown;
}
public class Plan:MapThing
{
    public override E_MapThingType Type => E_MapThingType.Plan;
}
public class Empty:MapThing
{
    public override E_MapThingType Type => E_MapThingType.Empty;
}
public class WorkEvent : MapEvent
{
    public override E_MapThingType Type => E_MapThingType.Event;
    public override int EventID => 0;
    public override string EventName => "工作";
    public override string EventDescription => "上班时间到了";
    public override E_EventType EventType => E_EventType.Work;
    public override int EventWeight => 1;
    public override int SpawnLimit => 1;
    public override int offset { get; set; } = 1;
    public override E_EventProperty Property => E_EventProperty.Neutral;
    public override List<EventChoice> Choices => new List<EventChoice>
    {
        new WorkChoice(),
        null,
        null,
        null,
        null
    };
    public override bool SpawnCondition()
    {
        return true; // 可以根据需要添加生成条件
    }
}
public class WorkChoice : EventChoice
{
    public override E_ChoiceType Type => E_ChoiceType.Positive;
    public override string ChoiceName => "选项名";
    public override string ChoiceDescription => "选项描述";
    public override void ExecuteChoice()
    {

    }
}
public class EntertainmentEvent : MapEvent
{
    public override E_MapThingType Type => E_MapThingType.Event;
    public override int EventID => 1;
    public override string EventName => "娱乐";
    public override string EventDescription => "娱乐时间到了";
    public override E_EventType EventType => E_EventType.Entertainment;
    public override int EventWeight => 1;
    public override int SpawnLimit => 1;
    public override int offset { get; set; } = 1;
    public override E_EventProperty Property => E_EventProperty.Neutral;
    public override List<EventChoice> Choices => new List<EventChoice>
    {
        new WorkChoice(),
        null,
        null,
        null,
        null
    };


    public override bool SpawnCondition()
    {
        return true; // 可以根据需要添加生成条件
    }
}
public class RestEvent : MapEvent
{
    public override E_MapThingType Type => E_MapThingType.Event;
    public override int EventID => 2;
    public override string EventName => "休息";
    public override string EventDescription => "睡觉时间到了";
    public override E_EventType EventType => E_EventType.Rest;
    public override int EventWeight => 1;
    public override int SpawnLimit => 1;
    public override int offset { get; set; } = 1;
    public override E_EventProperty Property => E_EventProperty.Neutral;
    public override List<EventChoice> Choices => new List<EventChoice>
    {
        new WorkChoice(),
        null,
        null,
        null,
        null
    };

    public override bool SpawnCondition()
    {
        return true; // 可以根据需要添加生成条件
    }
}
public class InteractEvent : MapEvent
{
    public override E_MapThingType Type => E_MapThingType.Event;
    public override int EventID => 3;
    public override string EventName => "社交";
    public override string EventDescription => "社交时间到了";
    public override E_EventType EventType => E_EventType.Interact;
    public override int EventWeight => 1;
    public override int SpawnLimit => 1;
    public override int offset { get; set; } = 1;
    public override E_EventProperty Property => E_EventProperty.Neutral;
    public override List<EventChoice> Choices => new List<EventChoice>
    {
        new WorkChoice(),
        null,
        null,
        null,
        null
    };


    public override bool SpawnCondition()
    {
        return true; // 可以根据需要添加生成条件
    }
}
public class SelfImprovementEvent : MapEvent
{
    public override E_MapThingType Type => E_MapThingType.Event;
    public override int EventID => 4;
    public override string EventName => "自我提升";
    public override string EventDescription => "学习时间到了";
    public override E_EventType EventType => E_EventType.Self_Improvement;
    public override int EventWeight => 1;
    public override int SpawnLimit => 1;
    public override int offset { get; set; } = 1;
    public override E_EventProperty Property => E_EventProperty.Neutral;
    public override List<EventChoice> Choices => new List<EventChoice>
    {
        new WorkChoice(),
        null,
        null,
        null,
        null
    };

    public override bool SpawnCondition()
    {
        return true; // 可以根据需要添加生成条件
    }
}