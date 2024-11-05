using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUp;
    [SerializeField] int pointsForCoinPickup= 100;
    bool wasCollected = false;
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            AudioSource.PlayClipAtPoint(coinPickUp, Camera.main.transform.position);
            Destroy(gameObject);
            FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
        }
    }
}
