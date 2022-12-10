using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{
    public int mapPieces = 0;
    public int coins = 0;
    [SerializeField] private Text CoinsText;
    [SerializeField] private AudioSource Collection;
    [SerializeField] private AudioSource MapComplete;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CoinTrigger collected = collision.gameObject.GetComponent<CoinTrigger>();
        if (collision.gameObject.CompareTag("Coin"))
        {
            Collection.Play();
            collected.Disappear();
            coins++;
            CoinsText.text = "Coins: " + coins;
            DontDestroyOnLoad(CoinsText);


        }
        else if (collision.gameObject.CompareTag("MapPiece") && mapPieces < 4)
        {
            Collection.Play();
            mapPieces++;
            collected.Disappear();
        }
        else if (collision.gameObject.CompareTag("MapPiece") && mapPieces > 3)
        {
            MapComplete.Play();
            mapPieces++;
            collected.Disappear();

        }
    }

}
