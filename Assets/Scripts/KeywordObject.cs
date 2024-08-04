using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeywordObject : MonoBehaviour
{
    [SerializeField] private TMP_Text keyword;
    private KeywordAdder keywordAdder;

    public void SetUp(KeywordAdder keywordAdder, string str)
    {
        this.keywordAdder = keywordAdder;
        keyword.text = str;
    }

    public void Remove()
    {
        keywordAdder.RemoveKeyword(keyword.text);
    }
}
