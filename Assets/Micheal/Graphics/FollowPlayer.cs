using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    GameObject player;
    public float acceleration = 20f;
    private float force;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        force = acceleration * rigidbody2D.mass;
    }
    void FixedUpdate()
    {
        Vector2 playerPosi = player.transform.position;
        Vector2 move = new Vector2(transform.position.x - playerPosi.x, transform.position.y - playerPosi.y);//new Vector2( (transform.position.x - playerPosi.x) / Mathf.Abs((transform.position.x - playerPosi.x)),
                                                                                                             //(transform.position.y - playerPosi.y) / Mathf.Abs((transform.position.y - playerPosi.y)) );
        move.Normalize();

        rigidbody2D.AddForce(-move * force * Time.deltaTime, ForceMode2D.Impulse);
    }
}
