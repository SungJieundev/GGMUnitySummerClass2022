using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip _dieClip;
    public float jumpForce = 700f;

    private int jumpCount = 0;
    private bool isGrounded = false; //�÷��̾ ���� ����ִ����� �Ǵ��ϴ� ����
    private bool isDead = false;  //�÷��̾ �׾����� �Ǵ��ϴ� ����

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
            rigid.velocity = rigid.velocity * 0.5f; //������ �� ����� �������� ª�� ����� �� ���� ���ݾƿ� �� ��Ȳ���� �ӵ��� �ٿ��ܿ� ���� ���� ª�� ����� ���� ������ ���� �ǰ� �� ����� ���� �ǰ�
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
