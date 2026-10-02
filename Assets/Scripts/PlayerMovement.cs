using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 4;
    private int scoreVal = 0;
    private int healthVal = 5;

    public TextMeshProUGUI scorebox;
    public TextMeshProUGUI healthbox;
    void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(transform.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(-transform.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-transform.right * speed * Time.deltaTime);
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -7f, 4f), Mathf.Clamp(transform.position.y, -4f, 4f), transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            if(collision.GetComponent<ProjectileMove>() != null)
            {
                scoreVal += collision.GetComponent<ProjectileMove>().points;
                scorebox.text = "Score: " + scoreVal; 
                healthVal += collision.GetComponent<ProjectileMove>().damage;
                healthbox.text = "Lives: " + healthVal;
                if (healthVal <= 0)
                {
                    scoreVal = 0;
                    healthVal = 5;
                    scorebox.text = "Score: " + scoreVal;
                    healthbox.text = "Lives: " + healthVal;
                    transform.position = new Vector3(-7, 0, 0);

                    ProjectileMove[] projectiles = FindObjectsOfType<ProjectileMove>();
                    foreach (ProjectileMove projectile in projectiles)
                    {
                        Destroy(projectile.gameObject);
                    }

                    AudioSource bgm = GameObject.Find("BGM").GetComponent<AudioSource>();
                    if (bgm != null)
                    {
                        bgm.Stop();
                        bgm.Play();
                    }
                }
            }
            Destroy(collision.gameObject);
        }
    }
}
