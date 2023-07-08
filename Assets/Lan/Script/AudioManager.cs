using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource player;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        player = GetComponent<AudioSource>();

    }
    
    public void Play(string name)
    {
        //using egg:
        //ͨ AudioManager.Instance.Play("Die");

        AudioClip clip = Resources.Load<AudioClip>(name);

        //one times play voice
        player.PlayOneShot(clip);
    }

}
