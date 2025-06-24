using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboAI : MonoBehaviour
{
    private Rigidbody2D EnemyRigid;
    [SerializeField] private float MoveSpeed = 10.0f;
    [SerializeField] private float MaxRoboHealth = 2;
    private float CurrentRoboHealth;
    private Animator RoboAnim;
    
    
   
    // Start is called before the first frame update
    void Start()
    {
        EnemyRigid = GetComponent<Rigidbody2D>();
        CurrentRoboHealth = MaxRoboHealth;
        RoboAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (IsFacingRight())
        {
            EnemyRigid.velocity = new Vector2(-MoveSpeed , 0f);
        }
        else
        {
            EnemyRigid.velocity = new Vector2(MoveSpeed , 0f);
        }
    }
    private bool IsFacingRight()
    {
        return transform.localScale.x < 0f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.localScale = new Vector2(transform.localScale.x * -1,transform.localScale.y);
        
    }   
    private void TakeDamage(int amount)
    {
        CurrentRoboHealth -= amount;
        if(CurrentRoboHealth <= 0)
        {
            RoboAnim.SetTrigger("death");
        }
    }
}
