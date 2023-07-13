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

    private bool once = true;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))

        GachaStart();
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
            gachaItemSpawn.GachaGacha();
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
