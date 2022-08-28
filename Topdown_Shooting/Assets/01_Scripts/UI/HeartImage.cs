using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartImage : MonoBehaviour
{
    private Image _image;
    [SerializeField] HeartImageSO _heartImageSO;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void SetHeartImage(bool value)
    {
        if(value == true)
        {
            _image.sprite = _heartImageSO.fullHeart;
        }else
        {
            _image.sprite = _heartImageSO.emptyHeart;
        }
    }

}
