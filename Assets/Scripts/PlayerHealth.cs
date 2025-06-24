using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private Rigidbody2D PlayerRigid;
    private Animator anim;
    [SerializeField] private AudioSource DeathSoundEffect;
    [SerializeField] private float MaxHealth = 3;
    private float CurrentHealth;
    [SerializeField] private Image Heart1, Heart2, Heart3;
    [SerializeField] private AudioSource HurtSoundEffect;
    [SerializeField] private AudioSource HeartSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
        anim = GetComponent<Animator>();
        PlayerRigid = GetComponent<Rigidbody2D>();
    }
    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        
        if(CurrentHealth == 2)
        {
            Heart3.fillAmount = 0f;
        }
        if (CurrentHealth == 1)
        {
            Heart2.fillAmount = 0f;
        }
        if (CurrentHealth <= 0)
        {
            Heart1.fillAmount = 0f;
            {
                DeathSoundEffect.Play();
                PlayerRigid.bodyType = RigidbodyType2D.Static;

                anim.SetTrigger("death");
            }
           
        }
    }

    private void RestartLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("trap"))
        {
            TakeDamage(1);
           
            HurtSoundEffect.Play();
            

            anim.SetTrigger("hurt");
        }
        if (collision.gameObject.CompareTag("death"))
        {
            TakeDamage(3);

            HurtSoundEffect.Play();
            Heart1.fillAmount = 0;
            Heart2.fillAmount = 0;
            Heart3.fillAmount = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("heart"))
        {
            HeartSoundEffect.Play();
            CurrentHealth = 3;
            Destroy(collision.gameObject);
            Heart1.fillAmount = 1;
            Heart2.fillAmount = 1;
            Heart3.fillAmount = 1;
        }       
    }
}
