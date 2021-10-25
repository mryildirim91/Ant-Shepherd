using System;
using System.Collections;
using System.Collections.Generic;
using MyUtils;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]private float _xPlaceFactor, _yPlaceFactor;
    private float _offset;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        _offset = _spriteRenderer.size.x * 0.5f;
        transform.position = ScreenBoundaries.GetScreenBoundaries(_xPlaceFactor, _xPlaceFactor * _offset,
            _yPlaceFactor, _yPlaceFactor * _offset);
    }
}
