using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Anthill : MonoBehaviour
{
    private int _numOfAntsEntered;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ant"))
        {
            other.GetComponentInChildren<SpriteRenderer>().DOFade(0, 1f).OnComplete(() => other.gameObject.SetActive(false));
            
            _numOfAntsEntered++;
            PlayerPrefs.SetInt("Candy Points", PlayerPrefs.GetInt("Candy Points") + 10);
            EventManager.TriggerAntEntersAnthillEvent();
            
            if (_numOfAntsEntered == PlayerPrefs.GetInt("Alive Ants"))
            {
                Debug.Log("Level Complete");
                EventManager.TriggerLevelCompleteEvent();
            }
        }
    }
}
