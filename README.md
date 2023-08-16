# unity-lib
Frequently used features
# Content
- [ScriptableObject Variables and Events](#scriptableobject-variables-and-events)
- [Common Async Actions](#common-async-actions)

## ScriptableObject Variables and Events
* Description:
  + Implement [this original idea](https://www.youtube.com/watch?v=raQ3iHhE_Kk&ab_channel=Unity)
  + Additional: 
    - **ScriptableObject Event:** allow passing parameter when raising event; add editor script for better debugging.
      
      ![image](https://github.com/Aluminum18/unity-lib/assets/14157400/d5807985-54b1-40f1-9752-2e5f1ae7c991)
      
      **Event Explanation:** Dev's note area.
      
      **Log When Raise:** Helps debugging.
      
      **Subcribers:** List of objects that aware this event. Useful for debugging.
      
      **Raise button:** Raise this event from Inspector without Raiser. Useful for testing your feature independently if Raiser is not ready (such as other member is developing Raiser).
      
      **Raise With Params button:** Allow raising event with parameter(s) from Inspector. Declare you parameter in **Event Params** by specifying class name and its value.

    - **ScriptableObject Message:** Similar with ScriptableObject Event except a Message Listener only listens messages from specified MessageBroadcaster while EventListener always listen specified Events regardless Raiser. 
      
* git url: https://github.com/Aluminum18/unity-lib.git?path=Assets/ScriptableObjectSystem/MainModules

## Common Async Actions
* Description: Bring async actions to inspector to reduce coding effort. Built by using [UniTask](https://github.com/Cysharp/UniTask).
  - **SequentialActions:** Invoke actions sequentially with configurable timing.
    ![SequentialActions (online-video-cutter com)](https://github.com/Aluminum18/unity-lib/assets/14157400/1f032da7-1d84-4521-b0ed-5c90135f0044)
  - **FeededAction:** Check following example for easier getting FeededAction idea :)
    
    A bird only sings if he is feeded. When he is hungry, he stops singing. When he is feeded again, he starts singing the song from where he stopped. FeededAction can mimic his behavior with following setup
![image](https://github.com/Aluminum18/unity-lib/assets/14157400/0be985b1-241f-41c5-8703-cd5a11343b49)

    **First Time Feeded Action:** Action invoked on first time Feed()
    
    **On Feeded Action:** Action invoked everytime Feed() including first time
 
    **On Hungry Action:** Action invoked after **Full Time** second(s) since the last Feed()
  - **IntervalAction**: As its name, specified action is invoked every **Interval** second(s)
    ![image](https://github.com/Aluminum18/unity-lib/assets/14157400/07828fc3-0958-48cd-b3fc-9d3e25df1316)

    Tip: You can use ScriptableObject Event as 'Start Action' command for reducing coding effort (like Example gif in [SequencialActions](sequentialactions))
