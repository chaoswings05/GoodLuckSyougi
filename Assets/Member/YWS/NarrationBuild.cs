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
    [Header("駒が成ったらこれを入れる")] public AudioClip PieceReversed;
    private Vector2Int PreviousTurnPos = Vector2Int.zero;

    private Dictionary<string, int> PieceNameDictionary = new Dictionary<string, int>()
    {
        {"玉将",0},
        {"飛車",1},
        {"歩兵",2},
        {"桂馬",3},
        {"角行",4},
        {"金将",5},
        {"銀将",6},
        {"香車",7},
        {"と金",8},
        {"成桂",9},
        {"成銀",10},
        {"成香",11},
        {"龍馬",12},
        {"龍王",13},
        {"聖剣",14},
        {"忍者",15},
        {"屈強",16},
        {"卑怯",17},
        {"富豪",18},
        {"俳優",19},
        {"砲台",20},
        {"筋肉",21},
    };

    public void WordCombine(int PlayerNum, Vector2Int movedPos, string Name, bool IsPieceTurn)
    {
        SoundManager.Instance.Narration.Add(Player[PlayerNum-1]);

        SoundManager.Instance.Narration.Add(RampantNum[10-movedPos.x-1]);

        SoundManager.Instance.Narration.Add(ColumnNum[10-movedPos.y-1]);

        if (movedPos == PreviousTurnPos)
        {
            SoundManager.Instance.Narration.Add(SamePlace);
        }
        PreviousTurnPos = movedPos;

        SoundManager.Instance.Narration.Add(PieceName[PieceNameDictionary[Name]]);

        if (IsPieceTurn)
        {
            SoundManager.Instance.Narration.Add(PieceReversed);
        }
    }
}
