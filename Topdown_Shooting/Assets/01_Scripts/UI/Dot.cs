using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Dot : MonoBehaviour
{      

    public TextMeshProUGUI text;
    
    private void Start() {
        StartCoroutine(Pop());
    }
    private IEnumerator Pop()
    {
        Sequence seq = DOTween.Sequence();

        while(true)
        {
            seq.Append(text.rectTransform.DOScale(new Vector3(0.95f, 0.95f), 3f));
            yield return new WaitForSeconds(3f);
            seq.Append(text.rectTransform.DOScale(new Vector3(1.05f, 1.05f), 3f));
            yield return new WaitForSeconds(3f);
        }
    }
}
