using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KomaManager : MonoBehaviour
{
    //�L�������ׂĂ��Ǘ�����B
    public List<Koma> komas = new List<Koma>();
    void Start()
    {
        // ��v����f�[�^�^�̂̎q�v�f�����ׂĎ擾����
        GetComponentsInChildren(komas);
    }


    public Koma GetKoma(Vector2Int pos)
    {
        foreach (var koma in komas)
        {
            if (koma.Position == pos)
            {
                Debug.Log(koma);
                return koma;
            }
        }
        return null;
    }

}
