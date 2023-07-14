using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [HideInInspector] public int winner = 0;
    [SerializeField] private TextMesh GiveUpUI = null;
    [SerializeField] private TextMesh GameEndUI = null;
    [SerializeField] private TextMesh P1WinUI = null;
    [SerializeField] private TextMesh P2WinUI = null;

    private bool IsGiveUpUIfade = false;
    private bool IsGameEndUIfade = false;
    public bool AllMovementEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        GiveUpUI.GetComponent<MeshRenderer>().sortingLayerName = "Text";
        GameEndUI.GetComponent<MeshRenderer>().sortingLayerName = "Text";
        P1WinUI.GetComponent<MeshRenderer>().sortingLayerName = "Text";
        P2WinUI.GetComponent<MeshRenderer>().sortingLayerName = "Text";
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGiveUpUIfade)
        {
            GiveUpUI.transform.localScale -= new Vector3(Time.deltaTime,Time.deltaTime,Time.deltaTime);

            if (GiveUpUI.transform.localScale.x <= 1)
            {
                GiveUpUI.transform.localPosition = Vector3.one;
                GiveUpUI.gameObject.SetActive(false);
                IsGiveUpUIfade = false;
                IsGameEndUIfade = true;
            }
        }

        if (IsGameEndUIfade)
        {
            GameEndUI.transform.localScale -= new Vector3(Time.deltaTime,Time.deltaTime,Time.deltaTime);

            if (GameEndUI.transform.localScale.x <= 1)
            {
                GameEndUI.transform.localPosition = Vector3.one;
                GameEndUI.gameObject.SetActive(false);
                IsGameEndUIfade = false;

                if (winner == 1)
                {
                    P1WinUI.gameObject.SetActive(true);
                }
                else if (winner == 2)
                {
                    P2WinUI.gameObject.SetActive(true);
                }
                SoundManager.Instance.PlayBGM(2);
                AllMovementEnd = true;
            }
        }

        if (AllMovementEnd && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    public void ShowGiveUpUI()
    {
        GiveUpUI.gameObject.SetActive(true);
        IsGiveUpUIfade = true;
    }

    public void ShowGameEndUI()
    {
        GameEndUI.gameObject.SetActive(true);
        IsGameEndUIfade = true;
    }
}
