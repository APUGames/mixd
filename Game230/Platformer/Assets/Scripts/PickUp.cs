using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    [SerializeField] AudioClip CoinPickupSFX;

    [SerializeField] int coinValue = 1;

    private void OnTriggerEnter2D(Collider2D collision) {

        FindObjectOfType<GameSession>().ProcessPlayerScore(coinValue);
        AudioSource.PlayClipAtPoint(CoinPickupSFX, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
