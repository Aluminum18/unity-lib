# unity-lib
Frequently used features

## Scriptable Variables and Events
* Purpose:
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
