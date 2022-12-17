using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] AudioSource clickSoundEffect;
        
    public void UpdatingScene()
    {
        SceneManager.LoadScene(1);
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Sounds()
    {
        clickSoundEffect.Play();
    }
}
