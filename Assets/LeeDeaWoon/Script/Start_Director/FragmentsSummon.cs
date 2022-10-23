using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentsSummon : MonoBehaviour
{
    public GameObject fragmentPrefab;

    void Start()
    {
        Fragments_Summon();
    }

    void Update()
    {

    }

    void Fragments_Summon()
    {
        float posYMin = -3.0f;
        float posYMax = 2.0f;

        for (int i = 0; i < 5; i++)
            Instantiate(fragmentPrefab, new Vector3(-10, Random.Range(posYMin, posYMax), 0), Quaternion.identity).transform.parent = gameObject.transform;
    }
}
