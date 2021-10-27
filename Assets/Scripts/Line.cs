using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MyUtils;
using UnityEngine;

public class Line : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private EdgeCollider2D _edgeCollider2D;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _edgeCollider2D = GetComponent<EdgeCollider2D>();
    }

    private void OnDisable()
    {
        _lineRenderer.material.DOFade(1, 0);
        if (_lineRenderer.positionCount > 2)
            _lineRenderer.positionCount = 2;
        
        if (_edgeCollider2D.pointCount > 0)
            _edgeCollider2D.Reset();
    }

    public void StartDestroying()
    {
        Invoke(nameof(DelayDestroy), 10);
    }

    private void DelayDestroy()
    {
        _lineRenderer.material.DOFade(0, 1).OnComplete(() =>
        {
            EventManager.TriggerLineGoneEvent();
            ObjectPool.Instance.ReturnGameObject(gameObject);
        });
    }
}
