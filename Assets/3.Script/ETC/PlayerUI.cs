using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Text Hp;
    [SerializeField] private Text Atk;
    [SerializeField] private GameObject Item_Effect;
    [SerializeField] private Transform Canvas;
    [SerializeField] private Player_Controller player;
    Text Item_Effect_Text;
    private int player_hp;
    private int player_atk;

    private void Awake()
    {
        player_hp = 0;
        player_atk = 0;
        Item_Effect_Text = Item_Effect.GetComponent<Text>();
        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out player);

    }

    public void SetHp(int hp)
    {
        player_hp = hp;
        Hp.text = $"Hp : {player_hp}";
    }
    public void SetAtk(int atk)
    {
        player_atk = atk;
        Atk.text = $"Atk : {player_atk}";
    }
    
    public void SetEffect(string text)
    {
        Item_Effect_Text.text = $"{text}";

        StartCoroutine(Effect_co());
    }
    private IEnumerator Effect_co()
    {
        SpawnEffect(player);
        Item_Effect.SetActive(true);
        yield return new WaitForSeconds(1f);
        Item_Effect.SetActive(false);
    }
    private void SpawnEffect(Player_Controller player)
    {
        Item_Effect.transform.SetParent(Canvas);
        Item_Effect.transform.localScale = Vector3.one;
        Item_Effect.GetComponent<EffectPositionSetter>().SetUp(player.gameObject);
    }
}
