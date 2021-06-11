using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimateText : MonoBehaviour
{
    
    public Vector3 initialSize = new Vector3(0.0025f, 0.0025f, 0.0025f);
    public float sizeMultiplier = 1.5f;
    public float duration = 0.75f;
    

    private void Awake()
    {
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        transform.DOScale(initialSize, duration);
        yield return new WaitForSeconds(1);
        transform.DOScale(initialSize / sizeMultiplier, duration);
        yield return new WaitForSeconds(1);
        transform.DOScale(initialSize, duration);
        yield return new WaitForSeconds(1);
        transform.DOScale(initialSize / sizeMultiplier, duration);
        yield return new WaitForSeconds(1);
        transform.DOScale(initialSize, duration);
        yield return new WaitForSeconds(1);
        transform.localScale = Vector3.zero;
        Destroy(gameObject);
    }
}
