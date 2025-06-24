using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private LayerMask PlatformsLayerMask;
    [SerializeField] private LayerMask SlopeLayerMask;
    private Animator anim;
    private Rigidbody2D PlayerRigid;
    private CapsuleCollider2D Collider;
    [SerializeField] private float JumpHeight = 20.0f;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float VelocityCutMultiplier;
    [SerializeField] private float CoyoteTime = 0.2f;
    [SerializeField] private float CoyoteTimeCounter;
    private float BufferTime = 0.2f;
    private float BufferTimeCounter;
    Vector2 gravity2d;
    private string jump = "jump";
    private bool IsJumping = false;
    [SerializeField] private AudioSource JumpSoundEffect;
    // Start is called before the first frame update
    void Start()
    {
        gravity2d = new Vector2(0,-Physics2D.gravity.y);
        PlayerRigid = GetComponent<Rigidbody2D>();  
        Collider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
     
        IsGrounded();
        IsOnSlope();
        if (IsGrounded() || IsOnSlope())
        {
            CoyoteTimeCounter = CoyoteTime;
        }
        else
        {
            CoyoteTimeCounter -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump"))
        {
            BufferTimeCounter = BufferTime;
        }
        else
        {
            BufferTimeCounter -= Time.deltaTime;
        }
        if (BufferTimeCounter > 0 && CoyoteTimeCounter > 0f)
        {
            JumpSoundEffect.Play();
            PlayerRigid.velocity = new Vector2(PlayerRigid.velocity.x, JumpHeight);
            //PlayerRigid.AddForce(Vector2.up * JumpHeight,ForceMode2D.Impulse);
            BufferTimeCounter = 0f;
        }
        if(!IsGrounded() && !IsOnSlope())
        {
            anim.SetBool(jump, true);
            IsJumping = true;
        }
        else
        {
            anim.SetBool(jump, false);
            IsJumping = false;
        }
        if(PlayerRigid.velocity.y < 0)
        {
            PlayerRigid.velocity -= gravity2d * Time.deltaTime * fallMultiplier;
        }
        if(Input.GetKeyUp(KeyCode.Space) && PlayerRigid.velocity.y > 0f)
        {
            PlayerRigid.velocity = new Vector2(PlayerRigid.velocity.x, PlayerRigid.velocity.y * VelocityCutMultiplier);
            CoyoteTimeCounter = 0f;
        }
        
        
    }
    public bool IsGrounded()
    {
        return Physics2D.BoxCast(Collider.bounds.center, Collider.bounds.size, 0f, Vector2.down, .1f, PlatformsLayerMask);
    }
    public bool IsOnSlope()
    {
        return Physics2D.BoxCast(Collider.bounds.center, Collider.bounds.size, 0f, Vector2.down, .1f, SlopeLayerMask);
    }
}
