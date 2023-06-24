using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] CursorController cursor;
    [SerializeField] KomaManager komaManager;
    [SerializeField] BaseMap baseMap;

    [HideInInspector]
    public bool playerSelected = false;


    private void Start()
    {
        baseMap.CreateBaseMap();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && playerSelected == false)
        {

            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//���̂܂܂��Ƃ��܂������Ȃ��̂�Camera.main.ScreenToWorldPoint�ō��W�`����ϊ�����B
            RaycastHit2D hit2D = Physics2D.Raycast(clickPosition, Vector2.down);
            if (hit2D && hit2D.collider)
            {
                cursor.SetPosition(hit2D.transform);
                TileObj tileobj = hit2D.collider.GetComponent<TileObj>();
                //�I���^�C���̍��W
                Debug.Log(tileobj.positionInt);
                //�L�����̍��W
                Koma koma = komaManager.GetKoma(tileobj.positionInt);
                if (koma)
                {
                    Debug.Log("����");
                }
                else
                {
                    Debug.Log("���Ȃ�");
                }
            }


        }
    }
}
