using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Ob1;
    public float SpawnTime;
    public float speed;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = SpawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0 && Grapling.instance.rb.velocity.magnitude > 2f)
        {
            if (Grapling.instance.rb.velocity.magnitude > speed)
            {
                Instantiate(Ob1, transform.position, Quaternion.identity);
                time = SpawnTime;
            }
           
        }
    }
}
