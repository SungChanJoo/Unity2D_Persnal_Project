using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControll : MonoBehaviour
{
    [SerializeField] private Stage_Data stagedata;
    private float destroyWeight = -4f;
    [SerializeField] private float dmg = 1f;
    private float Changed_dmg;

    private void Awake()
    {
        Changed_dmg = dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {

            Destroy(gameObject);
        }
        else if (collision.CompareTag("ItemEnemy"))
        {

            Destroy(gameObject);
        }
        else if (collision.CompareTag("Boss"))
        {

            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        if (transform.position.y < stagedata.LimitMin.y ||
            transform.position.y > stagedata.LimitMax.y ||
            transform.position.x < stagedata.LimitMin.x ||
            transform.position.x > stagedata.LimitMax.x + destroyWeight)
        {
            Destroy(gameObject);
        }
    }

    public void SetDamage(float damage)
    {
        Changed_dmg = damage;
    }
    public float GetOrizinalDamage()
    {
        return dmg;
    }
    public float GetDamage()
    {
        return Changed_dmg;
    }
}
