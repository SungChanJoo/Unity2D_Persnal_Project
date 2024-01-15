using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyControll : MonoBehaviour
{
    [SerializeField] private Player_Controller player;
    [SerializeField] private Stage_Data stagedata;
    [SerializeField] private EnemySpawner EnemySpawner;
    [SerializeField] private Movement2D movement2D;
    [SerializeField] private float MaxHp = 2f;
    private float currentHp;

    public float MAXHP => MaxHp;
    public float CurrentHp => currentHp;

    [SerializeField] private SpriteRenderer renderer;
    private bool isDie;
    private Animator animator;
    public bool isBoss;

    private void OnEnable()
    {
        currentHp = MaxHp;
        movement2D = GetComponent<Movement2D>();
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out player);
        GameObject.FindGameObjectWithTag("EnemySpawner").TryGetComponent(out EnemySpawner);
        renderer.color = Color.white;
        isDie = false;
        isBoss = false;
        animator.SetBool("isDie", false);
        movement2D.MoveTo(new Vector3(-1, 0, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(1);
        }
        else if (collision.CompareTag("Bullet"))
        {
            BulletControll bullet = collision.GetComponent<BulletControll>();
            TakeDamage(player.damage);
        }
    }
    private IEnumerator OnDie_co()
    {
        animator.SetBool("isDie", true);
        movement2D.MoveTo(new Vector3(1,0,0));
        yield return new WaitForSeconds(0.3f);
        OnDie();
    }
    public void OnDie()
    {

        EnemySpawner.TakeIn_enemy(gameObject);
        isDie = true;
        if (isBoss)
        {
            SceneManager.LoadScene("GameEnd");

        }
    }

    private void Update()
    {
        if(transform.position.x < stagedata.LimitMin.x -2f)
        {
            OnDie();
        }
    }

    public void TakeDamage(float Damage)
    {
        currentHp -= Damage;

        if(!isDie)
        {
            StopCoroutine("Hitanimation_co");
            StartCoroutine("Hitanimation_co");
        }
        if(currentHp <= 0)
        {
            StartCoroutine(OnDie_co());
        }
    }
    private IEnumerator Hitanimation_co()
    {
        renderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        renderer.color = Color.white;
    }
}
