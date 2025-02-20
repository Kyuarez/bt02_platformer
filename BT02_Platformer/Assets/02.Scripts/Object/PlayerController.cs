using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public enum ANIME_STATE
    {
        anim_player_idle,
        anim_player_run, 
        anim_player_jump,
        anim_player_clear,
        anim_player_gameover,
    }

    public static string state = "playing";

    float axisH = 0.0f;
    public float moveSpeed = 3.0f;
    public float jump = 8.0f;
    public LayerMask groundLayer;

    bool goJump = false;
    bool onGround = false;

    
    Rigidbody2D rigid;
    CapsuleCollider2D col;
    Animator anim;
    string currentAnim;
    string previousAnim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        state = "playing";
    }

    private void FixedUpdate()
    {
        //@tk : 지정한 두 점을 연결하는 가상의 선에 게임 오브젝트가 접촉하는지를 조사해서 true 또는 false로 리턴
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);

        if(true == onGround || axisH != 0) //좌우 이동
        {
            rigid.linearVelocity = new Vector2(moveSpeed * axisH, rigid.linearVelocityY);
        }
        if(true == onGround && true == goJump) //Jump
        {
            Vector2 jumpPw = new Vector2(0, jump);
            rigid.AddForce(jumpPw, ForceMode2D.Impulse);
            goJump = false; //one time
        }
    }

    private void Update()
    {
        if(state != "playing")
        {
            return;
        }


        axisH = Input.GetAxisRaw("Horizontal");

        if (axisH > 0.0f)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        if (true == Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        //anim
        if(true == onGround)
        {
            if(axisH == 0)
            {
                currentAnim = Enum.GetName(typeof(ANIME_STATE), 0);
            }
            else
            {
                currentAnim = Enum.GetName(typeof(ANIME_STATE), 1);
            }
        }
        else
        {
            currentAnim = Enum.GetName(typeof(ANIME_STATE), 2);
        }

        if(currentAnim != previousAnim)
        {
            previousAnim = currentAnim;
            anim.Play(currentAnim);
        }

    }

    private void Jump()
    {
        goJump = true;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Goal")
        {
            OnGoal();
        }
        else if(collision.gameObject.tag == "Dead")
        {
            OnGameOver();
        }
    }

    private void OnGameOver()
    {
        anim.Play(currentAnim = Enum.GetName(typeof(ANIME_STATE), 4));
        state = "gameover";
        GameStop();
        col.enabled = false;

        rigid.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);

    }

    private void OnGoal()
    {
        anim.Play(currentAnim = Enum.GetName(typeof(ANIME_STATE), 3));
        state = "gameclear";
        GameStop();
    }

    private void GameStop()
    {
        rigid.linearVelocity = new Vector2(0, 0);

    }

}
