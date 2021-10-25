
using System;

public static class EventManager
{
    public static Action ONAntEntersAnthillEvent;
    public static Action ONAntDeathEvent;
    public static Action ONLevelCompleteEvent;
    public static Action ONGameOverEvent;

    public static void TriggerAntEntersAnthillEvent()
    {
        ONAntEntersAnthillEvent?.Invoke();
    }
    public static void TriggerAntDeathEvent()
    {
        ONAntDeathEvent?.Invoke();
    }

    public static void TriggerLevelCompleteEvent()
    {
        ONLevelCompleteEvent?.Invoke();
    }

    public static void TriggerGameOverEvent()
    {
        ONGameOverEvent?.Invoke();
    }
}
