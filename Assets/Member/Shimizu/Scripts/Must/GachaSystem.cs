using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private GachaItemSpawn gachaItemSpawn = null;
    [SerializeField] private Image gachaKomaNameObj = null;
    [SerializeField] private Sprite[] gachaKomaNameUI = null;
    private Dictionary<string, int> gachaKomaNum = new Dictionary<string, int>()
    {
        {"SEI",0},
        {"SINOBI",1},
        {"KUKKYOU",2},
        {"HIKYOU",3},
        {"HUGOU",4},
        {"HOU",5},
        {"NIKU",6},
        {"HAIYUU",7},
    };
    [SerializeField] private GManager gameManager = null;
    public int gachaNum = 0;

    private bool once = true;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //�K�`���̃C���X�g��\��
    public void GachaStart(int num)
    {
        gachaNum = num;
        GachaImage.SetActive(true);
    }

    //�K�`���̃{�^���������Ƃ�
    public void GachaButtonDown()
    {
        if(once)
        {
            once = false;
            StartCoroutine(DelayEffect());
        }
    }

    IEnumerator DelayEffect()
    {
        for (int i = 0; i < gachaNum; i++)
        {
            audioSource.PlayOneShot(audioClip);
            gachaItemSpawn.GachaGacha();
            yield return new WaitForSeconds(1f);
            gachaItem.SetActive(true);
            particle.Play();
            yield return new WaitForSeconds(2f);
            gachaItem.SetActive(false);
        }
        GachaImage.SetActive(false);
        once = true;
        gameManager.GachaFinish();
    }

    public void gachaItemUpdate(string name)
    {
        Debug.Log(name);
        gachaKomaNameObj.sprite = gachaKomaNameUI[gachaKomaNum[name]];
        gameManager.KomaChange(gachaKomaNum[name], gachaKomaNameUI[gachaKomaNum[name]]);
    }
}
