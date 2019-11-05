using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    Character playerHealth;
    public int bonusHealth = 10;

    void Awake()
    {
        playerHealth = FindObjectOfType<Character>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(playerHealth.life < 100)
        {
            Destroy(gameObject);
            playerHealth.life = playerHealth.life + bonusHealth; 

        }
    }

}
