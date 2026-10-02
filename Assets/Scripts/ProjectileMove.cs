using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float speed = 6;
    public int points = 100;
    public int damage = 1;
    void Update()
    {
        transform.Translate(-transform.right * speed * Time.deltaTime);
        if(transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }
}
