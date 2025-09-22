namespace Wilproject.Pages.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; } // "To Do", "Doing", "Done"
    }
}