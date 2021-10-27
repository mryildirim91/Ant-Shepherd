using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private ObstacleType _obstacleType;
    
    public ObstacleType ObstacleType => _obstacleType;
}

public enum ObstacleType
{
    Normal,
    Deadly
}
