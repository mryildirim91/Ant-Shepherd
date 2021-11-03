
using System;
using System.Collections;
using DG.Tweening;
using MyUtils;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Candy : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private GameObject _pointsText;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        
        StartCoroutine(AnimateCandy());
    }
    
    private IEnumerator AnimateCandy()
    {
        while (true)
        {
            if (_spriteRenderer != null)
            {
                transform.DOScale(0.7f, 1);
                _spriteRenderer.DOFade(0.5f, 1);
                yield return BetterWaitForSeconds.Wait(1.2f);
                transform.DOScale(1, 1);
                _spriteRenderer.DOFade(1, 1);
                yield return BetterWaitForSeconds.Wait(1.2f);
            }
        }
    }

    private void Update()
    {
        transform.RotateAround(transform.position, Vector3.forward, 60*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ant"))
        {
            int points = Random.Range(5, 18);
            EventManager.TriggerAntCollectsCandy(points);
            GameObject obj = Instantiate(_pointsText);

            RectTransform rect = obj.GetComponent<RectTransform>();
            rect.anchoredPosition = transform.position + Vector3.up;
            TextMeshPro textMeshPro = obj.GetComponent<TextMeshPro>();
            textMeshPro.text = "+" + points;
        }
    }
}
