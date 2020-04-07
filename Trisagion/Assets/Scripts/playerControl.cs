using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerControl : MonoBehaviour
{
    private Rigidbody2D Rb2D;
    private Animator anim;

    public Transform groundCheck;
    public LayerMask ground;
    public Text scoresText;
    public Text hpText;

    int  score = 0;
    float speed = 8;
    float jumpForce = 12;
    int playerHp = 3;
    bool isGround,isFall;
    bool jumpPressed;
    int jumpCount = 2;
    bool isHurt = false;

    // Start is called before the first frame update
    void Start()
    {
        Rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        scoresText.text = "Score:0" ;
        hpText.text = "Hp:"+playerHp.ToString();
    }

    //使动作更加平滑
    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);

        if (!isHurt)
        {
            PlayerMove();
            PlayerJump();
        }
        SwitchAnim();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }
    }

    //角色移动
    void PlayerMove()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        Rb2D.velocity = new Vector2(horizontalMove * speed, Rb2D.velocity.y);
        if (horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }
    }

    //角色跳跃
    void PlayerJump()
    {
        if(isGround)
        {
            jumpCount = 2;
            isGround =true;
            isFall = true;
        }
        if(jumpPressed && isGround)
        {
            isGround = false;
            Rb2D.velocity = new Vector2(Rb2D.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
            isFall = false;
        }
        else if(jumpPressed && jumpCount>0 && !isGround)
        {
            Rb2D.velocity = new Vector2(Rb2D.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
            isFall = false;
        }
        else if(!jumpPressed && !isGround && isFall)
        {
            jumpCount--;
            isFall = false;
        }
    }

    //切换动画状态
    void SwitchAnim()
    {
        if (isHurt)
        {
            anim.SetBool("hurting", true);
            return;
        }

        anim.SetFloat("running", Mathf.Abs(Rb2D.velocity.x));

        if (isGround)
        {
            anim.SetBool("falling", false);
        }
        else if(!isGround && Rb2D.velocity.y > 0)
        {
            anim.SetBool("jumpping", true);
        }
        else if(Rb2D.velocity.y < 0)
        {
            anim.SetBool("jumpping", false);
            anim.SetBool("falling", true);
        }
    }

    //动画：从受伤状态恢复
    void RecFromDamage()
    {
        anim.SetInteger("playerHp", playerHp);
        if (playerHp > 0) 
        {
            anim.SetBool("hurting", false);
            isHurt = false;
        }
    }
    //动画：消失后销毁
    void DestoryThis()
    {
        Destroy(gameObject);
    }

    //触发器，吃到食物的时候
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isHurt) { return; }
        switch (collision.tag)
        {
            case "Enemy":
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.Hurt();
                Rb2D.velocity = new Vector2(Rb2D.velocity.x, jumpForce-2);
                Debug.Log(collision.gameObject);
                break;
            case "Collections2":
                collision.GetComponent<BoxCollider2D>().enabled = false;
                collision.SendMessage("SwitchAnim");
                score += 500;
                scoresText.text = "Score:" + score.ToString();
                break;
            case "Collections1":
                collision.GetComponent<BoxCollider2D>().enabled = false;
                collision.SendMessage("SwitchAnim");
                score += 200;
                scoresText.text = "Score:" + score.ToString();
                break;
            case "Collections":
                collision.GetComponent<BoxCollider2D>().enabled = false;
                collision.SendMessage("SwitchAnim");
                score += 100;
                scoresText.text = "Score:" + score.ToString();
                break;
            case "Traps":
                isHurt = true;
                playerHp--;
                hpText.text = "Hp:" + playerHp.ToString();
                if (transform.position.x < collision.gameObject.transform.position.x)
                {
                    Rb2D.velocity = new Vector2(-5, Rb2D.velocity.y);
                }
                else if (transform.position.x > collision.gameObject.transform.position.x)
                {
                    Rb2D.velocity = new Vector2(5, Rb2D.velocity.y);
                }
                if (transform.position.y < collision.gameObject.transform.position.y)
                {
                    Rb2D.velocity = new Vector2(Rb2D.velocity.x, -5);
                }
                else if (transform.position.y > collision.gameObject.transform.position.y)
                {
                    Rb2D.velocity = new Vector2(Rb2D.velocity.x, 5);
                }
                break;
            default:
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            isHurt = true;
            playerHp--;
            hpText.text = "Hp:" + playerHp.ToString();
            if (transform.position.x < collision.gameObject.transform.position.x)
            {
                Rb2D.velocity = new Vector2(-5, Rb2D.velocity.y);
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                Rb2D.velocity = new Vector2(5, Rb2D.velocity.y);
            }
            if (transform.position.y < collision.gameObject.transform.position.y)
            {
                Rb2D.velocity = new Vector2(Rb2D.velocity.x, -5);
            }
            else if (transform.position.y > collision.gameObject.transform.position.y)
            {
                Rb2D.velocity = new Vector2(Rb2D.velocity.x, 5);
            }
        }
    }
}
