using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    [SerializeField]
    GameObject handUI;
    public void OnPiecesDisplay()
    {
        handUI.SetActive(true);
    }
    public void OnPiecesHidden()
    {
        handUI.SetActive(false);
    }
}