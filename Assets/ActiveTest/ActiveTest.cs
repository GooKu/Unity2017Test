using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class ActiveTest : MonoBehaviour
{
    [SerializeField]
    private GameObject activeTestBoxSample;
    [SerializeField]
    private int testNumber = 10000;
    [SerializeField]
    private UnityEngine.UI.Text resultText;

    private List<GameObject> activeBoxList = new List<GameObject>();
    private bool isAvtive = true;

    private void Start()
    {
        for (int i = 0; i < testNumber; i++)
        {
            GameObject box = Instantiate(activeTestBoxSample);
            activeBoxList.Add(box);
        }
    }

    public void OnActiveGameobjectTest()
    {
        isAvtive = !isAvtive;

        Stopwatch sw = new Stopwatch();
        sw.Start();
        for (int i = 0; i < testNumber; i++)
        {
            activeBoxList[i].SetActive(isAvtive);
        }
        sw.Stop();
		showResult($"ActiveGameobject, active:${isAvtive}, result:{sw.ElapsedMilliseconds}ms");
    }

    public void OnEnableRenderTest()
    {
        isAvtive = !isAvtive;
        Stopwatch sw = new Stopwatch();
        sw.Start();
        for (int i = 0; i < testNumber; i++)
        {
            Renderer[] renders = activeBoxList[i].GetComponentsInChildren<Renderer>();
            for(int j = 0; j < renders.Length; j++)
            {
                renders[j].enabled = isAvtive;
            }
        }
        sw.Stop();
        showResult($"EnableRender, active:${isAvtive}, result:{sw.ElapsedMilliseconds}ms");
    }

    private void showResult(string result)
    {
        UnityEngine.Debug.Log(result);
        resultText.text = result;
    }
}
