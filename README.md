# unity-lib
Frequently used logic
# Content
- [ScriptableObject Variables and Events](#scriptableobject-variables-and-events)
- [Common Async Actions](#common-async-actions)
- [Object Pooling](#object-pooling)
- [UnityWebRequest Wrapper](#unitywebrequest-wrapper)

## ScriptableObject Variables and Events
* Git url: https://github.com/Aluminum18/unity-lib.git?path=Assets/ScriptableObjectSystem/MainModules
* Description:
  + Implement [ScriptableObject architecture](https://www.youtube.com/watch?v=raQ3iHhE_Kk&ab_channel=Unity) which helps code becomes modular, editable and debuggable.
  + Additional: 
    - **ScriptableObject Event:** allow passing parameter when raising event; add editor script for better debugging.
      
      ![image](https://github.com/Aluminum18/unity-lib/assets/14157400/d5807985-54b1-40f1-9752-2e5f1ae7c991)
      
      **Event Explanation:** Dev's note area.
      
      **Log When Raise:** Helps debugging.
      
      **Subcribers:** List of objects that aware this event. Useful for debugging.
      
      **Raise button:** Raise this event from Inspector without Raiser. Useful for testing your feature independently if Raiser is not ready (such as other member is developing Raiser).
      
      **Raise With Params button:** Allow raising event with parameter(s) from Inspector. Declare you parameter in **Event Params** by specifying class name and its value.

    - **ScriptableObject Message:** Similar to ScriptableObject Event except a Message Listener only listens messages from specified MessageBroadcaster while EventListener always listen specified Events regardless Raiser. 

## Common Async Actions
* Git url: https://github.com/Aluminum18/unity-lib.git?path=Assets/Common/CommonActions
* Dependency: [UniTask](https://github.com/Cysharp/UniTask)
* Description: Bring async actions to inspector to reduce coding effort.
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
    
## Object Pooling
* Git url: https://github.com/Aluminum18/unity-lib.git?path=Assets/Common/ObjectPooling
* Description: Helper for minimizing effort to spawn recycle objects
  
  - **MultiplePools:** Single contact point for spawning any object
  ```csharp
    public class ObjectPoolTest : MonoBehaviour
    {
        public int poolSize = 10;
        public void SpawnOject(GameObject templateObject)
        {
            var clone = MultiplePools.Instance.SpawnGameObject(templateObject, poolSize);
            //  Cloned object will automatically return to Pool when it is disabled
        }
    }
  ```
  - **ObjectSpawner:** No coding Object Spawner. You can use ScriptableObject Event as 'Spawn Command' (see Example gif in [SequencialActions](sequentialactions) to spawn recycle object without coding.

    
  ![image](https://github.com/Aluminum18/unity-lib/assets/14157400/74a8669c-0889-4ac9-87db-8a59f59c1a4b)

   **Spawn()**: Spawn _Object In Pool's_ clone at _SpawnPos_ (world space) and in _Parent Transform_
  
   **SpawnRandomObject()**: Spawn randomly clone of objects in _Random Obj List_. Position and Parent similar to Spawn()
  
   **SpawnWithRandomRange()**: Spawn _Object In Pool's_ clone at random position within a box area which has Spawn Pos as center and half of _Random Range Pos_ as size

  ## UnityWebRequest Wrapper
  * Git url: https://github.com/Aluminum18/unity-lib.git?path=Assets/RestClient/Scripts/MainModules
  * Dependency: [UniTask](https://github.com/Cysharp/UniTask)
  * Description: Simplify sending UnityWebRequest logic. Support converting Json and Flatbuffer data type
    ```csharp
    public async UniTaskVoid DoSomeRequests()
    {
        string url = "request url";
        string tokenIfAny = "sample token";
        Tuple<string, string> headerIfAny = new("header1", "headerValue1");

        var getResponseData = await RequestSenderAsync.SendGetRequest(url, tokenIfAny, headerIfAny);
        JsonResponse yourGetResponse = ResponseDataConverter.ConvertJson<JsonResponse>(getResponseData.rawdata);
        // Convert logic uses built-in JsonUtility so your JsonResponse must be a Serialized class/struct

        byte[] postData = new byte[1024];
        var postResponse = await RequestSenderAsync.SendPostRequest(url, postData, tokenIfAny, headerIfAny);
        FlatBufferResponse yourPostResponse = ResponseDataConverter.ConvertFlatBuffer<FlatBufferResponse>(postResponse.rawdata);
        // Your FlatBufferResponse must be a struct implements IFlatbufferObject
    }
    ```
