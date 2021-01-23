using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class puzzleHint : MonoBehaviour
{
    //public TMP_Text text;
    public TextMesh text;
    public int id = 0;

    // Start is called before the first frame update
    void Awake()
    {
        //text = GetComponent<TMP_Text>();
        text = GetComponent<TextMesh>();
        CombineLockController.UpdateHint += TypeHint;
    }

    private void TypeHint(int idPuzzle, int[] answers)
    {
        List<string> stringAnswers = new List<string>();

        if(id != idPuzzle)
        {
            return;
        }

        for (int i = 0; i < answers.Length; i++)
        {
            stringAnswers.Add(answers[i].ToString());
        }
        text.text = string.Join(",",stringAnswers);
    }

    private void OnDestroy()
    {
        CombineLockController.UpdateHint -= TypeHint;
    }
}
