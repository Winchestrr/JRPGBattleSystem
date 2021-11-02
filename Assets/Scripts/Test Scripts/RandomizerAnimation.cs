using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomizerAnimation : MonoBehaviour
{
    public Text randomizerText;
    public int randomIterations;
    private int tempInt;

    public void randomButton()
    {
        randomizerText.color = Color.black;
        StartCoroutine("randomizerTextAnim");
    }

    public IEnumerator randomizerTextAnim()
    {
        for(int i = 0; i < randomIterations; i++)
        {
            randomizerText.text = Random.Range(1, 100).ToString();
            yield return new WaitForSeconds(0.05f);
        }

        tempInt = Random.Range(1, 100);

        if(tempInt > 50)
        {
            randomizerText.color = Color.green;
            randomizerText.text = tempInt.ToString();
            Debug.Log("won");

            if(tempInt > 90)
            {
                randomizerText.text += "!";
            }
        }
        else
        {
            randomizerText.color = Color.red;
            randomizerText.text = tempInt.ToString();
            Debug.Log("lost");
        }
    }
}
