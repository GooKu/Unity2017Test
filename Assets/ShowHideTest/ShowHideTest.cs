using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class ShowHideTest : MonoBehaviour
{
    [SerializeField]
    private GameObject activeTestBoxSample;
    [SerializeField]
    private int testNumber = 10000;
    [SerializeField]
    private UnityEngine.UI.Text resultText;
    [SerializeField]
    private UnityEngine.UI.Text fpsText;

    private List<GameObject> activeBoxList = new List<GameObject>();
    private Dictionary<int, Renderer[]> renderDic = new Dictionary<int, Renderer[]>();

    private Vector3 moveOutPosition = new Vector3(99999, 99999, 99999);
    private int hideLayer;

    private void Start()
    {
        hideLayer = LayerMask.NameToLayer("Hide");
        for (int i = 0; i < testNumber; i++)
        {
            GameObject testObj = Instantiate(activeTestBoxSample);
            activeBoxList.Add(testObj);
            Renderer[] renders = testObj.GetComponentsInChildren<Renderer>();
            renderDic.Add(i, renders);
        }
    }

    private void Update()
    {
        fpsText.text = string.Format("{0}fps",(int)(1/Time.deltaTime));
    }

    public void OnActiveGameobjectTest(bool isActive)
    {
        for (int i = 0; i < testNumber; i++)
        {
            activeBoxList[i].transform.position = Vector3.zero;
            activeBoxList[i].transform.localScale = Vector3.one;
            Renderer[] renders = renderDic[i];
            for (int j = 0; j < renders.Length; j++)
            {
                renders[j].enabled = true;
                renders[j].gameObject.layer = 0;
            }
        }

        Stopwatch sw = new Stopwatch();
        sw.Start();
        for (int i = 0; i < testNumber; i++)
            activeBoxList[i].SetActive(isActive);

        sw.Stop();
		showResult($"ActiveGameobject, active:${isActive}, result:{sw.ElapsedMilliseconds}ms");
    }

    public void OnEnableRenderTest(bool enable)
    {
        for (int i = 0; i < testNumber; i++)
        {
            activeBoxList[i].transform.position = Vector3.zero;
            activeBoxList[i].transform.localScale = Vector3.one;
            Renderer[] renders = renderDic[i];
            for (int j = 0; j < renders.Length; j++)
                renders[j].gameObject.layer = 0;
        }

        Stopwatch sw = new Stopwatch();
        sw.Start();
        for (int i = 0; i < testNumber; i++)
        {
            Renderer[] renders = renderDic[i];
            for (int j = 0; j < renders.Length; j++)
                renders[j].enabled = enable;
        }
        sw.Stop();
        showResult($"EnableRender, active:${enable}, result:{sw.ElapsedMilliseconds}ms");
    }

    public void OnMoveGameobjectTest(bool isMoveOut)
    {
        for (int i = 0; i < testNumber; i++)
        {
            activeBoxList[i].SetActive(true);
            activeBoxList[i].transform.localScale = Vector3.one;
            Renderer[] renders = renderDic[i];
            for (int j = 0; j < renders.Length; j++)
            {
                renders[j].enabled = true;
                renders[j].gameObject.layer = 0;
            }
        }

        Vector3 position = isMoveOut? moveOutPosition : Vector3.zero;

        Stopwatch sw = new Stopwatch();
        sw.Start();
        for (int i = 0; i < testNumber; i++)
            activeBoxList[i].transform.position = position;

        sw.Stop();
        showResult($"MoveGameobject, move out:${isMoveOut}, result:{sw.ElapsedMilliseconds}ms");
    }

    public void OnLayerMaskTest(bool isHideLayer)
    {
        for (int i = 0; i < testNumber; i++)
        {
            activeBoxList[i].SetActive(true);
            activeBoxList[i].transform.position = Vector3.zero;
            activeBoxList[i].transform.localScale = Vector3.one;
            Renderer[] renders = renderDic[i];
            for (int j = 0; j < renders.Length; j++)
                renders[j].enabled = true;
        }

        int layer = isHideLayer ? hideLayer : 0;

        Stopwatch sw = new Stopwatch();
        sw.Start();
        for (int i = 0; i < testNumber; i++)
        {
            Renderer[] renders = renderDic[i];
            for (int j = 0; j < renders.Length; j++)
                renders[j].gameObject.layer = layer;
        }

        sw.Stop();
        showResult($"LayerMask, hide layer:${isHideLayer}, result:{sw.ElapsedMilliseconds}ms");
    }

    public void OnScaleTest(bool isScaleZero)
    {
        for (int i = 0; i < testNumber; i++)
        {
            activeBoxList[i].SetActive(true);
            activeBoxList[i].transform.position = Vector3.zero;
            Renderer[] renders = renderDic[i];
            for (int j = 0; j < renders.Length; j++)
            {
                renders[j].enabled = true;
                renders[j].gameObject.layer = 0;
            }
        }

        Vector3 scale = isScaleZero ? Vector3.zero : Vector3.one;

        Stopwatch sw = new Stopwatch();
        sw.Start();
        for (int i = 0; i < testNumber; i++)
            activeBoxList[i].transform.localScale = scale;

        sw.Stop();
        showResult($"Scale, scale zero:${isScaleZero}, result:{sw.ElapsedMilliseconds}ms");
    }

    private void showResult(string result)
    {
        UnityEngine.Debug.Log(result);
        resultText.text = result;
    }
}
