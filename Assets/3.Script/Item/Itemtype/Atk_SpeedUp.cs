using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atk_SpeedUp : Item
{
    [SerializeField] private Player_Controller player;

    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out player);
    }

    public override void OnEvent()
    {
        player.Atk_SpeedUp();
        Destroy(gameObject);

    }
}
