using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float VeloMax;
    public bool isVisible=false;
    float LifeTime = 5f;

    public GameObject exPlosion;

    public int Point;
    public GameObject PointTXT;
    public SpriteRenderer spriteRenderer;
    public Collider2D collider;

    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        if (isGrounded())
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isVisible)
        {
            LifeTime -= Time.deltaTime;
        }
        if (LifeTime < 0) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude >= VeloMax)
            {
                Dead(collision);
                GameManager.instance.IncreasePoint(Point);
                PointTXT.SetActive(true);
            }
            else if(Point==1000)
            {
                collision.gameObject.GetComponent<Grapling>().Hit();
            }
        }
    }

    bool isGrounded()
    {
        int count = 0;
        if (Physics2D.OverlapCircle(transform.position, 2.5f, groundLayer))
        {
            count++;
            Debug.Log(count);
        }
        if (count >= 2) return true;
        else return false;
    }

    public void Dead(Collider2D collision)
    {
        collider.enabled = false;
        var Explo = Instantiate(exPlosion, transform.position, Quaternion.identity);
        if(collision!=null)
            Explo.gameObject.transform.rotation = Quaternion.Euler(transform.position - collision.transform.position);
        spriteRenderer.color = new Color(0, 0, 0, 0);
    }

    public void DestroyGameObject()
    {
        
        Destroy(gameObject);
    }
    private void OnBecameVisible()
    {
        isVisible = true;
    }
    private void OnBecameInvisible()
    {
        if (isVisible)
        {
            Destroy(gameObject);
        }
        else isVisible = true;
    }
}
