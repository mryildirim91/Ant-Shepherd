using System;
using System.Collections;
using DG.Tweening;
using MyUtils;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Candy : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private int _candyPoints;
    [SerializeField] private Text _candyPointsText;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(AnimateCandy());
        _candyPoints = PlayerPrefs.GetInt("Candy Points");
        UpdateCandyPointsText();
    }

    private void OnEnable()
    {
        EventManager.ONAntEntersAnthillEvent += UpdateCandyPointsText;
    }

    private void OnDisable()
    {
        EventManager.ONAntEntersAnthillEvent -= UpdateCandyPointsText;
    }

    private IEnumerator AnimateCandy()
    {
        while (true)
        {
            transform.DOScale(0.7f, 1);
            _spriteRenderer.DOFade(0.5f, 1);
            yield return BetterWaitForSeconds.Wait(1.2f);
            transform.DOScale(1, 1);
            _spriteRenderer.DOFade(1, 1);
            yield return BetterWaitForSeconds.Wait(1.2f);
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
            _candyPoints += Random.Range(5,18);
            PlayerPrefs.SetInt("Candy Points",_candyPoints);
            UpdateCandyPointsText();
        }
    }

    private void UpdateCandyPointsText()
    {
        _candyPointsText.text = PlayerPrefs.GetInt("Candy Points").ToString();
    }
}
