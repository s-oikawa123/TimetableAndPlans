using System.Collections;
using System.Collections.Generic;

public class Class
{
    public string Id { get; set; }
    public string Day { get; set; }
    public string Period { get; set; }
    public string Room { get; set; }
    public string Notes { get; set; }

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
