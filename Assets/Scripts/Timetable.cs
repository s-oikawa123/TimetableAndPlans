using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Timetable
{
    public List<Class> Classes { get; set; } = new List<Class>();

    public void UpdateClassInfo(Class classInfo)
    {
        var classToUpdate = Classes.Find(c => c.Id == classInfo.Id);
        if (classToUpdate != null)
        {
            classToUpdate.UpdateInfo(classInfo);
        }
    }

    public void AddClassNotes(string classId, string notes)
    {
        var classToUpdate = Classes.Find(c => c.Id == classId);
        if (classToUpdate != null)
        {
            classToUpdate.AddNotes(notes);
        }
    }
}
