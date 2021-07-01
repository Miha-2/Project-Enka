using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TurnButton : MonoBehaviour
{
    private bool isMyTurn = true;
    private bool isFlipping = false;
    [SerializeField] private float duration = 1f;
    [SerializeField] private Ease easeType = Ease.Linear;
    [SerializeField] private Ease easeType2 = Ease.Linear;
    private Vector3 size;
    private void OnMouseDown() => TryChangeTurn();

    private void Start()
    {
        
        size = transform.localScale;
    }

    private void TryChangeTurn()
    {
        if (isFlipping) return;
        StartCoroutine(ChangeTurn());
    }

    private IEnumerator ChangeTurn()
    {
        isFlipping = true;


        float scale = isMyTurn ? 1f : .6667f;
        float scale2 = !isMyTurn ? 1f : .6667f;
        float rotation = isMyTurn ? 0f : 180f;
        float targetRotation  = rotation == 0f ? 180f : 0f;
        
        isMyTurn = !isMyTurn;

        var tweener = DOTween.To(() => rotation, x => rotation = x, targetRotation, duration)
            .SetEase(easeType)
            .OnUpdate(() => transform.eulerAngles = new Vector3(rotation, 0f, 0f));
        
        var tweener2 = DOTween.To(() => scale, x => scale = x, scale2, duration)
            .SetEase(easeType2)
            .OnUpdate(() => transform.localScale = size * scale);

        while (tweener.IsActive())
            yield return null;

        isFlipping = false;
    }
}
