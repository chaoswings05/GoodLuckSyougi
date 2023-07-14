using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpin : MonoBehaviour
{
    [SerializeField] private TextMesh nameText = null;
    private string[] pieceName = new string[22]
    {
        "玉\n将",
        "飛\n車",
        "角\n行",
        "金\n将",
        "銀\n将",
        "桂\n馬",
        "香\n車",
        "歩\n兵",
        "と",
        "成\n香",
        "成\n桂",
        "成\n銀",
        "龍",
        "馬",
        "聖\n剣",
        "忍\n者",
        "屈\n強",
        "筋\n肉",
        "砲\n台",
        "俳\n優",
        "富\n豪",
        "卑\n怯",
    };

    // Update is called once per frame
    void Update()
    {
        int randomMove = Random.Range(1,3);

        switch(randomMove)
        {
            case 1:
            this.transform.Rotate(new Vector3(100*Time.deltaTime,0,0));
            break;

            case 2:
            this.transform.Rotate(new Vector3(0,100*Time.deltaTime,0));
            break;

            case 3:
            this.transform.Rotate(new Vector3(0,0,100*Time.deltaTime));
            break;

            default:
            break;
        }
    }

    public void SetNameText()
    {
        int num = Random.Range(0, pieceName.Length);

        nameText.text = pieceName[num];

        if (num >= 8 && num <= 13)
        {
            nameText.color = Color.red;
        }

        this.transform.Rotate(Random.Range(0,360), Random.Range(0,360), Random.Range(0,360));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("DeleteZone"))
        {
            Destroy(this.gameObject);
        }    
    }
}
