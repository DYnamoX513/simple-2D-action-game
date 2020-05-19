using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerDashControl : MonoBehaviour
{
    private Rigidbody2D Rb2D;
    private Animator anim;

    public Transform groundCheck;
    public LayerMask ground;
    public Transform bottom;
    [Space]
    [Header("DashPara")]
    public float dashTime = 0.05f;//dash时长
    float horizontalMove;
    float dashTimeLeft;
    public float dashSpeed = 15.0f;

    [Space]
    [Header("Particle")]
    public ParticleSystem runEffect;
    public GameObject jumpEffect;

    int  score = 0;
    float speed = 8.0f;
    float jumpForce = 12;
    bool isGround,isFall,isDashing;
    bool jumpPressed;
    int jumpCount = 2;
    bool isHurt = false;
    bool emitJE = true;
    float lastUseMp, nextTime = 0;

    public Text scoresText;
    public Text timeText;
    public Image hpSlider;
    public Image mpSlider;
    private float CountDownTime = 300f;
    private float GameTime;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        Rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        scoresText.text = "Score:0" ;
        hpSlider.fillAmount = (float)GameManager.playerHp / GameManager.playerHpMax;
        mpSlider.fillAmount = GameManager.playerMp / GameManager.playerMpMax;
        GameTime = CountDownTime;
        if (GameManager.personChoose == 2)
        {
            gameObject.SetActive(true);
        }
        else { gameObject.SetActive(false); }
    }

    //使动作更加平滑
    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);

        if (!isHurt)
        {
            Dash();
            if (isDashing)
            {
                return;
            }
            PlayerMove();
            PlayerJump();
        }
        SwitchAnim();
    }


    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < bottom.position.y)
        {
            DestoryThis();
        }

        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            SoundManager.instance.JumpAudio();
            jumpPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.Z) && Rb2D.velocity.x != 0 && GameManager.playerMp >= 2)
        {
            GameManager.playerMp -= 2;
            mpSlider.fillAmount = GameManager.playerMp / GameManager.playerMpMax;
            SoundManager.instance.DashAudio();
            lastUseMp = Time.time;
            dashTimeLeft = dashTime;
            isDashing = true;
        }

        if (GameManager.playerHp != 0)//做下开始游戏的判断
        {
            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                timer = 0;
                GameTime--;
                int M = (int)(GameTime / 60);
                float S = GameTime % 60;
                timeText.text = M + ":" + string.Format("{0:00}", S);
                if ((M == 0) && (S <= 0))
                {
                    GameManager.PlayerDied(); //直接结束游戏
                }
            }
        }

        MpChange();
    }

    //角色移动
    void PlayerMove()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
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
        if(Rb2D.velocity.x !=0 && Rb2D.velocity.y == 0)
        {
            runEffect.Play();
        }else if(Mathf.Abs(Rb2D.velocity.x)<0.1f || Rb2D.velocity.y!= 0)
        {
            runEffect.Stop();
        }

        if (isGround)
        {
            anim.SetBool("falling", false);
            anim.SetBool("jumpping", false);
        }
        else if(!isGround && Rb2D.velocity.y > 0)
        {
            anim.SetBool("jumpping", true);
            if(jumpCount == 0)
            {
                anim.SetBool("jumpping2", true);
                if (emitJE)
                {
                    GameObject dust = Instantiate(jumpEffect, groundCheck.transform.position, Quaternion.identity);
                    Destroy(dust, 0.5f);
                    emitJE = false;
                }
            }
        }
        else if(Rb2D.velocity.y < 0)
        {
            anim.SetBool("jumpping", false);
            anim.SetBool("jumpping2", false);
            anim.SetBool("falling", true);
            emitJE = true;
        }
    }

    //动画：从受伤状态恢复
    void RecFromDamage()
    {
        anim.SetInteger("playerHp", GameManager.playerHp);
        if (GameManager.playerHp > 0) 
        {
            anim.SetBool("hurting", false);
            isHurt = false;
        }
    }
    //动画：消失后销毁
    void DestoryThis()
    {
        SoundManager.instance.DeathAudio();
        GameManager.PlayerDied();
        Destroy(gameObject);
    }

    //触发器，吃到食物的时候
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isHurt) { return; }
        switch (collision.tag)
        {
            case "endPoint":
                GameManager.score = score;
                GameManager.time = GameTime;
                GameManager.PlayerWin();
                break;
            case "Enemy":
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.Hurt();
                Rb2D.velocity = new Vector2(Rb2D.velocity.x, jumpForce-2);
                Debug.Log(collision.gameObject);
                break;
            case "Collections7":
                collision.GetComponent<BoxCollider2D>().enabled = false;
                collision.SendMessage("SwitchAnim");
                SoundManager.instance.EatAudio();
                score += 10000;
                scoresText.text = "Score:" + score.ToString();
                break;
            case "Collections6":
                collision.GetComponent<BoxCollider2D>().enabled = false;
                collision.SendMessage("SwitchAnim");
                SoundManager.instance.EatAudio();
                score += 1200;
                scoresText.text = "Score:" + score.ToString();
                break;
            case "Collections5":
                collision.GetComponent<BoxCollider2D>().enabled = false;
                collision.SendMessage("SwitchAnim");
                SoundManager.instance.EatAudio();
                score += 1000;
                scoresText.text = "Score:" + score.ToString();
                break;
            case "Collections4":
                collision.GetComponent<BoxCollider2D>().enabled = false;
                collision.SendMessage("SwitchAnim");
                SoundManager.instance.EatAudio();
                score += 800;
                scoresText.text = "Score:" + score.ToString();
                break;
            case "Collections3":
                collision.GetComponent<BoxCollider2D>().enabled = false;
                collision.SendMessage("SwitchAnim");
                SoundManager.instance.EatAudio();
                score += 500;
                scoresText.text = "Score:" + score.ToString();
                break;
            case "Collections2":
                collision.GetComponent<BoxCollider2D>().enabled = false;
                collision.SendMessage("SwitchAnim");
                SoundManager.instance.EatAudio();
                score += 400;
                scoresText.text = "Score:" + score.ToString();
                break;
            case "Collections1":
                collision.GetComponent<BoxCollider2D>().enabled = false;
                collision.SendMessage("SwitchAnim");
                SoundManager.instance.EatAudio();
                score += 250;
                scoresText.text = "Score:" + score.ToString();
                break;
            case "Collections":
                collision.GetComponent<BoxCollider2D>().enabled = false;
                collision.SendMessage("SwitchAnim");
                SoundManager.instance.EatAudio();
                score += 100;
                scoresText.text = "Score:" + score.ToString();
                break;
            case "Spikes":
                isHurt = true;
                dashTimeLeft = 0;
                isDashing = false;
                Rb2D.velocity = new Vector2(0, Rb2D.velocity.y);
                GameManager.playerHp--;
                SoundManager.instance.HurtAudio();
                hpSlider.fillAmount = (float)GameManager.playerHp / GameManager.playerHpMax;
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
            case "Trampoline":
                Rb2D.velocity = new Vector2(Rb2D.velocity.x, 22);
                SoundManager.instance.JumpAudio();
                collision.SendMessage("SwitchAnim");
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
            dashTimeLeft = 0;
            isDashing = false;
            Rb2D.velocity = new Vector2(0, Rb2D.velocity.y);
            GameManager.playerHp--;
            SoundManager.instance.HurtAudio();
            hpSlider.fillAmount = (float)GameManager.playerHp / GameManager.playerHpMax;
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

    //角色特性技能
    void Dash()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                if(Rb2D.velocity.y >0 && !isGround)
                {
                    Rb2D.velocity = new Vector2(dashSpeed * horizontalMove, jumpForce);
                }
                Rb2D.velocity = new Vector2(dashSpeed * horizontalMove, Rb2D.velocity.y);
                
                dashTimeLeft -= Time.deltaTime;
                shadowPoolControl.instance.GetFromPool();
            }
            if(dashTimeLeft<= 0)
            {
                isDashing = false;
            }
        }
    }

    void MpChange()
    {
        if (Time.time > nextTime)
        {
            if (Time.time - lastUseMp > 2.0f && GameManager.playerMp < GameManager.playerMpMax)
            {
                GameManager.playerMp += 0.1f;
                if (GameManager.playerMp > GameManager.playerMpMax) GameManager.playerMp = GameManager.playerMpMax;
            }
            nextTime = Time.time + 0.1f;
        }
        mpSlider.fillAmount = GameManager.playerMp / GameManager.playerMpMax;
    }
}
