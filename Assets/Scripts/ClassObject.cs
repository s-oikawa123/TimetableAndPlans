using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClassObject : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text idText;
    public void SetUp(Class c)
    {
        nameText.text = c.Name;
        idText.text = c.Id;
    }

    public void ResetText()
    {
        nameText.text = string.Empty;
        idText.text = string.Empty;
    }
}
