using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemPosSetter : MonoBehaviour
{
    [SerializeField] private Vector3 distance = Vector3.up * 35f;

    private GameObject Target; //�޸� ���ʹ�
    private RectTransform UItransform;

    public void SetUp(GameObject target)
    {
        Target = target;
        UItransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(!Target.activeSelf)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(Target.transform.position);

        UItransform.position = screenPosition + distance;
    }
}
