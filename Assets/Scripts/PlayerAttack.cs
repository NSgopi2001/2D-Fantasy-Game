using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private LayerMask PlatformsLayerMask;
    private Animator anim;
    private CapsuleCollider2D Collider;
    [SerializeField] private AudioSource SwordSoundEffect;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Collider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && IsGrounded())
        {
            SwordSoundEffect.Play();
            anim.SetTrigger("attack");
        }

    }
    public bool IsGrounded()
    {
        return Physics2D.BoxCast(Collider.bounds.center, Collider.bounds.size, 0f, Vector2.down, .1f, PlatformsLayerMask);
    }
}
