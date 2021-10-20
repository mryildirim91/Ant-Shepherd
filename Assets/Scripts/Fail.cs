using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fail : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.transform.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
        }
    }

    private void OnBecameInvisible()
    {
        
    }
}
