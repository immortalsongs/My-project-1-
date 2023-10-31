using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{

    public float veloMax;
    public float HP=500;

    public bool Phase2=false;
    public Animator AniBoss;

    public LineRenderer Laser1, Laser2, Laser3;

    public GameObject Maincamera;

    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Maincamera = GameObject.Find("Main Camera");
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            AniBoss.SetBool("Phase2", true);
        }
    }

    public void Laser()
    {
        Laser1.gameObject.SetActive(true);
        Laser2.gameObject.SetActive(true);
        Laser3.gameObject.SetActive(true);
        Vector3 endPoint = new Vector3(33.5f, 0, 0);
        StartCoroutine(AnimateLine(Laser1));
        StartCoroutine(AnimateLine(Laser2));
        StartCoroutine(AnimateLine(Laser3));
    }

    IEnumerator AnimateLine(LineRenderer line)
    {
        float i = 0;
        while (i<33.5f)
        {
            i += 3.5f;
            line.SetPosition(1, new Vector3(i, 0, 0));
            yield return new WaitForEndOfFrame();
        }
    }

    public void EndLaser()
    {
        Laser1.gameObject.SetActive(false);
        Laser2.gameObject.SetActive(false);
        Laser3.gameObject.SetActive(false);
    }

    public void Detach()
    {
        transform.parent = null;
        AniBoss.enabled = false;
        transform.position = Player.transform.position + new Vector3(0, 5, 0);
        Debug.Log(transform.position);
    }

    public void Intach()
    {
        transform.SetParent(Maincamera.transform);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Obstacle>().Dead(this.GetComponent<CircleCollider2D>());
        }
        if (collision.gameObject.tag == "Player")
        {
            if (collision.contacts[0].otherCollider.transform.name == "Body")
            {
                HP -= collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
                if (HP < 250 && !Phase2)
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
