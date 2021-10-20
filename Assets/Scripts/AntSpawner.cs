using System.Collections;
using System.Collections.Generic;
using MyUtils;
using UnityEngine;

public class AntSpawner : MonoBehaviour
{
    private int _numOfAnts = 10;
    [SerializeField] private GameObject _ant;
    
    void Start()
    {
        StartCoroutine(SpawnAnt());
    }

    private IEnumerator SpawnAnt()
    {
        while (_numOfAnts > 0)
        {
            GameObject ant = Instantiate(_ant);
            ant.transform.position = ScreenBoundaries.GetScreenBoundaries(0, 0, -1, -1);
            _numOfAnts--;
            yield return BetterWaitForSeconds.Wait(1f);
        }
    }
}
