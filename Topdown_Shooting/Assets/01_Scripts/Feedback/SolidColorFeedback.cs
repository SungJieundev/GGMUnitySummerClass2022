using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidColorFeedback : Feedback
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private float _flashTime = 0.1f;

    public override void CompletePrevFeedback()
    {
        StopAllCoroutines();
        _spriteRenderer.material.SetInt("MakeHit", 0);
    }

    public override void CreateFeedback()
    {
        if(_spriteRenderer.material.HasProperty("_MakeHit"))
        {
            _spriteRenderer.material.SetInt("_MakeHit", 1);
            StartCoroutine(WaitBeforeChangingBack());
        }
    }

    IEnumerator WaitBeforeChangingBack()
    {
        yield return new WaitForSeconds(_flashTime);
        _spriteRenderer.material.SetInt("_MakeHit", 0);
    }
}
