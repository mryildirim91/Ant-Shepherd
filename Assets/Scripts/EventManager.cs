
using System;

public static class EventManager
{
    public static Action<int> ONAntCollectsCandy;
    public static Action ONAntEntersAnthillEvent;
    public static Action ONAntDeathEvent;
    public static Action ONLevelCompleteEvent;
    public static Action ONGameOverEvent;
    public static Action ONLineGoneEvent;

    public static void TriggerAntCollectsCandy(int points)
    {
        ONAntCollectsCandy?.Invoke(points);
    }
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

    public static void TriggerLineGoneEvent()
    {
        ONLineGoneEvent?.Invoke();
    }
}
