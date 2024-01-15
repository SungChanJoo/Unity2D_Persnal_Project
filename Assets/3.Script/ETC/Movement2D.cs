using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    public float Move_Speed = 0f;
    [SerializeField] private Vector3 MoveDirection = Vector3.zero;

    private void Update()
    {
        transform.position += MoveDirection * Move_Speed * Time.deltaTime;
    }

    public void MoveTo(Vector3 direction)
    {
        MoveDirection = direction;
    }
}
