using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu] //에셋으로 만들기 위한 방법.
public class Stage_Data : ScriptableObject //유니티에서 데이터를 저장하는 포맷
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
