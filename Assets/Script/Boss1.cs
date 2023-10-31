using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{

    public float veloMax;
    public GameObject Player;

    public float speed;

    public Rigidbody2D rb;

    public float HP = 1000;

    public Animator AniBoss;

    bool Phase2=false;
    // Start is called before the first frame update
    void Start()
    {
        Player= GameObject.Find("Player");
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            AniBoss.SetBool("Dead", true);
        }

        transform.position = new Vector3(transform.position.x, Player.transform.position.y, transform.position.z);

        rb.velocity = new Vector2((Player.transform.position-transform.position).normalized.x * speed, 0);
    }

    public void Dead()
    {
        GameManager.instance.IncreasePoint(10000).bossCount++;
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Enemy")
        {
            collision.gameObject.GetComponent<Obstacle>().Dead(this.GetComponent<CircleCollider2D>());
        }
        if(collision.gameObject.tag=="Player")
        {

            if (collision.contacts[0].otherCollider.transform.name == "Body")
            {
                HP -= collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
                if (HP < 500 && !Phase2)
                {
                    AniBoss.SetBool("Phase2", true);
                    Phase2 = true;
                }
                else if (HP <= 0 && Phase2)
                {
                    AniBoss.SetBool("Dead", true);
                }
            }
            else if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude < veloMax) collision.gameObject.GetComponent<Grapling>().Hit();
        }
    }


}
