using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject Player_Bullet;
    private BulletControll bullet;
    [SerializeField] private Vector3 weapon_distance_y;
    [SerializeField] private Vector3 weapon_distance_x;
    private void Awake()
    {
        bullet = Player_Bullet.GetComponent<BulletControll>();

        weapon_distance_y = new Vector3(0, 0.2f, 0);
        weapon_distance_x = new Vector3(0.5f, 0, 0);
    }
    public void TryAttack(int count)
    {
        switch(count)
        {
            case 1:
                Instantiate(Player_Bullet, transform.position+weapon_distance_x, Quaternion.identity);
                break;                                       
            case 2:                                          
                Instantiate(Player_Bullet, transform.position+weapon_distance_x + weapon_distance_y, Quaternion.identity);
                Instantiate(Player_Bullet, transform.position+weapon_distance_x - weapon_distance_y, Quaternion.identity);
                break;                                      
            case 3:                                         
                Instantiate(Player_Bullet, transform.position+weapon_distance_x + weapon_distance_y*2, Quaternion.identity);
                Instantiate(Player_Bullet, transform.position+weapon_distance_x, Quaternion.identity);
                Instantiate(Player_Bullet, transform.position+weapon_distance_x - weapon_distance_y*2, Quaternion.identity);
                break;                                      
            case 4:                                         
                Instantiate(Player_Bullet, transform.position+weapon_distance_x + weapon_distance_y*3, Quaternion.identity);
                Instantiate(Player_Bullet, transform.position+weapon_distance_x + weapon_distance_y, Quaternion.identity);
                Instantiate(Player_Bullet, transform.position+weapon_distance_x - weapon_distance_y, Quaternion.identity);
                Instantiate(Player_Bullet, transform.position+weapon_distance_x - weapon_distance_y*3, Quaternion.identity);
                break;                                      
            case 5:                                         
                Instantiate(Player_Bullet, transform.position + weapon_distance_y * 4, Quaternion.identity);
                Instantiate(Player_Bullet, transform.position+weapon_distance_x + weapon_distance_y * 2, Quaternion.identity);
                Instantiate(Player_Bullet, transform.position+weapon_distance_x, Quaternion.identity);
                Instantiate(Player_Bullet, transform.position+weapon_distance_x - weapon_distance_y * 2, Quaternion.identity);
                Instantiate(Player_Bullet, transform.position - weapon_distance_y * 4, Quaternion.identity);
                break;
        }
    }

    public void SetWeapon(float damage)
    {
        bullet.SetDamage(damage);
    }
    public float GetWeaponDamage()
    {
        return bullet.GetDamage();
    }
    public float GetOrizinalWeaponDamage()
    {
        return bullet.GetOrizinalDamage();

    }
    public void BulletChange(GameObject change_bullet)
    {
        Player_Bullet = change_bullet;
        bullet = Player_Bullet.GetComponent<BulletControll>();

    }
    //private IEnumerator TryAttack_co()
    //{
    //    while(true)
    //    {
    //        Instantiate(Player_Bullet, transform.position, Quaternion.identity);
    //        yield return new WaitForSeconds(Attack_Rate);
    //    }
    //}
    //public void StartFire()
    //{
    //    StartCoroutine(TryAttack_co());
    //}
    //public void StopFire()
    //{
    //    StopCoroutine(TryAttack_co());
    //}
}
