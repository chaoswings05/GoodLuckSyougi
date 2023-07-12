using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IconScript : MonoBehaviour
{
    [SerializeField]
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 1*speed*Time.deltaTime, 0);
    }
}
