using System.Collections;
using MyUtils;
using UnityEngine;
using UnityEngine.UI;

public class AntSpawner : MonoBehaviour
{
    private int _numOfAnts, _numOfAliveAnts;
    [SerializeField] private GameObject _ant;
    [SerializeField] private Text _numOfAliveAntsText;
    
    void Start()
    {
        if (PlayerPrefs.HasKey("Total Ants"))
        {
            _numOfAnts = PlayerPrefs.GetInt("Total Ants"); 
        }
        else
        {
            _numOfAnts = 10;
            PlayerPrefs.SetInt("Total Ants", _numOfAnts);
        }
        
        StartCoroutine(SpawnAnt());
        _numOfAliveAnts = _numOfAnts;
        _numOfAliveAntsText.text = _numOfAliveAnts.ToString();
        PlayerPrefs.SetInt("Alive Ants", _numOfAliveAnts);
    }

    private void OnEnable()
    {
        EventManager.ONAntDeathEvent += CountAliveEnemies;
    }

    private void OnDisable()
    {
        EventManager.ONAntDeathEvent -= CountAliveEnemies;
    }

    private IEnumerator SpawnAnt()
    {
        while (_numOfAnts > 0)
        {
            yield return null;
            
            if (GameManager.Instance.StartGame)
            {
                GameObject ant = Instantiate(_ant);
                ant.transform.position = ScreenBoundaries.GetScreenBoundaries(0, 0, -1, -1);
                _numOfAnts--;
                yield return BetterWaitForSeconds.Wait(1f);
            }
        }
    }

    private void CountAliveEnemies()
    {
        _numOfAliveAnts--;
        _numOfAliveAntsText.text = _numOfAliveAnts.ToString();
        PlayerPrefs.SetInt("Alive Ants", _numOfAliveAnts);

        if (_numOfAliveAnts < 1)
        {
            Debug.Log("Game Over");
            EventManager.TriggerGameOverEvent();
        }
    }

    public void IncreaseAntNumber()
    {
        _numOfAnts++;
        _numOfAliveAnts++;
        _numOfAliveAntsText.text = _numOfAliveAnts.ToString();
        PlayerPrefs.SetInt("Alive Ants", _numOfAliveAnts);
    }
}
