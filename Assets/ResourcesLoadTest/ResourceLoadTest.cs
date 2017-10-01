using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ResourceLoadTest : MonoBehaviour
{
    [SerializeField]
    private string[] resourceName = new string[] { "T1", "T2", "T3" };
    [SerializeField]
    private int testNumber = 10000;
    [SerializeField]
    private UnityEngine.UI.Text resultText;

    public void OnResourcesLoadTest()
    {
        int resourceCount = resourceName.Length;
        GameObject gObj;
        Stopwatch sw = new Stopwatch();
        sw.Start();
        for (int i = 0; i < testNumber; i++)
        {
            for(int j = 0; j < resourceCount; j++)
            {
                gObj = Resources.Load<GameObject>(resourceName[j]);
            }
        }
        sw.Stop();
        showResult($"OnResourcesLoadTest, result:{sw.ElapsedMilliseconds}ms");
    }

    public void OnDicGetTest()
    {
        int resourceCount = resourceName.Length;
        Dictionary<string, GameObject> resourcesDic = new Dictionary<string, GameObject>();
        for (int j = 0; j < resourceCount; j++)
        {
            resourcesDic.Add(resourceName[j], Resources.Load<GameObject>(resourceName[j]));
        }
        GameObject gObj;
        Stopwatch sw = new Stopwatch();
        sw.Start();
        for (int i = 0; i < testNumber; i++)
        {
            for (int j = 0; j < resourceCount; j++)
            {
                gObj = resourcesDic[resourceName[j]];
            }
        }
        sw.Stop();
        showResult($"OnDicGetTest, result:{sw.ElapsedMilliseconds}ms");
    }

    private void showResult(string result)
    {
        UnityEngine.Debug.Log(result);
        resultText.text = result;
    }
}
