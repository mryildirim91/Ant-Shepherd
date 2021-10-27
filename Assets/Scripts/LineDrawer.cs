
using System;
using System.Collections.Generic;
using DG.Tweening;
using MyUtils;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [SerializeField] private float _lineDensity;
    [SerializeField] private GameObject _line, _pencil;
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layers;

    private int _numOFLines = 4;
    private bool _stopDrawingLine;
    private Vector2 _startPos;
    private GameObject _currentLine;
    private LineRenderer _lineRenderer;
    private EdgeCollider2D _edgeCollider2D;
    private readonly List<Vector2> _touchPositions = new List<Vector2>();

    private void OnEnable()
    {
        EventManager.ONLineGoneEvent += IncreaseNumberOfLines;
    }

    private void OnDisable()
    {
        EventManager.ONLineGoneEvent -= IncreaseNumberOfLines;
    }

    private void Update()
    {
        if (!GameManager.Instance.StartGame || _numOFLines < 1)
            return;
        
        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 endPos = _camera.ScreenToWorldPoint(Input.mousePosition);

            if (Physics2D.Raycast(endPos, Vector3.back, Mathf.Infinity, _layers) || _touchPositions.Count > 10)
                _stopDrawingLine = true;

            if (Vector2.Distance(endPos, _touchPositions[_touchPositions.Count - 1]) > _lineDensity && !_stopDrawingLine)
            { 
                _pencil.gameObject.transform.DOMove(endPos, 0);
                UpdateLine(endPos);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_numOFLines > 0)
                _numOFLines--;
            
            _stopDrawingLine = false;
            _pencil.SetActive(false);
            
            if(_currentLine != null) 
                _currentLine.GetComponent<Line>().StartDestroying();
        }
    }

    private void CreateLine()
    {
        _pencil.SetActive(true);
        _startPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        _pencil.transform.position = _startPos;
        _currentLine = ObjectPool.Instance.GetObject(_line);
        _lineRenderer = _currentLine.GetComponent<LineRenderer>();
        _edgeCollider2D = _currentLine.GetComponent<EdgeCollider2D>();


        if(_touchPositions.Count > 0)
            _touchPositions.Clear();
        
        _touchPositions.Add(_startPos);
        _touchPositions.Add(_startPos);
        _lineRenderer.SetPosition(0, _touchPositions[0]);
        _lineRenderer.SetPosition(1, _touchPositions[1]);
        _edgeCollider2D.points = _touchPositions.ToArray();
    }

    private void UpdateLine(Vector2 touchPos)
    {
        _touchPositions.Add(touchPos);
        var positionCount = _lineRenderer.positionCount;
        positionCount++;
        _lineRenderer.positionCount = positionCount;
        _lineRenderer.SetPosition(positionCount - 1, touchPos);
        _edgeCollider2D.points = _touchPositions.ToArray();
    }

    private void IncreaseNumberOfLines()
    {
        if (_numOFLines < 3)
        {
            _numOFLines++;
        }
    }
}
