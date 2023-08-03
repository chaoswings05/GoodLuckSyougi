using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PieceDataBase : ScriptableObject
{
    public List<PieceParameter> pieceList = new List<PieceParameter>();
}
