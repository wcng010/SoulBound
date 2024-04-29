using UnityEngine.Playables;
using UnityEngine.Serialization;

public class TimelineManager:NormSingleton<TimelineManager>
{
    public PlayableDirector TimeLine1;
    public PlayableDirector TimeLine2;
    public PlayableDirector beginTimeline;
    public PlayableDirector endTimeline;
}
