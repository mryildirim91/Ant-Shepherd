using System;
using System.Collections;
using DG.Tweening;
using MyUtils;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MenuUI : MonoBehaviour
{
    private int _candyPoints;
    [SerializeField] private GameObject _gameOverPanel, _nextPanel;
    [SerializeField] private Text _startButtonText;
    [SerializeField] private Text _candyPointsText;
    [SerializeField] private Button _moreAntButton;
    [SerializeField] private Image[] _stars;

    private void Start()
    {
        StartCoroutine(FadeInAndOut());
        _candyPoints = PlayerPrefs.GetInt("Candy Points");
        UpdateCandyPointsText();
    }

    private void OnEnable()
    {
        EventManager.ONLevelCompleteEvent += OpenNextPanel;
        EventManager.ONGameOverEvent += OpenGameOverPanel;
        EventManager.ONAntEntersAnthillEvent += UpdateCandyPointsText;
        EventManager.ONAntCollectsCandy += UpdateCandyPoints;
    }

    private void OnDisable()
    {
        EventManager.ONLevelCompleteEvent -= OpenNextPanel;
        EventManager.ONGameOverEvent -= OpenGameOverPanel;
        EventManager.ONAntEntersAnthillEvent -= UpdateCandyPointsText;
        EventManager.ONAntCollectsCandy -= UpdateCandyPoints;
    }

    private IEnumerator FadeInAndOut()
    {
        while (_startButtonText.gameObject.activeSelf)
        {
            _startButtonText.DOFade(0, 0.5f);
            yield return BetterWaitForSeconds.Wait(0.6f);
            _startButtonText.DOFade(1, 0.5f);
            yield return BetterWaitForSeconds.Wait(0.6f);
        }
    }

    private void Update()
    {
        if (_candyPoints < 100 && _moreAntButton.interactable)
        {
            _moreAntButton.interactable = false;
        }
        else if (_candyPoints >= 100 && !_moreAntButton.interactable)
        {
            _moreAntButton.interactable = true;
        }
    }

    public void SpendCandyPoints(Text candyPointsText)
    {
        _candyPoints -= 100;
        candyPointsText.text = _candyPoints.ToString();
        PlayerPrefs.SetInt("Candy Points", _candyPoints);
    }

    private IEnumerator OpenStars()
    {
        foreach (var t in _stars)
        {
            yield return BetterWaitForSeconds.Wait(0.2f);
            t.gameObject.SetActive(true);
        }
    }

    private void OpenNextPanel()
    {
        _nextPanel.SetActive(true);
        StartCoroutine(OpenStars());
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
    }

    private void OpenGameOverPanel()
    {
        _gameOverPanel.SetActive(true);
    }

    private void UpdateCandyPoints(int points)
    {
        _candyPoints += points;
        PlayerPrefs.SetInt("Candy Points", _candyPoints);
        UpdateCandyPointsText();
    }

    private void UpdateCandyPointsText()
    {
        _candyPointsText.text = PlayerPrefs.GetInt("Candy Points").ToString();
    }
}
