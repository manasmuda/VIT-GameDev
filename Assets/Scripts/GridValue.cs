using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridValue : MonoBehaviour
{
    public int value;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { GameManager.instance.GridSelected(value); });
    }
}
