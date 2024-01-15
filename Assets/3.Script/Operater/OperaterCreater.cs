using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//플레이어와 닿으면 사라지고
//텍스트에 있는 연산자를 가져와서 플레이어의 메소드를 활성화 시킨다.

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
            //연산자 받아와야함
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
