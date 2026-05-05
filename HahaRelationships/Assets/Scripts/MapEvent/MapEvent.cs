using System.Collections.Generic;
using System.Collections;
public enum EventType
{
    Work,
    Entertainment,
    Rest,
    Interact,
    Self_Improvement,
}
public enum EventProperty
{
    Good,
    Bad,
    Neutral,
}
public enum ChoiceType
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
public abstract class MapEvent :IIgnore
{
    public abstract int EventID { get; }
    public abstract string EventName { get; }
    public abstract string EventDescription { get; }
    public abstract EventType Type { get; }
    public abstract int EventWeight { get; }
    public abstract int SpawnLimit { get; }
    public abstract EventProperty Property { get; }
    public abstract List<EventChoice> Choices { get; }
    public abstract bool SpawnCondition();
}
public abstract class EventChoice:IIgnore
{
    public abstract ChoiceType Type { get; }
    public abstract string ChoiceDescription { get; }
    public abstract void ExecuteChoice();
}