using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollect : MonoBehaviour
{
    [SerializeField] private int coin = 0;
    [SerializeField] private Text CoinCol;
    [SerializeField] private AudioSource CoinSoundEffect;
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("coin"))
        {
            CoinSoundEffect.Play();
            Destroy(collision.gameObject);
            coin++;
            CoinCol.text = "Coins : " + coin;
        }
    }
}
