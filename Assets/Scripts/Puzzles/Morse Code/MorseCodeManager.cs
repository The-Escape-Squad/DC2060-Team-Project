using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseCodeMetaData
{
    public enum eOnType
    {
        eDot = 1,
        eDash = 3
    }

    public enum eOffType
    {
        eShortSpace = -1,        //Time between two similar letters
        eLongSpace  = -7         //Time Between different letters / words
    }

    public static float fTimeUnit = 0.5f;  //1 dot is set to half second
}

public class MorseCodeManager : MonoBehaviour
{
    static MorseCodeManager m_Instance;

    public static MorseCodeManager GetInstance() { return m_Instance; }

    void Start()
    {
        m_Instance = this;
    }

    public IEnumerator ProcessMorseSentence(GameObject obj, List<int> morseString)
    {
        Debug.Log("Morse Processing started,sentence size is " + morseString.Count);

        for (int i = 0; i < morseString.Count; ++i)
        {
            Debug.Log("Moving to next iteration");
            if (morseString[i] > 0)
            {
                //Positive Morse 
                
                obj.SetActive(true);
                Debug.Log("morse letter is " + morseString[i]);
                //Either dot or dash turning on light object per unit time
                //Turning on light for x amount of time (eOnType * fUnit)
                yield return new WaitForSeconds(Mathf.Abs(morseString[i] * MorseCodeMetaData.fTimeUnit));
                
                //Light off between dots/dashes
                obj.SetActive(false);
                Debug.Log("Moving to next letter");
                yield return new WaitForSeconds(Mathf.Abs((float)MorseCodeMetaData.eOffType.eShortSpace) * MorseCodeMetaData.fTimeUnit);
            }

            if (morseString[i] < 0)
            {
                //Negative Morse
                Debug.Log("Blank space");
                
                obj.SetActive(false);

                yield return new WaitForSeconds(Mathf.Abs(morseString[i] * MorseCodeMetaData.fTimeUnit));
            }
        }
    }
}
