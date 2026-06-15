using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public static class WorkEventPool
{
    private static readonly List<BlockEvent>_workEvents = new List<BlockEvent>();
    
    static WorkEventPool()
    {
        var initEvents=Events.events.Where(e=>
        e is WorkEvent1
        or WorkEvent2
        or WorkEvent3
        or WorkEvent4
        or WorkEvent5
        or WorkEvent6
        or WorkEvent7
        or WorkEvent8
        or WorkEvent9
        or WorkEvent10
        or WorkEvent11
        or WorkEvent12
        or WorkEvent13
        or WorkEvent14)
            .Where(e => e.eventId != 2)
            .ToList();
        _workEvents.AddRange(initEvents);
    }
    public static void AddEvent(BlockEvent workEvent)
    {
        if (workEvent == null) throw new ArgumentNullException(nameof(workEvent));
        if (_workEvents.Any(e => e.eventId == workEvent.eventId))
            throw new InvalidOperationException($"事件ID {workEvent.eventId} 已存在");

        _workEvents.Add(workEvent);
    }

    public static bool RemoveEvent(int eventId)
    {
        var targetEvent = _workEvents.FirstOrDefault(e => e.eventId==eventId);
        if (targetEvent == null) return false;

        _workEvents.Remove(targetEvent);
        return true;
    }

    public static BlockEvent QueryEvent(int eventId)
    {
        return _workEvents.FirstOrDefault(e => e.eventId == eventId);
    }

    public static List<BlockEvent> QueryAllEvents()
    {
        return new List<BlockEvent>(_workEvents);
    }

    public static bool UpdateEvent(int eventId, BlockEvent newEvent)
    {
        if (newEvent == null) throw new ArgumentNullException(nameof(newEvent));
        var targetIndex = _workEvents.FindIndex(e => e.eventId == eventId);
        if (targetIndex == -1) return false;
        _workEvents[targetIndex] = newEvent;
        return true;
    }

    public static Block GetWorkEvent()
    {
        if (_workEvents.Count == 0)
        {
            return null;
        }
        var random = new Random();
        int randomIndex = random.Next(0, _workEvents.Count);
        BlockEvent randomEvent = _workEvents[randomIndex];
        return new Block(E_BlockType.工作, randomEvent);
    }
}

public static class EntertainEventPool
{
    private static readonly List<BlockEvent> _entertainEvents = new List<BlockEvent>();

    static EntertainEventPool()
    {
        var initEvents = Events.events.Where(e =>
        e is EntertainEvent1
        or EntertainEvent2
        or EntertainEvent3
        or EntertainEvent4
        or EntertainEvent5
        or EntertainEvent6
        or EntertainEvent7
        or EntertainEvent8
        or EntertainEvent9
        or EntertainEvent10
        or EntertainEvent11
        or EntertainEvent12
        or EntertainEvent13)
            .ToList();
        _entertainEvents.AddRange(initEvents);
    }
    public static void AddEvent(BlockEvent entertainEvent)
    {
        if (entertainEvent == null) throw new ArgumentNullException(nameof(entertainEvent));
        if (_entertainEvents.Any(e => e.eventId == entertainEvent.eventId))
            throw new InvalidOperationException($"事件ID {entertainEvent.eventId} 已存在");

        _entertainEvents.Add(entertainEvent);
    }

    public static bool RemoveEvent(int eventId)
    {
        var targetEvent = _entertainEvents.FirstOrDefault(e => e.eventId == eventId);
        if (targetEvent == null) return false;

        _entertainEvents.Remove(targetEvent);
        return true;
    }

    public static BlockEvent QueryEvent(int eventId)
    {
        return _entertainEvents.FirstOrDefault(e => e.eventId == eventId);
    }

    public static List<BlockEvent> QueryAllEvents()
    {
        return new List<BlockEvent>(_entertainEvents);
    }

    public static bool UpdateEvent(int eventId, BlockEvent newEvent)
    {
        if (newEvent == null) throw new ArgumentNullException(nameof(newEvent));
        var targetIndex = _entertainEvents.FindIndex(e => e.eventId == eventId);
        if (targetIndex == -1) return false;
        _entertainEvents[targetIndex] = newEvent;
        return true;
    }

