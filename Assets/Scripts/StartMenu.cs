using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] AudioSource clickSoundEffect;
        
    public void UpdatingScene()
    {
        SceneManager.LoadScene(2);
    }

    public void Sounds()
    {
        clickSoundEffect.Play();
    }
}
