using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperaterSpanwner : MonoBehaviour
{
    [SerializeField] private Stage_Data stagedata;
    [SerializeField] private float SpawnTime;
    [SerializeField] private GameObject Operater_Prefab;
    [SerializeField] private GameObject op_UI;
    [SerializeField] private Transform Canvas;

    private int oper_num=0;
    private void Awake()
    {
        StartCoroutine(SpawnOperater_co());
    }
    private IEnumerator SpawnOperater_co()
    {
        WaitForSeconds wfs = new WaitForSeconds(SpawnTime);

        while (true)
        {

            Vector3 UpPosition = new Vector3(stagedata.LimitMax.x + 1f, 1.5f, 0);
            Vector3 DownPosition = new Vector3(stagedata.LimitMax.x + 1f, -1.5f, 0);
            GameObject OperUp = Instantiate(Operater_Prefab, UpPosition, Quaternion.Euler(0,0,-90));

            SpawnOperater(OperUp.GetComponent<OperaterCreater>());
            ChangeColor(OperUp);

            GameObject OperDown = Instantiate(Operater_Prefab, DownPosition, Quaternion.Euler(0, 0, -90));
            SpawnOperater(OperDown.GetComponent<OperaterCreater>());
            ChangeColor(OperDown);

            yield return wfs;
        }
    }
    private void SpawnOperater(OperaterCreater operater)
    {

        GameObject OpClone = Instantiate(op_UI);
        OpClone.transform.SetParent(Canvas);
        OpClone.transform.localScale = Vector3.one;
        OpClone.GetComponent<OperaterPositionSetter>().SetUp(operater.gameObject);
        OpClone.GetComponent<OperaterViewer>().SetUp(operater);
        operater.receiveOpClone(OpClone);
        oper_num = OpClone.GetComponent<OperaterViewer>().GetOperater();
    }
    
    private void ChangeColor(GameObject gameObject)
    {
        if (oper_num == 1 || oper_num == 3)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }


}
