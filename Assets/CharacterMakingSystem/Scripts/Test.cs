using Cysharp.Threading.Tasks;
using UnityEngine;

public class Test : MonoBehaviour
{

    private async UniTaskVoid Start()
    {
        var system = new CharacterMakingSystem.CharacterMakingSystem();
        var data = await system.EntryCharacterMaking();
        Debug.Log(data);
    }

    // private void Update()
    // {
    //     float fps = 1.0f / Time.deltaTime;
    //     float mem = (Profiler.GetTotalAllocatedMemoryLong() >> 10) / 1024f;
    //     float unused = (Profiler.GetTotalUnusedReservedMemoryLong() >> 10) / 1024f;
    //
    //     //systemText.text = fps.ToString("0.00") + " FPS\nMemory: " + mem + " / " + unused;
    // }
}