    public static Block GetEntertainEvent()
    {
        if (_entertainEvents.Count == 0)
        {
            return null;
        }
        var random = new Random();
        int randomIndex = random.Next(0, _entertainEvents.Count);
        BlockEvent randomEvent = _entertainEvents[randomIndex];
        return new Block(E_BlockType.娱乐, randomEvent);
    }
}

public static class RestEventPool
{
    private static readonly List<BlockEvent> _restEvents = new List<BlockEvent>();

    static RestEventPool()
    {
        var initEvents = Events.events.Where(e =>
        e is RestEvent)
            .ToList();
        _restEvents.AddRange(initEvents);
    }
    public static void AddEvent(BlockEvent restEvent)
    {
        if (restEvent == null) throw new ArgumentNullException(nameof(restEvent));
        if (_restEvents.Any(e => e.eventId == restEvent.eventId))
            throw new InvalidOperationException($"事件ID {restEvent.eventId} 已存在");

        _restEvents.Add(restEvent);
    }

    public static bool RemoveEvent(int eventId)
    {
        var targetEvent = _restEvents.FirstOrDefault(e => e.eventId == eventId);
        if (targetEvent == null) return false;

        _restEvents.Remove(targetEvent);
        return true;
    }

    public static BlockEvent QueryEvent(int eventId)
    {
        return _restEvents.FirstOrDefault(e => e.eventId == eventId);
    }

    public static List<BlockEvent> QueryAllEvents()
    {
        return new List<BlockEvent>(_restEvents);
    }

    public static bool UpdateEvent(int eventId, BlockEvent newEvent)
    {
        if (newEvent == null) throw new ArgumentNullException(nameof(newEvent));
        var targetIndex = _restEvents.FindIndex(e => e.eventId == eventId);
        if (targetIndex == -1) return false;
        _restEvents[targetIndex] = newEvent;
        return true;
    }

    public static Block GetRestEvent()
    {
        if (_restEvents.Count == 0)
        {
            return null;
        }
        var random = new Random();
        int randomIndex = random.Next(0, _restEvents.Count);
        BlockEvent randomEvent = _restEvents[randomIndex];
        return new Block(E_BlockType.休息, randomEvent);
    }
}

public static class InteractEventPool
{
    private static readonly List<BlockEvent> _interactEvents = new List<BlockEvent>();

    static InteractEventPool()
    {
        var initEvents = Events.events.Where(e =>
        e is InteractEvent1
        or InteractEvent2
        or InteractEvent3
        or InteractEvent4
        or InteractEvent5
        or InteractEvent6
        or InteractEvent7
        or InteractEvent8
        or InteractEvent9
        or InteractEvent10)
            .ToList();
        _interactEvents.AddRange(initEvents);
    }
    public static void AddEvent(BlockEvent interactEvent)
    {
        if (interactEvent == null) throw new ArgumentNullException(nameof(interactEvent));
        if (_interactEvents.Any(e => e.eventId == interactEvent.eventId))
            throw new InvalidOperationException($"事件ID {interactEvent.eventId} 已存在");

        _interactEvents.Add(interactEvent);
    }

    public static bool RemoveEvent(int eventId)
    {
        var targetEvent = _interactEvents.FirstOrDefault(e => e.eventId == eventId);
        if (targetEvent == null) return false;

        _interactEvents.Remove(targetEvent);
        return true;
    }

    public static BlockEvent QueryEvent(int eventId)
    {
        return _interactEvents.FirstOrDefault(e => e.eventId == eventId);
    }

    public static List<BlockEvent> QueryAllEvents()
    {
        return new List<BlockEvent>(_interactEvents);
    }

    public static bool UpdateEvent(int eventId, BlockEvent newEvent)
    {
        if (newEvent == null) throw new ArgumentNullException(nameof(newEvent));
        var targetIndex = _interactEvents.FindIndex(e => e.eventId == eventId);
        if (targetIndex == -1) return false;
        _interactEvents[targetIndex] = newEvent;
        return true;
    }

    public static Block GetInteractEvent()
    {
        if (_interactEvents.Count == 0)
        {
            return null;
        }
        var random = new Random();
        int randomIndex = random.Next(0, _interactEvents.Count);
        BlockEvent randomEvent = _interactEvents[randomIndex];
        return new Block(E_BlockType.和ta互动, randomEvent);
    }
}

public static class SelfEventPool
{
    private static readonly List<BlockEvent> _selfEvents = new List<BlockEvent>();

