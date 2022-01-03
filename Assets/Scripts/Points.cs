using DG.Tweening;
using MyUtils;
using TMPro;
using UnityEngine;

public class Points : MonoBehaviour
{
    private RectTransform _rectTransform;
    private TextMeshPro _textMeshPro;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _textMeshPro = GetComponent<TextMeshPro>();
    }

    private void OnEnable()
    {
        _textMeshPro.DOFade(1, 0);
        _rectTransform.DOAnchorPos(Vector2.up * 100, 100);
        _rectTransform.DOAnchorPos(Vector2.up * 100, 100);
        _textMeshPro.DOFade(0, 2).OnComplete(() => ObjectPool.Instance.ReturnGameObject(gameObject));
    }
}
