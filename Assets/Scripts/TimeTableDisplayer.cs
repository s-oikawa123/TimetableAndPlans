using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Rendering;

public class TimeTableDisplayer : MonoBehaviour
{
    [SerializeField] private ClassObject[] mon;
    [SerializeField] private ClassObject[] tue;
    [SerializeField] private ClassObject[] wed;
    [SerializeField] private ClassObject[] thu;
    [SerializeField] private ClassObject[] fri;
    [SerializeField] private ClassObject[] sat;

    public void UpdateKeywordDisplay(IReadOnlyList<Class> classes)
    {
        foreach (var item in mon)
        {
            item.ResetText();
        }
        foreach (var item in tue)
        {
            item.ResetText();
        }
        foreach (var item in wed)
        {
            item.ResetText();
        }
        foreach (var item in thu)
        {
            item.ResetText();
        }
        foreach (var item in fri)
        {
            item.ResetText();
        }
        foreach (var item in sat)
        {
            item.ResetText();
        }

        foreach (Class c in classes)
        {
            switch (c.Day)
            {
                case "��":
                    mon[int.Parse(c.Period) - 1].SetUp(c);
                    break;
                case "��":
                    tue[int.Parse(c.Period) - 1].SetUp(c);
                    break;
                case "��":
                    wed[int.Parse(c.Period) - 1].SetUp(c);
                    break;
                case "��":
                    thu[int.Parse(c.Period) - 1].SetUp(c);
                    break;
                case "��":
                    fri[int.Parse(c.Period) - 1].SetUp(c);
                    break;
                case "�y":
                    sat[int.Parse(c.Period) - 1].SetUp(c);
                    break;
            }
        }
    }
}
