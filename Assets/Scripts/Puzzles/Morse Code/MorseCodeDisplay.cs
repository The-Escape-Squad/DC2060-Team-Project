using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseCodeData
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

    public static float fTimeUnit = 0.25f;  // The time value for a dot in morse code

    public static Dictionary<char, int[]> morseCodeValues = new Dictionary<char, int[]>()
    {
        { '1', new int[] { 1, 3, 3, 3, 3 } },
        { '2', new int[] { 1, 1, 3, 3, 3 } },
        { '3', new int[] { 1, 1, 1, 3, 3 } },
        { '4', new int[] { 1, 1, 1, 1, 3 } },
        { '5', new int[] { 1, 1, 1, 1, 1 } },
        { '6', new int[] { 3, 1, 1, 1, 1 } },
        { '7', new int[] { 3, 3, 1, 1, 1 } },
        { '8', new int[] { 3, 3, 3, 1, 1 } },
        { '9', new int[] { 3, 3, 3, 3, 1 } },
        { '0', new int[] { 3, 3, 3, 3, 3 } },
    };
}

public class MorseCodeParser
{

    public static List<int> ParseMorseCode(int intValue)
    {
        List<int> parsedMorseCode = new List<int>();

        parsedMorseCode.Add(1);
        parsedMorseCode.Add(1);
        parsedMorseCode.Add(1);
        parsedMorseCode.Add(-7);

        char[] valueToParse = intValue.ToString().ToCharArray();

        for(int i = 0; i < valueToParse.Length; i++)
        {
            int[] characterValue = MorseCodeData.morseCodeValues[valueToParse[i]];
            for(int j = 0; j < characterValue.Length; j++)
            {
                parsedMorseCode.Add(characterValue[j]);
                if(j < characterValue.Length - 1) parsedMorseCode.Add(-1);
            }
            parsedMorseCode.Add(-7);
        }

        return parsedMorseCode;
    }

}

public class MorseCodeDisplay : MonoBehaviour
{

    public SpriteRenderer m_light;

    public int morseValue { get; private set; }
    private List<int> morseSentence = new List<int>();

    public Color onColour;
    public Color offColour;

    public LockedInteractable[] bricks;

    public void Start()
    {
        int value = Random.Range(1, 16);
        Debug.Log(value);

        for (int i = 0; i < bricks.Length; i++)
        {
            if(i != value - 1)
            {
                bricks[i].interactionMessage = "There doesn't seem to be anything here...";
                bricks[i].onInteractEvent = new UnityEngine.Events.UnityEvent();
                bricks[i].interactionClip = null;
            }
        }

        morseSentence = MorseCodeParser.ParseMorseCode(value);
        StartCoroutine(DisplayMorseSentence(morseSentence));
    }

    public IEnumerator DisplayMorseSentence(List<int> morseString)
    {
        for (int i = 0; i < morseString.Count; ++i)
        {
            if (morseString[i] > 0)
            {
                //Positive Morse 
                
                m_light.color = onColour;
                //Either dot or dash turning on light object per unit time
                //Turning on light for x amount of time (eOnType * fUnit)
                yield return new WaitForSeconds(Mathf.Abs(morseString[i] * MorseCodeData.fTimeUnit));
                
                //Light off between dots/dashes
                m_light.color = offColour;
                yield return new WaitForSeconds(Mathf.Abs((float)MorseCodeData.eOffType.eShortSpace) * MorseCodeData.fTimeUnit);
            }

            if (morseString[i] < 0)
            {
                //Negative Morse
                
                m_light.color = offColour;

                yield return new WaitForSeconds(Mathf.Abs(morseString[i] * MorseCodeData.fTimeUnit));
            }
        }

        // Play the message again
        StartCoroutine(DisplayMorseSentence(morseSentence));
    }
}
