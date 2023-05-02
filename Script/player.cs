using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    //血量相關
   [SerializeField] float maxhealth = 100f;
   [SerializeField] float health = 100f;
   [SerializeField] float HitCDtime = 2f;
   public shoot shoot;
   private bool HitCD = false;
   
   [SerializeField] float MoveSpeed = 5f;
   [SerializeField] float Direction = 0f;
   [SerializeField] float Vertical = 0f;
   [SerializeField] float JumpSpeed = 10f;
   [SerializeField] float MaxJumpTime = 1;
   [SerializeField] Rigidbody2D Player;
   private float JumpTime = 1;
   private float Face = 0;
   //落地偵測
   [SerializeField] Transform groundCheck;
   [SerializeField] float groundCheckRadius;
   [SerializeField] LayerMask groundLayer;
   private bool isTouchingGround;
   //滑牆
   [SerializeField] Transform wallCheckR;
   [SerializeField] Transform wallCheckL;
   [SerializeField] float wallCheckRadius;
   [SerializeField] LayerMask wallLayer;
   [SerializeField] float wallslidespeed = 20f;
   private bool isTouchingwallL;
   private bool isTouchingwallR;
   private bool onWall;
   //跳牆

   private float walljumpDirection;
   private float walljumpingTime = 0.1f;
   private float walljumpingCount;
   private Vector2 walljumpingPower = new Vector2(0f,12f);
   //衝刺
   private bool CanDash = true;
   private bool CanDrop = true;
   private bool Isdashing;
   private float DashPower = 24f;
   private float Dashtime = 0.2f;
   private float DashCD = 0.5f;
   [SerializeField] private TrailRenderer tr;

    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {  
        Dead();
        //跳躍地板檢查
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius,groundLayer);
        //跳牆動作檢查
        isTouchingwallR = Physics2D.OverlapCircle(wallCheckR.position,wallCheckRadius,wallLayer);
        isTouchingwallL = Physics2D.OverlapCircle(wallCheckL.position,wallCheckRadius,wallLayer);    
        //檢查面對方向
        if(Direction  > 0)
        {
            Face = 1;
        }
        else if (Direction < 0)
        {
            Face = -1;
        }
        //衝刺時取消控制
        if(Isdashing)
        {
            return;
        }
        Move();
        Jump();
        Dash();
        WallSlide();
        WallJump(); 
        fastfall();
    }
    //左右移動
    private void Move()
    {
        Direction = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        if(Direction != 0f )
        {
            Player.velocity = new Vector2(Direction*MoveSpeed,Player.velocity.y);
        }
    }
    //跳
    private void Jump()
    {
        //按鍵偵測+有觸碰地板=跳
        if(Input.GetButtonDown("Jump")&& JumpTime>0||Input.GetButtonDown("Jump")&&isTouchingGround==true) 
        {
            Player.velocity = new Vector2(Player.velocity.x,JumpSpeed);
            JumpTime = JumpTime-1;
            if(isTouchingGround)
            {
                JumpTime = MaxJumpTime;
            }   
        }
    }
    //衝刺狀態
    private IEnumerator Dashing()
    {
        CanDash = false;
        Isdashing = true;
        float originalG = Player.gravityScale;
        Player.gravityScale = 0f;
        Player.velocity = new Vector2(Face*DashPower,0f);
        tr.emitting = true;
        Physics2D.IgnoreLayerCollision(8,9,true);
        yield return new WaitForSeconds(Dashtime);
        tr.emitting = false;
        Physics2D.IgnoreLayerCollision(8,9,false);
        Player.gravityScale = originalG;
        Isdashing = false;
        yield return new WaitForSeconds(DashCD);
        CanDash = true;
    }
    //衝刺
    private void Dash()
    {
        if(Input.GetButtonDown("Dash")&&CanDash)
        {
            StartCoroutine(Dashing());
        }
    }
    //滑牆狀態
    private void WallSlide()
    {
        if(!isTouchingGround && isTouchingwallL && Direction != 0f || !isTouchingGround && isTouchingwallR && Direction != 0f)
        {
            onWall = true;
            Player.velocity = new Vector2(Player.velocity.x,Mathf.Clamp(Player.velocity.y,-wallslidespeed,float.MaxValue));
            JumpTime = MaxJumpTime;

        }
        else
        {
            onWall = false;
        }
    }    
    
    //蹬牆跳狀態
    private void WallJump()
    {
        if(onWall)
        {
            walljumpDirection = -transform.localScale.x;
            walljumpingCount = walljumpingTime;


        }
        else
        {
            walljumpingCount -= Time.deltaTime;
        }
        if(Input.GetButtonDown("Jump")&&walljumpingCount>0f)
        {
            Player.velocity = new Vector2(0,walljumpingPower.y);
            walljumpingCount=0f;
        }
    }   
    //快速下降
    private void fastfall()
    {
        if(Input.GetButtonDown("Drop")&&CanDrop == true)
        {
            StartCoroutine(Drop());
        }
    }
    private IEnumerator Drop()
    {
        CanDrop = false;
        Player.velocity = new Vector2(Player.velocity.x,-15f);
        tr.emitting = true;
        Physics2D.IgnoreLayerCollision(8,9,true);
        yield return new WaitForSeconds(0.3f);
        Physics2D.IgnoreLayerCollision(8,9,false);
        tr.emitting = false;
        yield return new WaitForSeconds(DashCD);
        CanDrop = true;
    }

    //受擊判定&核心表現
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "enemy" && HitCD != true)
        {
            health = health-10;
            HitCD = true;
            wait();
            HitCD = false;    
            shoot.BurstUI.fillAmount += 0.1f;
        }
        if (coll.gameObject.tag == "BloodOrb")
        {
            health += 2;
            if(health >= 100)
            {
                health = maxhealth;
            }
            shoot.BurstUI.fillAmount += 0.02f;
            Destroy(coll.gameObject);
        }
        GameObject.Find("PlayerCore").transform.localScale = new Vector3(health/100,health/100,1f);
    }
    void Dead()
    {
        if(health<=0)
        {
            SceneManager.LoadScene("Death");
        }
    }
    private IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
    }
}