using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeywordAdder : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Application application;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private GameObject keywordPrefab;

    public void GetKeyword()
    {
        if (inputField.text.Length != 0)
        {
            string keyword = inputField.text;
            application.AddImportantKeyword(keyword);
        }
    }

    public void RemoveKeyword(string keyword)
    {
        application.RemoveImportantKeyword(keyword);
    }

    public void UpdateKeywordDisplay(IReadOnlyList<string> importantKeyword)
    {
        int count = rectTransform.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(rectTransform.GetChild(i).gameObject);
        }

        foreach (string keyword in importantKeyword)
        {
            KeywordObject keywordObject = Instantiate(keywordPrefab, rectTransform).GetComponent<KeywordObject>();
            keywordObject.SetUp(this, keyword);
        }
    }
}
