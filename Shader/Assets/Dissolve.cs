using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{   
    [SerializeField]
    private bool _isDissolve;
    private float fade = 1;
    Material mat;

    private void Awake() {
        mat = GetComponent<SpriteRenderer>().material;
    }

    private void Update() {
        if(_isDissolve){
            fade -= Time.unscaledDeltaTime;

            if(fade <= 0){
                fade = 0;

                _isDissolve = false;
            }
            mat.SetFloat = 
        }
    }
}
