using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu] //�������� ����� ���� ���.
public class Stage_Data : ScriptableObject //����Ƽ���� �����͸� �����ϴ� ����
{
    [SerializeField] private Vector2 _LimitMin;
    [SerializeField] private Vector2 _LimitMax;

    public Vector2 LimitMin
    {
        get
        {
            return _LimitMin;
        }
    }
    public Vector2 LimitMax
    {
        get
        {
            return _LimitMax;
        }
    }
}
