using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private int spawnNum = 10;
    [SerializeField] private GameObject spawnObj = null;
    [SerializeField] private Transform maxPos = null;
    [SerializeField] private Transform minPos = null;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnStart());   
    }

    private IEnumerator SpawnStart()
    {
        while(true)
        {
            SpawnPiece();

            yield return new WaitForSeconds(1);
        }
    }

    private void SpawnPiece()
    {
        for (int i = 0; i < spawnNum; i++)
        {
            GameObject obj = Instantiate(spawnObj, new Vector3(Random.Range(minPos.position.x, maxPos.position.x), maxPos.position.y+Random.Range(0,5), maxPos.position.z), Quaternion.identity);
            PieceSpin piece = obj.GetComponent<PieceSpin>();
            piece.SetNameText();
        }
    }
}
