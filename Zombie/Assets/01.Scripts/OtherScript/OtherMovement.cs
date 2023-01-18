using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherMovement : MonoBehaviour
{   
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 180;

    private Rigidbody rigidBody = null;
    private Animator animator = null;

    private int moveHash = Animator.StringToHash("Move");

    private void Awake() {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void SetPosition(Vector3 pos){
        transform.position = pos;
    }

    public void SetRotate(float angle){
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

}
