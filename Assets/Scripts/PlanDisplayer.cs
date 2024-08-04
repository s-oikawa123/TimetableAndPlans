using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanDisplayer : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private GameObject PlanPrefab;

    public void UpdateKeywordDisplay(IReadOnlyList<Plan> plans)
    {
        int count = rectTransform.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(rectTransform.GetChild(i).gameObject);
        }

        foreach (Plan plan in plans)
        {
            PlanObject planObject = Instantiate(PlanPrefab, rectTransform).GetComponent<PlanObject>();
            planObject.SetUp(plan);
        }
    }
}
