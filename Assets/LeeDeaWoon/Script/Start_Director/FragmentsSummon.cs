using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentsSummon : MonoBehaviour
{
    public List<GameObject> fragmentList = new List<GameObject>();

    [Header("ÆÄÆí YÁÂÇ¥")]
    public float posYMin;
    public float posYMax;

    void Start()
    {
        Fragments_Summon();
    }

    void Update()
    {

    }

    void Fragments_Summon()
    {
        for (int i = 0; i < 4; i++)
            Instantiate(fragmentList[i], new Vector3(-10, Random.Range(posYMin, posYMax), 0), Quaternion.identity).transform.parent = gameObject.transform;
    }
}
