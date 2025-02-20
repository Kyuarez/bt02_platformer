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
    }

    private void Start()
    {
        GameManager.STATE = GAME_STATE.PLAYING;
    }

    private void FixedUpdate()
    {
        //@tk : ������ �� ���� �����ϴ� ������ ���� ���� ������Ʈ�� �����ϴ����� �����ؼ� true �Ǵ� false�� ����
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);

        if(true == onGround || axisH != 0) //�¿� �̵�
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
        if(GameManager.STATE != GAME_STATE.PLAYING)
        {
            if(rigid.linearVelocity.magnitude > 0)
            {
                rigid.linearVelocity = Vector2.zero;
            }
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

    public void InitPlayerPos(Vector2 pos)
    {
        rigid.linearVelocity = new Vector2 (0,0);
        transform.position = pos;
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
        GameManager.STATE = GAME_STATE.GAMEOVER;
        GameStop();
        col.enabled = false;

        rigid.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);

    }

    private void OnGoal()
    {
        anim.Play(currentAnim = Enum.GetName(typeof(ANIME_STATE), 3));
        GameManager.STATE = GAME_STATE.GAMECLEAR;
        GameStop();
    }

    private void GameStop()
    {
        rigid.linearVelocity = new Vector2(0, 0);

    }

}
