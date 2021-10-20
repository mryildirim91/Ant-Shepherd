
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [SerializeField] private float _lineDensity;
    [SerializeField] private GameObject _line, _pencil;
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layers;

    private bool _stopDrawingLine;
    private GameObject _currentLine;
    private LineRenderer _lineRenderer;
    private EdgeCollider2D _edgeCollider2D;
    private List<Vector2> _touchPositions = new List<Vector2>();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 positions = _camera.ScreenToWorldPoint(Input.mousePosition);

            if (Physics2D.Raycast(positions, Vector3.back, Mathf.Infinity, _layers))
                _stopDrawingLine = true;

            if (Vector2.Distance(positions, _touchPositions[_touchPositions.Count - 1]) > _lineDensity && !_stopDrawingLine)
            {
                _pencil.gameObject.transform.DOMove(positions, 0);
                UpdateLine(positions);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _stopDrawingLine = false;
            _pencil.SetActive(false);
        }
    }

    private void CreateLine()
    {
        _pencil.SetActive(true);
        Vector2 startPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        _pencil.transform.position = startPos;
        _currentLine = Instantiate(_line, Vector2.zero, Quaternion.identity);
        _lineRenderer = _currentLine.GetComponent<LineRenderer>();
        _edgeCollider2D = _currentLine.GetComponent<EdgeCollider2D>();
        
        if(_touchPositions.Count > 0)
            _touchPositions.Clear();
        
        _touchPositions.Add(startPos);
        _touchPositions.Add(startPos);
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
}
