using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class MyTween : MonoBehaviour
{
    [SerializeField] private Transform pos1;
    [SerializeField] private Transform pos2;
    [Space] [SerializeField] private Transform target;
    [SerializeField] private float duration = 2f;

    [ContextMenu(nameof(StartTween))]
    public void StartTween()
    {
        StartCoroutine(Tween());
    }
    
    [ContextMenu(nameof(ToStart))]
    public void ToStart()
    {
        target.position = pos1.position;
        target.rotation = pos1.rotation;
        target.localScale = pos1.localScale;
    }

    private IEnumerator Tween()
    {
        float timer = duration;


        while (timer >= 0f)
        {
            float delta = timer / duration;
            float curve = 1 - Mathf.Pow(1 - delta, 3);
            
            target.position = Vector3.Lerp(pos2.position, pos1.position, curve);
            target.rotation = Quaternion.Lerp(pos2.rotation, pos1.rotation, curve);
            target.localScale = Vector3.Lerp(pos2.localScale, pos1.localScale, curve);
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        yield return null;
    }
}
