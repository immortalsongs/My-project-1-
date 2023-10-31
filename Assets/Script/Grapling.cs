using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Grapling : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer line;
    public DistanceJoint2D joint;
    public Rigidbody2D rb;

    public float pushForce;

    public static Grapling instance;

    public bool laserHit = false;

    public Animator Ani;

    private float HP = 200;

    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        joint.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && !GameManager.instance.inMenu && !IsPointerOverUIObject() && !laserHit)
        {
            Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
            rb.AddForce(new Vector2(mousePos.x-transform.position.x,0).normalized * pushForce);
            line.enabled = true;
            line.SetPosition(0, mousePos);
            line.SetPosition(1, transform.position);
            joint.enabled = true;
            joint.connectedAnchor = mousePos;
        }
        else if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            joint.enabled = false;
            line.enabled = false;
            if(rb.velocity.magnitude>25f)
                Spin();
        }
        if(joint.enabled)
        {
            line.SetPosition(1, transform.position);
        }
        if(laserHit)
        {
            StartCoroutine(LaserHit());
        }
    }


    public void Spin()
    {
        Ani.SetBool("Spin", true);
    }

    public void EndSpin()
    {
        Ani.SetBool("Spin", false);
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void Hit()
    {
        HP -= rb.velocity.magnitude/3;
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        while(slider.value>HP)
        {
            slider.value -= 1f;
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator LaserHit()
    {
        Debug.Log(1);
        yield return new WaitForSeconds(1f);
        laserHit = true;
    }
}
