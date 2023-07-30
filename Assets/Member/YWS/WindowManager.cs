using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowManager : MonoBehaviour
{
    [SerializeField] private NarrationBuild narration = null;
    [SerializeField] private GameObject P1PieceEffectWindow = null;
    [SerializeField] private Text P1PieceName = null;
    [SerializeField] private Text P1PieceEffect = null;
    [SerializeField] private Text P1PieceMovement = null;
    [SerializeField] private GameObject P2PieceEffectWindow = null;
    [SerializeField] private Text P2PieceName = null;
    [SerializeField] private Text P2PieceEffect = null;
    [SerializeField] private Text P2PieceMovement = null;
    [SerializeField] private GameObject NinjaEffectWindow = null;
    private bool IsNinjaEffectThinking = false;
    private bool IsShowingNinjaWindow = false;
    [SerializeField] private GameObject HikyoEffectWindow = null;
    private bool IsHikyoEffectThinking = false;
    private bool IsShowingHikyoWindow = false;
    [SerializeField] private GameObject ReverseWindow = null;
    private bool IsReverseThinking = false;
    private bool IsShowingReverseWindow = false;
    [SerializeField] private Image PieceImageBefore = null;
    [SerializeField] private Image PieceImageAfter = null;
    [SerializeField] private Sprite[] PieceImageBeforeList = null;
    [SerializeField] private Sprite[] PieceImageAfterList = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (IsNinjaEffectThinking)
            {
                if (IsShowingNinjaWindow)
                {
                    UnshowNinjaWindow();
                }
                else
                {
                    ShowNinjaWindow();
                }
            }
            else if (IsHikyoEffectThinking)
            {
                if (IsShowingHikyoWindow)
                {
                    UnshowHikyoWindow();
                }
                else
                {
                    ShowHikyoWindow();
                }
            }
            else if (IsReverseThinking)
            {
                if (IsShowingReverseWindow)
                {
                    UnshowReverseWindow();
                }
                else
                {
                    ShowReverseWindow();
                }
            }
        }
    }

    public void ShowP1PieceEffectWindow(string komaName)
    {
        switch(komaName)
        {
            case "Seiken":
            P1PieceName.text = "聖剣";
            P1PieceMovement.text = "前後方向の突き当たりにしか移動できない";
            P1PieceEffect.text = "自分の駒を含め、\n通ったマスの駒を全て取る";
            break;

            case "Ninja":
            P1PieceName.text = "忍者";
            P1PieceMovement.text = "上下左右に1マス動ける";
            P1PieceEffect.text = "通常移動を放棄し、\n空いているマスに移動できる";
            break;

            case "Kukkyou":
            P1PieceName.text = "屈強";
            P1PieceMovement.text = "上下左右に1マス動ける";
            P1PieceEffect.text = "この駒は「筋肉」以外に取られない";
            break;

            case "Hikyo":
            P1PieceName.text = "卑怯";
            P1PieceMovement.text = "上下左右に1マス動ける";
            P1PieceEffect.text = "通常移動を放棄し、自分の駒一つと場所を入れ替えられる";
            break;

            case "Fugo":
            P1PieceName.text = "富豪";
            P1PieceMovement.text = "上下左右に1マス動ける";
            P1PieceEffect.text = "この駒で相手の駒を取った場合、ガチャを追加で一回回す";
            break;

            case "Houdai":
            P1PieceName.text = "砲台";
            P1PieceMovement.text = "上下左右に何マスでも動ける";
            P1PieceEffect.text = "この駒は移動先にある駒を一個飛ばして、その先にある駒を取れる";
            break;

            case "Kinniku":
            P1PieceName.text = "筋肉";
            P1PieceMovement.text = "全方向に1マス動ける";
            P1PieceEffect.text = "この駒は唯一「屈強」を取る事ができる";
            break;

            case "Haiyu":
            P1PieceName.text = "俳優";
            P1PieceMovement.text = "上下左右に1マス動ける";
            P1PieceEffect.text = "この駒は、\n取った駒と同じ駒になる";
            break;

            default:
            break;
        }
        P1PieceEffectWindow.SetActive(true);
    }

    public void UnshowP1PieceEffectWindow()
    {
        P1PieceEffectWindow.SetActive(false);
    }

    public void ShowP2PieceEffectWindow(string komaName)
    {
        switch(komaName)
        {
            case "Seiken":
            P2PieceName.text = "聖剣";
            P2PieceMovement.text = "前後方向の突き当たりにしか移動できない";
            P2PieceEffect.text = "自分の駒を含め、\n通ったマスの駒を全て取る";
            break;

            case "Ninja":
            P2PieceName.text = "忍者";
            P2PieceMovement.text = "上下左右に1マス動ける";
            P2PieceEffect.text = "通常移動を放棄し、\n空いているマスに移動できる";
            break;

            case "Kukkyou":
            P2PieceName.text = "屈強";
            P2PieceMovement.text = "上下左右に1マス動ける";
            P2PieceEffect.text = "この駒は「筋肉」以外に取られない";
            break;

            case "Hikyo":
            P2PieceName.text = "卑怯";
            P2PieceMovement.text = "上下左右に1マス動ける";
            P2PieceEffect.text = "通常移動を放棄し、自分の駒一つと場所を入れ替えられる";
            break;

            case "Fugo":
            P2PieceName.text = "富豪";
            P2PieceMovement.text = "上下左右に1マス動ける";
            P2PieceEffect.text = "この駒で相手の駒を取った場合、ガチャを追加で一回回す";
            break;

            case "Houdai":
            P2PieceName.text = "砲台";
            P2PieceMovement.text = "上下左右に何マスでも動ける";
            P2PieceEffect.text = "この駒は移動先にある駒を一個飛ばして、その先にある駒を取れる";
            break;

            case "Kinniku":
            P2PieceName.text = "筋肉";
            P2PieceMovement.text = "全方向に1マス動ける";
            P2PieceEffect.text = "この駒は唯一「屈強」を取る事ができる";
            break;

            case "Haiyu":
            P2PieceName.text = "俳優";
            P2PieceMovement.text = "上下左右に1マス動ける";
            P2PieceEffect.text = "この駒は、\n取った駒と同じ駒になる";
            break;

            default:
            break;
        }
        P2PieceEffectWindow.SetActive(true);
    }

    public void UnshowP2PieceEffectWindow()
    {
        P2PieceEffectWindow.SetActive(false);
    }

    public void ShowNinjaWindow()
    {
        IsNinjaEffectThinking = true;
        NinjaEffectWindow.SetActive(true);
        IsShowingNinjaWindow = true;
    }

    public void UnshowNinjaWindow()
    {
        NinjaEffectWindow.SetActive(false);
        IsShowingNinjaWindow = false;
    }

    public void OnNinjaWindowYesButtonPress()
    {

    }

    public void OnNinjaWindowNoButtonPress()
    {

    }

    public void ShowHikyoWindow()
    {
        IsHikyoEffectThinking = true;
        HikyoEffectWindow.SetActive(true);
        IsShowingHikyoWindow = true;
    }

    public void UnshowHikyoWindow()
    {
        HikyoEffectWindow.SetActive(false);
        IsShowingHikyoWindow = false;
    }

    public void OnHikyoWindowYesButtonPress()
    {

    }

    public void OnHikyoWindowNoButtonPress()
    {
        
    }

    public void ShowReverseWindow()
    {
        switch(GManager.Instance.selectedKoma.PieceName)
        {
            case "歩兵":
            PieceImageBefore.sprite = PieceImageBeforeList[0];
            PieceImageAfter.sprite = PieceImageAfterList[0];
            break;

            case "香車":
            PieceImageBefore.sprite = PieceImageBeforeList[1];
            PieceImageAfter.sprite = PieceImageAfterList[1];
            break;

            case "桂馬":
            PieceImageBefore.sprite = PieceImageBeforeList[2];
            PieceImageAfter.sprite = PieceImageAfterList[2];
            break;

            case "銀将":
            PieceImageBefore.sprite = PieceImageBeforeList[3];
            PieceImageAfter.sprite = PieceImageAfterList[3];
            break;

            case "角行":
            PieceImageBefore.sprite = PieceImageBeforeList[4];
            PieceImageAfter.sprite = PieceImageAfterList[4];
            break;

            case "飛車":
            PieceImageBefore.sprite = PieceImageBeforeList[5];
            PieceImageAfter.sprite = PieceImageAfterList[5];
            break;

            default:
            break;
        }
        ReverseWindow.SetActive(true);
        IsReverseThinking = true;
        IsShowingReverseWindow = true;
    }

    public void UnshowReverseWindow()
    {
        IsShowingReverseWindow = false;
        ReverseWindow.SetActive(false);
    }

    public void OnReverseWindowYesButtonPress()
    {
        IsReverseThinking = false;
        UnshowReverseWindow();
        if (GManager.Instance.gamePhase == GManager.Phase.Player1WindowSelection)
        {
            GManager.Instance.gamePhase = GManager.Phase.Player1Narration;
            narration.WordCombine(1,GManager.Instance.selectedKoma.Position,GManager.Instance.selectedKoma.PieceName,true);
            SoundManager.Instance.PlayNarration();
            GManager.Instance.selectedKoma.PieceReverse();
            GManager.Instance.selectedKoma = null;
        }
        else if (GManager.Instance.gamePhase == GManager.Phase.Player2WindowSelection)
        {
            GManager.Instance.gamePhase = GManager.Phase.Player2Narration;
            narration.WordCombine(2,GManager.Instance.selectedKoma.Position,GManager.Instance.selectedKoma.PieceName,true);
            SoundManager.Instance.PlayNarration();
            GManager.Instance.selectedKoma.PieceReverse();
            GManager.Instance.selectedKoma = null;
        }
    }

    public void OnReverseWindowNoButtonPress()
    {
        IsReverseThinking = false;
        UnshowReverseWindow();
        if (GManager.Instance.gamePhase == GManager.Phase.Player1WindowSelection)
        {
            GManager.Instance.gamePhase = GManager.Phase.Player1Narration;
            narration.WordCombine(1,GManager.Instance.selectedKoma.Position,GManager.Instance.selectedKoma.PieceName,false);
            SoundManager.Instance.PlayNarration();
            GManager.Instance.selectedKoma = null;
        }
        else if (GManager.Instance.gamePhase == GManager.Phase.Player2WindowSelection)
        {
            GManager.Instance.gamePhase = GManager.Phase.Player2Narration;
            narration.WordCombine(2,GManager.Instance.selectedKoma.Position,GManager.Instance.selectedKoma.PieceName,false);
            SoundManager.Instance.PlayNarration();
            GManager.Instance.selectedKoma = null;
        }
    }
}
