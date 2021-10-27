using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Death : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.transform.CompareTag("Obstacle"))
        {
            if(other.gameObject.GetComponent<Obstacle>().ObstacleType == ObstacleType.Deadly)
                Destroy();
        }
    }
    
    private void Destroy()
    {
        GetComponent<Movement>().StopMoving();
        EventManager.TriggerAntDeathEvent();
        GetComponentInChildren<Animator>().SetTrigger(Animator.StringToHash("Death"));
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.DOFade(0, 5f);
    }
}
