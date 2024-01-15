using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atk_DamageUp : Item
{
    [SerializeField] private Player_Controller player;

    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out player);
    }

    public override void OnEvent()
    {
        player.WeaponDamageChanger();
        Destroy(gameObject);
    }
}
