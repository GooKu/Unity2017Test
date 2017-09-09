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

    private void Start()
    {
        for (int i = 0; i < testNumber; i++)
        {
            GameObject box = Instantiate(activeTestBoxSample);
            activeBoxList.Add(box);
        }
    }

    public void OnActiveGameobjectTest(bool isActive)
    {
        for (int i = 0; i < testNumber; i++)
        {
            Renderer[] renders = activeBoxList[i].GetComponentsInChildren<Renderer>();
            for (int j = 0; j < renders.Length; j++)
            {
                if(!renders[j].enabled)
                    renders[j].enabled = true;
            }
        }

        Stopwatch sw = new Stopwatch();
        sw.Start();
        for (int i = 0; i < testNumber; i++)
        {
            activeBoxList[i].SetActive(isActive);
        }
        sw.Stop();
		showResult($"ActiveGameobject, active:${isActive}, result:{sw.ElapsedMilliseconds}ms");
    }

    public void OnEnableRenderTest(bool enable)
    {
        for (int i = 0; i < testNumber; i++)
        {
            if (!activeBoxList[i].activeSelf)
                activeBoxList[i].SetActive(true);
        }

        Stopwatch sw = new Stopwatch();
        sw.Start();
        for (int i = 0; i < testNumber; i++)
        {
            Renderer[] renders = activeBoxList[i].GetComponentsInChildren<Renderer>();
            for(int j = 0; j < renders.Length; j++)
            {
                renders[j].enabled = enable;
            }
        }
        sw.Stop();
        showResult($"EnableRender, active:${enable}, result:{sw.ElapsedMilliseconds}ms");
    }

    private void showResult(string result)
    {
        UnityEngine.Debug.Log(result);
        resultText.text = result;
    }
}
