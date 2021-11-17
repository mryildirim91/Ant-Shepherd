using DG.Tweening;
using UnityEngine;

public class Anthill : MonoBehaviour
{
    private int _numOfAntsEntered;

    private void Start()
    {
        InvokeRepeating(nameof(CheckRemainingAnts), 10, 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ant"))
        {
            SpriteRenderer spriteRenderer = other.GetComponentInChildren<SpriteRenderer>();
            if(spriteRenderer != null)
                spriteRenderer.DOFade(0, 1f).OnComplete(() => other.gameObject.SetActive(false));
            
            _numOfAntsEntered++;
            PlayerPrefs.SetInt("Candy Points", PlayerPrefs.GetInt("Candy Points") + 10);
            EventManager.TriggerAntEntersAnthillEvent();
        }
    }

    private void CheckRemainingAnts()
    {
        if(_numOfAntsEntered < 1) return;

        if (_numOfAntsEntered == PlayerPrefs.GetInt("Alive Ants"))
        {
            EventManager.TriggerLevelCompleteEvent();
        }
    }
}
