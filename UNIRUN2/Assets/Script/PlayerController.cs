using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip _dieClip;
    public float jumpForce = 700f;

    private int jumpCount = 0;
    private bool isGrounded = false; //플레이어가 땅에 닿아있는지를 판단하는 변수
    private bool isDead = false;  //플레이어가 죽었는지 판단하는 변수

    private AudioSource audioSource;
    private Rigidbody2D rigid;
    private Animator animator;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(isDead == true)
        {
            return;
        }
        if(Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            jumpCount++;
            rigid.velocity = Vector2.zero;
            rigid.AddForce(new Vector2(0, jumpForce));
            animator.SetTrigger("IsJump");
            audioSource.Play();
        }
        else if(Input.GetMouseButtonUp(0) && rigid.velocity.y > 0)
        {
            rigid.velocity = rigid.velocity * 0.5f; //점프를 꾹 누루고 있을수도 짧게 누루고 땔 수도 있잖아요 땐 상황에서 속도를 줄여줌에 따라서 만약 짧게 누루고 땐다 점프가 낮게 되게 꾹 누루면 높게 되게
        }
    }

    private void Die()
    {
        animator.SetTrigger("IsDie");
        audioSource.clip = _dieClip;
        audioSource.Play();

        rigid.velocity = Vector2.zero;
        isDead = true;
        GameManager.Instance.OnPlayerDead();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Dead" && isDead == false)
        {
            Die();
        }
    }
}
