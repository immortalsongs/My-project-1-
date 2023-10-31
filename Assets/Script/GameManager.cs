using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int Point=0;
    public TMP_Text pointTXT;

    public Animator pointAni;

    public bool inMenu;

    public GameObject PauseMenu, DeathMenu;


    public int bossCount = 0;
    public GameObject boss1, boss2;
    public GameObject SpawnBoss;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        inMenu = false;
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            IncreasePoint(10001);
        }
    }


    

    public GameManager IncreasePoint(int p)
    {
        Point += p;
        pointTXT.text = Point.ToString();
        if(Point>10000 && bossCount<1)
        {
            Instantiate(boss1, SpawnBoss.transform.position, Quaternion.identity);
            SpawnBoss.GetComponent<Animator>().SetBool("Spawn", true);
            bossCount++;
        }
        if (Point > 100000 && bossCount >2)
        {
            GameObject temp=Instantiate(boss2, SpawnBoss.transform.position, Quaternion.identity);
            temp.transform.SetParent(SpawnBoss.transform.parent);
            bossCount++;
        }
        StartCoroutine(WaitAnimation());
        return this;
    }

    IEnumerator WaitAnimation()
    {
        pointAni.SetBool("Increase", true);
        yield return new WaitForSeconds(0.2f);
    }

    public void Death()
    {
        Time.timeScale = 0;
        DeathMenu.SetActive(true);
        inMenu = true;
    }

    public void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
        inMenu = true;
    }
    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        inMenu = false;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        inMenu = false;
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
