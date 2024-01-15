using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEnemyControll : MonoBehaviour
{
    [SerializeField] private Player_Controller player;
    [SerializeField] private Stage_Data stagedata;
    [SerializeField] private ItemEnemySpawner ItemEnemy_Spawner;
    [SerializeField] private Movement2D movement2D;
    private int MaxHp = 1;

    private Item item;
    private GameObject Enemy_Item;
    private float currentHp;

    public float MAXHP => MaxHp;
    public float CurrentHp => currentHp;

    [SerializeField] private SpriteRenderer renderer;
    private bool isDie;
    private Animator animator;

    private void OnEnable()
    {
        MaxHp += UnityEngine.Random.Range(1, 5);
        MaxHp += (int)(MaxHp *1.5f); 
        currentHp = MaxHp;
        movement2D = GetComponent<Movement2D>();
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out player);
        GameObject.FindGameObjectWithTag("ItemEnemySpawner").TryGetComponent(out ItemEnemy_Spawner);
        renderer.color = Color.white;
        isDie = false;
        animator.SetBool("isDie", false);
        movement2D.MoveTo(new Vector3(-1, 0, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(1);
        }
        else if(collision.CompareTag("Bullet"))
        {
            BulletControll bullet = collision.GetComponent<BulletControll>();
            TakeDamage(player.damage);
        }
    }
    private IEnumerator OnDie_co()
    {
        animator.SetBool("isDie", true);
        movement2D.MoveTo(new Vector3(1, 0, 0));
        yield return new WaitForSeconds(0.3f);
        item.OnEvent();
        OnDie();
    }
    public void OnDie()
    {
        MaxHp += (int)(MaxHp * 10f);
        ItemEnemy_Spawner.TakeIn_enemy(gameObject);
        Destroy(Enemy_Item);
        isDie = true;
    }

    private void Update()
    {
        if (transform.position.x < stagedata.LimitMin.x - 2f)
        {
            OnDie();
        }
    }

    public void TakeDamage(float Damage)
    {
        currentHp -= Damage;
        if (currentHp <= 0)
        {
            StartCoroutine(OnDie_co());
        }
        if (!isDie)
        {
            StopCoroutine("Hitanimation_co");
            StartCoroutine("Hitanimation_co");
        }

    }
    private IEnumerator Hitanimation_co()
    {
        renderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        renderer.color = Color.white;
    }
    public void SpawnItem()
    {
        Enemy_Item = Instantiate(ItemEnemy_Spawner.Enemy_Item);
        item = Enemy_Item.GetComponent<Item>();
        Enemy_Item.GetComponent<EnemyItemPosSetter>().SetUp(gameObject);
    }
}
