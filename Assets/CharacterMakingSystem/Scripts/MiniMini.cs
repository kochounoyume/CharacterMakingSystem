using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MiniMini : MonoBehaviour
{
    [SerializeField] private GameObject[] objs;

    // Start is called before the first frame update
    void Start()
    {
        List<float> renderers = new List<float>();
        renderers.AddRange(objs.Select(obGameObject => obGameObject.transform.Find("Face").GetComponent<SkinnedMeshRenderer>().bounds.size.y));
        Debug.Log($"最小値{renderers.Min()}を出したのは{renderers.IndexOf(renderers.Min())}番目の要素です");
    }
}
