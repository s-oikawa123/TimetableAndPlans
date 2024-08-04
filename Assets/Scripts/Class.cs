using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Class
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Day { get; set; }
    public string Period { get; set; }
    public string Room { get; set; }
    public string Notes { get; set; }

    public Class(string id, string name, string day, string period)
    {
        Id = id;
        Name = name;
        Day = day;
        Period = period;
    }

    public void UpdateInfo(Class classInfo)
    {
        Room = classInfo.Room;
        Notes = classInfo.Notes;
    }

    public void UpdateRoom(string newRoom)
    {
        Room = newRoom;
    }

    public void AddNotes(string notes)
    {
        Notes = notes;
    }

    public void HighlightChange()
    {
        // Logic to highlight changes
    }
}
