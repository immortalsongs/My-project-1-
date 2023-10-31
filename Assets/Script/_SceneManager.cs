using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _SceneManager : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    public void SettingButton()
    {

    }
    public void ShopButton()
    {

    }
}
