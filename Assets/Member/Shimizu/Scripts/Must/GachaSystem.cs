using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaSystem : MonoBehaviour
{
    [SerializeField]
    GameObject GachaImage;
    [SerializeField]
    GameObject gachaItem;
    [SerializeField]
    ParticleSystem particle;
    [SerializeField]
    AudioClip audioClip;
    AudioSource audioSource;

    private bool once = true;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //�K�`���̃C���X�g��\��
    public void GachaStart()
    {
        GachaImage.SetActive(true);
    }
    //�K�`���̃{�^���������Ƃ�
    public void GachaButtonDown()
    {
        if(once)
        {
            once = false;
            audioSource.PlayOneShot(audioClip);
            StartCoroutine(DelayEffect());
        }
        
    }
    IEnumerator DelayEffect()
    {
        yield return new WaitForSeconds(1f);
        gachaItem.SetActive(true);
        particle.Play();
        yield return new WaitForSeconds(2f);
        GachaImage.SetActive(false);
        gachaItem.SetActive(false);
        once = false;

    }
}