    static SelfEventPool()
    {
        var initEvents = Events.events.Where(e =>
        e is SelfEvent1
        or SelfEvent2)
            .ToList();
        _selfEvents.AddRange(initEvents);
    }
    public static void AddEvent(BlockEvent selfEvent)
    {
        if (selfEvent == null) throw new ArgumentNullException(nameof(selfEvent));
        if (_selfEvents.Any(e => e.eventId == selfEvent.eventId))
            throw new InvalidOperationException($"事件ID {selfEvent.eventId} 已存在");

        _selfEvents.Add(selfEvent);
    }

    public static bool RemoveEvent(int eventId)
    {
        var targetEvent = _selfEvents.FirstOrDefault(e => e.eventId == eventId);
        if (targetEvent == null) return false;

        _selfEvents.Remove(targetEvent);
        return true;
    }

    public static BlockEvent QueryEvent(int eventId)
    {
        return _selfEvents.FirstOrDefault(e => e.eventId == eventId);
    }

    public static List<BlockEvent> QueryAllEvents()
    {
        return new List<BlockEvent>(_selfEvents);
    }

    public static bool UpdateEvent(int eventId, BlockEvent newEvent)
    {
        if (newEvent == null) throw new ArgumentNullException(nameof(newEvent));
        var targetIndex = _selfEvents.FindIndex(e => e.eventId == eventId);
        if (targetIndex == -1) return false;
        _selfEvents[targetIndex] = newEvent;
        return true;
    }

    public static Block GetSelfEvent()
    {
        if (_selfEvents.Count == 0)
        {
            return null;
        }
        var random = new Random();
        int randomIndex = random.Next(0, _selfEvents.Count);
        BlockEvent randomEvent = _selfEvents[randomIndex];
        return new Block(E_BlockType.自我提升, randomEvent);
    }
}

public static class ImportantWorkEventPool
{
    private static readonly List<BlockEvent> _workEvents = new List<BlockEvent>();

    static ImportantWorkEventPool()
    {
        var initEvents = Events.events.Where(e =>
        e is WorkEvent1
        or WorkEvent2
        or WorkEvent3
        or WorkEvent4
        or WorkEvent5
        or WorkEvent6
        or WorkEvent7
        or WorkEvent8
        or WorkEvent9
        or WorkEvent10
        or WorkEvent11
        or WorkEvent12
        or WorkEvent13
        or WorkEvent14)
            .Where(e => e.eventId != 2)
            .ToList();
        _workEvents.AddRange(initEvents);
    }
    public static void AddEvent(BlockEvent workEvent)
    {
        if (workEvent == null) throw new ArgumentNullException(nameof(workEvent));
        if (_workEvents.Any(e => e.eventId == workEvent.eventId))
            throw new InvalidOperationException($"事件ID {workEvent.eventId} 已存在");

        _workEvents.Add(workEvent);
    }

    public static bool RemoveEvent(int eventId)
    {
        var targetEvent = _workEvents.FirstOrDefault(e => e.eventId == eventId);
        if (targetEvent == null) return false;

        _workEvents.Remove(targetEvent);
        return true;
    }

    public static BlockEvent QueryEvent(int eventId)
    {
        return _workEvents.FirstOrDefault(e => e.eventId == eventId);
    }

    public static List<BlockEvent> QueryAllEvents()
    {
        return new List<BlockEvent>(_workEvents);
    }

    public static bool UpdateEvent(int eventId, BlockEvent newEvent)
    {
        if (newEvent == null) throw new ArgumentNullException(nameof(newEvent));
        var targetIndex = _workEvents.FindIndex(e => e.eventId == eventId);
        if (targetIndex == -1) return false;
        _workEvents[targetIndex] = newEvent;
        return true;
    }

    public static Block GetImportantWorkEvent()
    {
        if (_workEvents.Count == 0)
        {
            return null;
        }
        var random = new Random();
        int randomIndex = random.Next(0, _workEvents.Count);
        BlockEvent randomEvent = _workEvents[randomIndex];
        return new Block(E_BlockType.重要工作, randomEvent);
    }
}

public static class ImportantEntertainEventPool
{
    private static readonly List<BlockEvent> _entertainEvents = new List<BlockEvent>();

