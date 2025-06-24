using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    private float MoveChar;
    [SerializeField] private float MoveForce = 10.0f;
    private Animator anim;
    private string walk = "walk";
    private string run = "run";
    private Rigidbody2D PlayerRigid;
    
      
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        PlayerRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerMove();
        runanim();
        animatingPlayer();
        
    }
    public void playerMove()
    {
        MoveChar = Input.GetAxisRaw("Horizontal");
        PlayerRigid.velocity = new Vector3(MoveChar * MoveForce, PlayerRigid.velocity.y,0f);
        //transform.position += new Vector3(MoveChar * Time.deltaTime * MoveForce, 0.0f, 0.0f) ;
    }
    void animatingPlayer()
    {
        
        Vector3 CharacterScale = transform.localScale;
        if (MoveChar > 0.0f)
        {
            anim.SetBool(walk, true);
            CharacterScale.x = 1.2f;
        }
        else if (MoveChar < 0.0f)
        {
            anim.SetBool(walk, true);
            CharacterScale.x = -1.2f;
        }
        else
        {
            anim.SetBool(walk, false);
        }
        transform.localScale = CharacterScale;
    }  
    void runanim()
    {
        if (Input.GetKey(KeyCode.LeftShift) && MoveChar != 0)
        {
            MoveForce = 16.0f;
            anim.SetBool(run, true);
        }
        else
        {
            MoveForce = 10.0f;
            anim.SetBool(run, false);
        }
    }
    
}
