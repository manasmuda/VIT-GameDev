using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunScript : MonoBehaviour
{

    public int value;
    public int probability;
    public GameObject dest;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { GameManager.instance.BatRunsSelected(value,probability,dest.transform.position); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
