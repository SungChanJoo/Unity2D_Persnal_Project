using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpPositionSetter : MonoBehaviour
{
    [SerializeField] private Vector3 distance = Vector3.down * 35f;

    private GameObject Target;
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
