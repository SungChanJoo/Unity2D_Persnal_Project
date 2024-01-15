using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//�÷��̾�� ������ �������
//�ؽ�Ʈ�� �ִ� �����ڸ� �����ͼ� �÷��̾��� �޼ҵ带 Ȱ��ȭ ��Ų��.

public class OperaterCreater : MonoBehaviour
{

    [SerializeField] private Stage_Data stagedata;
    [SerializeField] private Player_Controller player;

    private int num;
    private int oper_num;
    private GameObject OpClone;
    private void OnEnable()
    {
        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out player);

    }
    private void Update()
    {

        if (transform.position.x < stagedata.LimitMin.x - 2f)
        {
            OnDie();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            OnDie();
            //������ �޾ƿ;���
            OnOperater();
        }
    }

    public void OnOperater()
    {
        oper_num = OpClone.GetComponent<OperaterViewer>().GetOperater();
        num = OpClone.GetComponent<OperaterViewer>().GetNum();
        player.WeaponDamageChanger(oper_num, num);

    }
    public void OnDie()
    {
        Destroy(gameObject);
        Destroy(OpClone);
    }
    public void receiveOpClone(GameObject opclone)
    {
        this.OpClone = opclone;
    }
}
