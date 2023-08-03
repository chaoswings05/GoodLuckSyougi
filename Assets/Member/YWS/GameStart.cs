using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    [SerializeField, Header("フェードを行うオブジェクト")] private TextMesh targetText = null;
    private bool IsFadeOut = false;
    private bool IsFadeIn = false;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayBGM(0);
        IsFadeOut = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SoundManager.Instance.StopBGM();
            SoundManager.Instance.PlaySE(1);
            SceneManager.LoadScene("ShougiMain");
        }

        if (IsFadeOut)
        {
            targetText.color -= new Color(0,0,0,Time.deltaTime);

            if (targetText.color.a <= 0)
            {
                targetText.color = new Color(targetText.color.r,targetText.color.g,targetText.color.b,0);
                IsFadeOut = false;
                IsFadeIn = true;
            }
        }

        if (IsFadeIn)
        {
            targetText.color += new Color(0,0,0,Time.deltaTime);

            if (targetText.color.a >= 1)
            {
                targetText.color = new Color(targetText.color.r,targetText.color.g,targetText.color.b,1);
                IsFadeIn = false;
                IsFadeOut = true;
            }
        }
    }
}
