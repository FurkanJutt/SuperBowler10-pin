using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingMusic : MonoBehaviour
{
    AudioSource loadingSound;

    // Start is called before the first frame update
    void Start()
    {
        loadingSound = GetComponent<AudioSource>();
        loadingSound.volume = PlayerPerfControler.GetMasterVolume();
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetVolume(float volume)
    {
        loadingSound.volume = volume;
    }
}
