using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    Character playerHealth;
    public int bonusHealth =1;

    void Awake()
    {
        playerHealth = FindObjectOfType<Character>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {       
            Destroy(gameObject);
            playerHealth.life = playerHealth.life + bonusHealth;  
    }

}
