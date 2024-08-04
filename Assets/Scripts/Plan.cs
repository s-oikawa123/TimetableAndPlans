using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Plan
{
    public string Id { get; set; }
    public string From { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public DateTime Deadline { get; set; }

    public Plan(string id, string from, string title, string description, DateTime date)
    {
        Id = id;
        From = from;
        Title = title;
        Description = description;
        Date = date;
        Deadline = date.AddMonths(1);
    }

    public bool IsImportant(List<string> importantKeywords)
    {
        foreach (var keyword in importantKeywords)
        {
            if (Title.Contains(keyword) || Description.Contains(keyword))
            {
                return true;
            }
        }
        return false;
    }

    public bool IsOverdue()
    {
        return DateTime.Now > Deadline;
    }

    public string CheckStatus(List<string> importantKeywords)
    {
        if (IsImportant(importantKeywords))
        {
            return "Important";
        }
        else if (IsOverdue())
        {
            return "Overdue";
        }
        else
        {
            return "New";
        }
    }
}
