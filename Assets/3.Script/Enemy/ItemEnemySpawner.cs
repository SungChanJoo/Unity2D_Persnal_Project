using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEnemySpawner : MonoBehaviour
{
    [SerializeField] private int Enemy_count = 10;
    [SerializeField] private int TakeOutEnemy_count = 0;
    [SerializeField] private List<GameObject> Enemy_Prefabs;
    [SerializeField] private Stage_Data stagedata;
    [SerializeField] private float SpawnTime;

    private Vector3 PoolPosition = new Vector3(0f, -25f, 0);
    private Queue<GameObject> Enemy_q;


    [SerializeField] private List<GameObject> Item_Prefabs;
    public GameObject Enemy_Item;

    [SerializeField] private GameObject Enemy_Hp;
    [SerializeField] private Transform Canvas;

    private void Awake()
    {
        Enemy_q = new Queue<GameObject>();

        for (int i = 0; i < Enemy_count; i++)
        {
            GameObject enemy = Instantiate(Enemy_Prefabs[0], PoolPosition, Quaternion.identity);
            enemy.SetActive(false);
            Enemy_q.Enqueue(enemy);
        }
        StartCoroutine(SpawnEnemy_co());


    }

    public void TakeOut_enemy(Vector3 position)
    {
        if (Enemy_q.Count <= 0)
        {
            return;
        }
        GameObject enemy = Enemy_q.Dequeue();

        if (!enemy.activeSelf)
        {
            enemy.SetActive(true);

        }
        enemy.transform.position = position;
        SpawnEnemy_item(enemy.GetComponent<ItemEnemyControll>());
        TakeOutEnemy_count++;

    }
    public void TakeIn_enemy(GameObject enemy)
    {
        enemy.transform.position = PoolPosition;
        if (enemy.activeSelf)
        {
            enemy.SetActive(false);
        }
        Enemy_q.Enqueue(enemy);
    }

    private IEnumerator SpawnEnemy_co()
    {
        WaitForSeconds wfs = new WaitForSeconds(SpawnTime);

        while (true)
        {
            float positionY = Random.Range(stagedata.LimitMin.y + 3.6f, stagedata.LimitMax.y - 3f);
            Vector3 position = new Vector3(stagedata.LimitMax.x + 1f, positionY, 0);
            TakeOut_enemy(position);
            if (TakeOutEnemy_count % 2 == 0)
            {
                yield return new WaitForSeconds(SpawnTime*2f);
            }


            yield return wfs;
        }
    }


    private void SpawnEnemy_item(ItemEnemyControll enemy)
    {
        Enemy_Item = Item_Prefabs[Random.Range(0, Item_Prefabs.Count)];
        enemy.SpawnItem();
        GameObject ItemClone = Instantiate(Enemy_Item);
        ItemClone.transform.SetParent(Canvas);
        ItemClone.transform.localScale = Vector3.one;
        ItemClone.transform.position = PoolPosition;

        ItemClone.GetComponent<EnemyItemPosSetter>().SetUp(enemy.gameObject);

        GameObject HpClone = Instantiate(Enemy_Hp);
        HpClone.transform.SetParent(Canvas);
        HpClone.transform.localScale = Vector3.one;
        HpClone.transform.position = PoolPosition;
        HpClone.GetComponent<EnemyHpPositionSetter>().SetUp(enemy.gameObject);
        HpClone.GetComponent<EnemyHpViewer>().SetUp(enemy);
    }
}
