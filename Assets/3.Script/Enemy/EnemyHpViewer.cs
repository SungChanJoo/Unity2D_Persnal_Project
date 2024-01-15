using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpViewer : MonoBehaviour
{
    private ItemEnemyControll Itemenemy;
    private EnemyControll enemy;
    private Text text;

    public void SetUp(ItemEnemyControll enemy)
    {
        this.Itemenemy = enemy;
        TryGetComponent(out text);
    }
    public void SetUp(EnemyControll enemy)
    {
        this.enemy = enemy;
        TryGetComponent(out text);
    }
    private void Update()
    {
        if(Itemenemy != null)
        {
            if (Itemenemy.CurrentHp <= 0)
            {
                text.text = $"0";
            }
            else
            {
                text.text = $"{Itemenemy.CurrentHp}";
            }
        }
        else
        {
            if (enemy.CurrentHp <= 0)
            {
                text.text = $"0";
            }
            else
            {
                text.text = $"{enemy.CurrentHp}";
            }
        }

    }
}
