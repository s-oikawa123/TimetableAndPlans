using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlanObject : MonoBehaviour
{
    [SerializeField] private TMP_Text fromText;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text dateText;
    private string id;

    public void SetUp(Plan plan)
    {
        id = plan.Id;
        fromText.text = plan.From;
        titleText.text = plan.Title;
        dateText.text = plan.Date.ToString();
    }

    public void OpenMail()
    {
        UnityEngine.Application.OpenURL($"https://mail.google.com/mail/u/0/#inbox/{id}");
    }
}