    static ImportantEntertainEventPool()
    {
        var initEvents = Events.events.Where(e =>
        e is EntertainEvent1
        or EntertainEvent2
        or EntertainEvent3
        or EntertainEvent4
        or EntertainEvent5
        or EntertainEvent6
        or EntertainEvent7
        or EntertainEvent8
        or EntertainEvent9
        or EntertainEvent10
        or EntertainEvent11
        or EntertainEvent12
        or EntertainEvent13)
            .ToList();
        _entertainEvents.AddRange(initEvents);
    }
    public static void AddEvent(BlockEvent entertainEvent)
    {
        if (entertainEvent == null) throw new ArgumentNullException(nameof(entertainEvent));
        if (_entertainEvents.Any(e => e.eventId == entertainEvent.eventId))
            throw new InvalidOperationException($"事件ID {entertainEvent.eventId} 已存在");

        _entertainEvents.Add(entertainEvent);
    }

    public static bool RemoveEvent(int eventId)
    {
        var targetEvent = _entertainEvents.FirstOrDefault(e => e.eventId == eventId);
        if (targetEvent == null) return false;

        _entertainEvents.Remove(targetEvent);
        return true;
    }

    public static BlockEvent QueryEvent(int eventId)
    {
        return _entertainEvents.FirstOrDefault(e => e.eventId == eventId);
    }

    public static List<BlockEvent> QueryAllEvents()
    {
        return new List<BlockEvent>(_entertainEvents);
    }

    public static bool UpdateEvent(int eventId, BlockEvent newEvent)
    {
        if (newEvent == null) throw new ArgumentNullException(nameof(newEvent));
        var targetIndex = _entertainEvents.FindIndex(e => e.eventId == eventId);
        if (targetIndex == -1) return false;
        _entertainEvents[targetIndex] = newEvent;
        return true;
    }

    public static Block GetImportantEntertainEvent()
    {
        if (_entertainEvents.Count == 0)
        {
            return null;
        }
        var random = new Random();
        int randomIndex = random.Next(0, _entertainEvents.Count);
        BlockEvent randomEvent = _entertainEvents[randomIndex];
        return new Block(E_BlockType.重要娱乐, randomEvent);
    }
}

public static class ImportantInteractEventPool
{
    private static readonly List<BlockEvent> _interactEvents = new List<BlockEvent>();

    static ImportantInteractEventPool()
    {
        var initEvents = Events.events.Where(e =>
        e is InteractEvent1
        or InteractEvent2
        or InteractEvent3
        or InteractEvent4
        or InteractEvent5
        or InteractEvent6
        or InteractEvent7
        or InteractEvent8
        or InteractEvent9
        or InteractEvent10)
            .ToList();
        _interactEvents.AddRange(initEvents);
    }
    public static void AddEvent(BlockEvent interactEvent)
    {
        if (interactEvent == null) throw new ArgumentNullException(nameof(interactEvent));
        if (_interactEvents.Any(e => e.eventId == interactEvent.eventId))
            throw new InvalidOperationException($"事件ID {interactEvent.eventId} 已存在");

        _interactEvents.Add(interactEvent);
    }

    public static bool RemoveEvent(int eventId)
    {
        var targetEvent = _interactEvents.FirstOrDefault(e => e.eventId == eventId);
        if (targetEvent == null) return false;

        _interactEvents.Remove(targetEvent);
        return true;
    }

    public static BlockEvent QueryEvent(int eventId)
    {
        return _interactEvents.FirstOrDefault(e => e.eventId == eventId);
    }

    public static List<BlockEvent> QueryAllEvents()
    {
        return new List<BlockEvent>(_interactEvents);
    }

    public static bool UpdateEvent(int eventId, BlockEvent newEvent)
    {
        if (newEvent == null) throw new ArgumentNullException(nameof(newEvent));
        var targetIndex = _interactEvents.FindIndex(e => e.eventId == eventId);
        if (targetIndex == -1) return false;
        _interactEvents[targetIndex] = newEvent;
        return true;
    }

    public static Block GetImportantInteractEvent()
    {
        if (_interactEvents.Count == 0)
        {
            return null;
        }
        var random = new Random();
        int randomIndex = random.Next(0, _interactEvents.Count);
        BlockEvent randomEvent = _interactEvents[randomIndex];
        return new Block(E_BlockType.重要和ta互动, randomEvent);
    }
}

