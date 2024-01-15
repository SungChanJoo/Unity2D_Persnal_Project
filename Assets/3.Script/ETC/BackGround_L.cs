using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround_L : MonoBehaviour
{
    [SerializeField] private float ScrollRange = 1f;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= -ScrollRange)
        {
            BackGroundOffset();
        }
    }

    public void BackGroundOffset()
    {
        Vector2 offset = new Vector2(ScrollRange * 2f, 0);
        transform.position = (Vector2)transform.position + offset;
    }
}
