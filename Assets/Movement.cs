using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    void Update()
    {
        Move();   
    }

    private void Move()
    {
        transform.Translate(transform.up * _speed * Time.deltaTime,Space.World);
    }
}
