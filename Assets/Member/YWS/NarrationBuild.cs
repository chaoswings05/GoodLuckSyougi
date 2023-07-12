using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrationBuild : MonoBehaviour
{
    [Header("先手/後手")] public AudioClip[] Player;
    [Header("横の数字")] public AudioClip[] RampantNum;
    [Header("縦の数字")] public AudioClip[] ColumnNum;
    [Header("前の手番で相手が動かした駒と同じマスに駒を動かしたらこれを入れる")] public AudioClip SamePlace;
    [Header("動かした駒の名前")] public AudioClip[] PieceName;
    [Header("今動かしたこの駒は最初左の駒なのか右の駒なのか")] public AudioClip[] LeftOrRight;
    [Header("駒が成ったらこれを入れる")] public AudioClip PieceReversed;

    private Dictionary<string, int> PieceNameDictionary = new Dictionary<string, int>()
    {
        {"玉将",0},
        {"飛車",1},
        {"歩",2},
        {"桂馬",3},
        {"角",4},
        {"金将",5},
        {"銀将",6},
        {"香車",7},
        {"と金",8},
        {"成桂",9},
        {"成銀",10},
        {"成香",11},
        {"馬",12},
        {"龍",13},
        {"聖剣",14},
        {"忍者",15},
        {"屈強",16},
        {"卑怯",17},
        {"富豪",18},
        {"俳優",19},
        {"砲台",20},
        {"筋肉",21},
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            WordCombine(1,6,7,false,"と金","",true);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SoundManager.Instance.PlayNarration();
        }
    }

    public void WordCombine(int PlayerNum, int Rampant, int Column, bool IsSamePlace, string Name, string OriginalPlace, bool IsPieceTurn)
    {
        SoundManager.Instance.Narration.Add(Player[PlayerNum-1]);

        SoundManager.Instance.Narration.Add(RampantNum[Rampant-1]);

        SoundManager.Instance.Narration.Add(ColumnNum[Column-1]);

        if (IsSamePlace)
        {
            SoundManager.Instance.Narration.Add(SamePlace);
        }

        SoundManager.Instance.Narration.Add(PieceName[PieceNameDictionary[Name]]);

        if (OriginalPlace == "左")
        {
            SoundManager.Instance.Narration.Add(LeftOrRight[0]);
        }
        else if (OriginalPlace == "右")
        {
            SoundManager.Instance.Narration.Add(LeftOrRight[1]);
        }

        if (IsPieceTurn)
        {
            SoundManager.Instance.Narration.Add(PieceReversed);
        }
    }
}
