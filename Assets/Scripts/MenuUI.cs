using System.Collections;
using DG.Tweening;
using MyUtils;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    private int _candyPoints, _price;
    [SerializeField] private GameObject _gameOverPanel, _nextPanel;
    [SerializeField] private Text _startButtonText;
    [SerializeField] private Text _candyPointsText, _priceText;
    [SerializeField] private Button _moreAntButton;
    [SerializeField] private Image[] _stars;

    private void Start()
    {
        StartCoroutine(FadeInAndOut());
        _candyPoints = PlayerPrefs.GetInt("Candy Points");
        UpdateCandyPointsText();

        _price = !PlayerPrefs.HasKey("Price") ? 100 : PlayerPrefs.GetInt("Price");
        _priceText.text = "-" + _price;
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
        if (_candyPoints < _price && _moreAntButton.interactable)
        {
            _moreAntButton.interactable = false;
        }
        else if (_candyPoints >= _price && !_moreAntButton.interactable)
        {
            _moreAntButton.interactable = true;
        }
    }

    public void SpendCandyPoints(Text candyPointsText)
    {
        _candyPoints -= _price;
        _price += 100;
        candyPointsText.text = _candyPoints.ToString();
        PlayerPrefs.SetInt("Candy Points", _candyPoints);
        PlayerPrefs.SetInt("Price",_price);
        _priceText.text = "-" + _price;
    }
    
    private void OpenStars()
    {
        float ratio = (float)PlayerPrefs.GetInt("Alive Ants") / PlayerPrefs.GetInt("Total Ants");
        
        if (ratio < 0.4f)
        {
            StartCoroutine(DelayOpenStars(2));
        }
        else if (ratio >= 0.4f && ratio < 0.8f)
        {
            StartCoroutine(DelayOpenStars(1));
        }
        else
        {
            StartCoroutine(DelayOpenStars(0));
        }
        
        PlayerPrefs.SetInt("Total Ants", PlayerPrefs.GetInt("Alive Ants"));
    }
    
    private IEnumerator DelayOpenStars(int indexReducer)
    {
        for (int i = 0; i < _stars.Length - indexReducer; i++)
        {
            yield return BetterWaitForSeconds.Wait(0.2f);
            _stars[i].gameObject.SetActive(true);
        }
    }

    private void OpenNextPanel()
    {
        Invoke(nameof(DelayNextPanel),2);
    }

    private void DelayNextPanel()
    {
        _nextPanel.SetActive(true);
        OpenStars();
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
    }

    private void OpenGameOverPanel()
    {
        Invoke(nameof(DelayGameOverPanel),2);
    }

    private void DelayGameOverPanel()
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
