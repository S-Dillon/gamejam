using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    public int score = 0;
    
    
    

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            score++;
            Debug.Log("Score: " + score);
            Destroy(other.gameObject);
        }
    }
}
