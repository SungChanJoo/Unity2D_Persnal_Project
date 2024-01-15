using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{
    [SerializeField]
    private float MaxHp = 3f;
    private float currentHp;

    public float MAXHP => MaxHp;
    public float CurrentHp => currentHp;

    private SpriteRenderer renderer;

    private Movement2D movement2D;
    [SerializeField] private Stage_Data stagedata;
    [SerializeField] private Weapon weapon;
    [SerializeField] private float attack_speed = 0.5f;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerUI PlayerUI;
    private bool isWalk = false;
    public float damage = 1f;
    [SerializeField] private int bullet_count = 1;
    private void Awake()
    {
        movement2D = transform.GetComponent<Movement2D>();
        weapon = transform.GetComponent<Weapon>();
        animator = transform.GetComponent<Animator>();
        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out PlayerUI);
        PlayerUI.SetHp((int)MaxHp);
        PlayerUI.SetAtk((int)damage);
        currentHp = MaxHp;
        TryGetComponent(out renderer);
        weapon.SetWeapon(damage);
        StartCoroutine(Attack_co());
    }

    private void Start()
    {
        if(movement2D.Move_Speed <= 0f)
        {
            movement2D.Move_Speed = 5f;
        }
    }

    private void Update()
    {
        float y = Input.GetAxisRaw("Vertical");

        movement2D.MoveTo(new Vector3(0, y, 0));

    }
    private IEnumerator Attack_co()
    {
        while(true)
        {
            if (!isWalk)//이동중이 아니라면
            {
                weapon.TryAttack(bullet_count);
            }
            yield return new WaitForSeconds(attack_speed);
        }

    }
    private void LateUpdate()
    {
        transform.position = new Vector3
            (
            Mathf.Clamp(transform.position.x, stagedata.LimitMin.x, stagedata.LimitMax.x),
            Mathf.Clamp(transform.position.y, stagedata.LimitMin.y+3.6f, stagedata.LimitMax.y-3f),
            0
            );
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        StopCoroutine(Hitanimation_co());
        StartCoroutine(Hitanimation_co());
        PlayerUI.SetHp((int)currentHp);

        if (currentHp <= 0)
        {
            OnDie();
        }
    }
    private void OnDie()
    {
        //todo1009 게임오버씬추가해줘 성찬아
        SceneManager.LoadScene("GameOver");
        Destroy(gameObject);
    }
    private IEnumerator Hitanimation_co()
    {
        renderer.color = Color.black;
        yield return new WaitForSeconds(0.1f);
        renderer.color = Color.white;
    }

    public void Atk_SpeedUp()
    {
        if(attack_speed<0.1)
        {
            return;
        }
        else
        {
            attack_speed -= attack_speed * 0.2f;
            PlayerUI.SetEffect("공격속도 UP!");
            //todo1010 공속업 효과 추가해줬으면 좋겠다 성찬아...
        }

    }
    public void BulletCountUp()
    {
        if(bullet_count<5)
        {
            bullet_count++;
            PlayerUI.SetEffect("발사체 개수 증가!");
        }
        else
        {
            return;
        }
    }
    public void BulletCountDown()
    {
        if (bullet_count > 1)
        {
            bullet_count--;
            PlayerUI.SetEffect("발사체 개수 감소!");
        }
        else
        {
            return;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Boss"))
        {
            OnDie();
        }
    }
    public void WeaponDamageChanger(int oper = 0, float dmg = 1)
    {
        damage = weapon.GetWeaponDamage();
        if(oper == 3)//나누기
        {
            damage =(int)(damage / dmg);
            PlayerUI.SetEffect("Atk Down!");
        }
        else if(oper == 2)//곱하기
        {
            damage *= dmg;
            PlayerUI.SetEffect("Atk Up!");
        }
        else if(oper == 1) //빼기
        {
            damage -= dmg;
            PlayerUI.SetEffect("Atk Down!");
        }
        else //더하기
        {
            damage += dmg;
            PlayerUI.SetEffect("Atk Up!");
        }
        if(damage <1)
        {
            damage = 1;
        }
        PlayerUI.SetAtk((int)damage);

        weapon.SetWeapon(damage);
    }

    public void ChangeWeapon(GameObject bullet)
    {

        weapon.BulletChange(bullet);
        damage += weapon.GetOrizinalWeaponDamage();
        PlayerUI.SetAtk((int)damage);
        PlayerUI.SetEffect("Atk Up!");
        weapon.SetWeapon(damage);
    }
}
