using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : Item
{
    [SerializeField] private Player_Controller player;
    [SerializeField] private GameObject bullet;
    private void Awake()
    {

        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out player);
    }

    public override void OnEvent()
    {
        player.ChangeWeapon(bullet);
        Destroy(gameObject);
    }
}
