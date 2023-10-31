using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHelper : MonoBehaviour
{
    public void DestroyGameObject()
    {
        
        transform.root.GetComponent<Obstacle>().DestroyGameObject();
    }
}
