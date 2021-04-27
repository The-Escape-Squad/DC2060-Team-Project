using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public GameObject m_light;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject obj = gameObject;
            List<int> sentence = new List<int>() {1,1,1,1,3,-7,1,1,3,3,3};
            StartCoroutine(MorseCodeManager.GetInstance().ProcessMorseSentence(m_light, sentence));
        }
    }
}
