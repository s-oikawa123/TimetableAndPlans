using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Application : MonoBehaviour
{
    private List<Plan> plans = new List<Plan>();
    private List<Plan> importantPlans = new List<Plan>();
    private Timetable timetable = new Timetable();
    private DateTime lastCheckedDate;
    private List<string> importantKeywords = new List<string>();
    private DisplayState displayState;
    [SerializeField] private GameObject planWindow;
    [SerializeField] private GameObject timetableWindow;
    [SerializeField] private ButtonManager[] buttonManagers;
    [SerializeField] private KeywordAdder keywordAdder;
    [SerializeField] private PlanDisplayer importantPlanDisplayer;
    [SerializeField] private PlanDisplayer planDisplayer;
    [SerializeField] private TimeTableDisplayer timeTableDisplayer;
    private enum DisplayState
    {
        none,
        plan,
        timetable
    }


    public void Start()
    {
        lastCheckedDate = DateTime.Now.AddMonths(-1);
        UpdatePlans();
        UpdateTimetable();
        Display();
    }

    public void UpdatePlans()
    {
        var newPlans = GmailAPI.FetchPlans(lastCheckedDate);
        plans.AddRange(newPlans);
        if (plans.Count == 0)
        {
            lastCheckedDate = DateTime.Now;
        }
        else
        {
            lastCheckedDate = plans.Max(plan => plan.Date);
        }
        FilterImportantPlans();
        DisplayPlans();
        DisplayImportantPlans();
    }

    public void UpdateTimetable()
    {
        timetable.Classes = GoogleClassroomAPI.FetchTimetable();
        DisplayTimetable();
    }

    public void ToggleDisplayMode(int index)
    {
        if (displayState != (DisplayState)(index + 1))
        {
            for (int i = 0; i < buttonManagers.Length; i++)
            {
                if (i != index)
                {
                    buttonManagers[i].ResetButton();
                }
            }

            displayState = (DisplayState)(index + 1);

            Display();
        }
    }

    public void AddImportantKeyword(string keyword)
    {
        if (!importantKeywords.Contains(keyword))
        {
            importantKeywords.Add(keyword);
            keywordAdder.UpdateKeywordDisplay(importantKeywords);
            FilterImportantPlans();
            DisplayImportantPlans();
        }
    }

    public void RemoveImportantKeyword(string keyword)
    {
        if (importantKeywords.Contains(keyword))
        {
            importantKeywords.Remove(keyword);
            keywordAdder.UpdateKeywordDisplay(importantKeywords);
            FilterImportantPlans();
            DisplayImportantPlans();
        }
    }

    private void FilterImportantPlans()
    {
        importantPlans = plans.Where(plan => plan.IsImportant(importantKeywords)).ToList();
    }

    private void Display()
    {
        planWindow.SetActive(false);
        timetableWindow.SetActive(false);
        switch (displayState)
        {
            case DisplayState.plan:
                DisplayPlans();
                DisplayImportantPlans();
                planWindow.SetActive(true);
                break;
            case DisplayState.timetable:
                DisplayTimetable();
                timetableWindow.SetActive(true);
                break;
        }
    }

    private void DisplayPlans()
    {
        planDisplayer.UpdateKeywordDisplay(plans);
    }

    private void DisplayTimetable()
    {
        timeTableDisplayer.UpdateKeywordDisplay(timetable.Classes);
    }

    private void DisplayImportantPlans()
    {
        importantPlanDisplayer.UpdateKeywordDisplay(importantPlans);
    }
}
