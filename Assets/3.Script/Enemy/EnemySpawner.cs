using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int Enemy_count = 10;
    [SerializeField] private int TakeOutEnemy_count = 0;

    [SerializeField] private List<GameObject> Enemy_Prefabs;
    [SerializeField] private Stage_Data stagedata;
    [SerializeField] private float SpawnTime;

    private Vector3 PoolPosition = new Vector3(0, -25f, 0);
    private Queue<GameObject> Enemy_q;

    [SerializeField] private float wave = 0;
    [SerializeField] private float Maxwave = 2;
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

        StartCoroutine(SpawnSlime_co());


    }

    public void TakeOut_enemy(Vector3 position)
    {
        if(Enemy_q.Count <= 0)
        {
            return;
        }
        GameObject enemy = Enemy_q.Dequeue();
        if(!enemy.activeSelf)
        {
            enemy.SetActive(true);
        }
        enemy.transform.position = position;
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

    private IEnumerator SpawnSlime_co()
    {
        WaitForSeconds wfs = new WaitForSeconds(SpawnTime);


        while (true)
        {

            float positionY = Random.Range(stagedata.LimitMin.y + 3.6f, stagedata.LimitMax.y - 3f);
            Vector3 position = new Vector3(stagedata.LimitMax.x + 1f, positionY, 0);
            TakeOut_enemy(position);
            if(TakeOutEnemy_count % 10 == 0)
            {
                wave++;
                yield return new WaitForSeconds(SpawnTime*10f);

            }
            if(wave > Maxwave)
            {
                wave = 0;
                break;
            }
            yield return wfs;
        }
        StartCoroutine(SpawnGhoul_co());

    }
    private IEnumerator SpawnGhoul_co()
    {
        WaitForSeconds wfs = new WaitForSeconds(SpawnTime);

        for (int i = 0; i < Enemy_count; i++)
        {
            GameObject enemy = Instantiate(Enemy_Prefabs[1], PoolPosition, Quaternion.identity);
            enemy.SetActive(false);
            Enemy_q.Enqueue(enemy);
        }

        while (true)
        {

            float positionY = Random.Range(stagedata.LimitMin.y + 3.6f, stagedata.LimitMax.y - 3f);
            Vector3 position = new Vector3(stagedata.LimitMax.x + 1f, positionY, 0);
            TakeOut_enemy(position);
            if (TakeOutEnemy_count % 10 == 0)
            {
                wave++;
                yield return new WaitForSeconds(SpawnTime * 10f);

            }
            if (wave > Maxwave)
            {
                wave = 0;
                break;

            }
            yield return wfs;
        }
        SpawnBoss();

    }
    private void SpawnBoss()
    {
        GameObject enemy = Instantiate(Enemy_Prefabs[2], PoolPosition, Quaternion.identity);
        Vector3 position = new Vector3(stagedata.LimitMax.x + 1f, 3f, 0);
        if (!enemy.activeSelf)
        {
            enemy.SetActive(true);
        }
        enemy.transform.position = position;
        SpawnEnemy(enemy.GetComponent<EnemyControll>());
        enemy.GetComponent<EnemyControll>().isBoss = true;
    }
    private void SpawnEnemy(EnemyControll enemy)
    {

        GameObject HpClone = Instantiate(Enemy_Hp);
        HpClone.transform.SetParent(Canvas);
        HpClone.transform.localScale = Vector3.one;
        HpClone.GetComponent<BossHpPositionSetter>().SetUp(enemy.gameObject);
        HpClone.GetComponent<EnemyHpViewer>().SetUp(enemy);
    }
}
