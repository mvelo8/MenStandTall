using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Wilproject.Pages.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Wilproject.Pages
{
    public class TasksModel : PageModel
    {
        [BindProperty]
        public List<TaskItem> Tasks { get; set; } = new();

        [BindProperty]
        public string NewTaskName { get; set; }

        // Use TempData to persist tasks between requests
        [TempData]
        public string TasksData { get; set; }

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(TasksData))
            {
                Tasks = JsonSerializer.Deserialize<List<TaskItem>>(TasksData);
                TempData.Keep(nameof(TasksData)); //keeps TempData for the next request
            }
            else
            {
                Tasks = new List<TaskItem>
                {
                    new TaskItem { Id = 1, Name = "Sample Task 1", Status = "To Do" },
                    new TaskItem { Id = 2, Name = "Sample Task 2", Status = "Doing" },
                    new TaskItem { Id = 3, Name = "Sample Task 3", Status = "Done" }
                };
                TasksData = JsonSerializer.Serialize(Tasks);
            }
        }

        public IActionResult OnPostAddTask()
        {
            if (!string.IsNullOrEmpty(TasksData))
                Tasks = JsonSerializer.Deserialize<List<TaskItem>>(TasksData);

            if (!string.IsNullOrWhiteSpace(NewTaskName))
            {
                int nextId = Tasks.Any() ? Tasks.Max(t => t.Id) + 1 : 1;
                Tasks.Add(new TaskItem
                {
                    Id = nextId,
                    Name = NewTaskName,
                    Status = "To Do"
                });
            }
            TasksData = JsonSerializer.Serialize(Tasks);
            return RedirectToPage();
        }

        public IActionResult OnPostMoveTask(int id, string newStatus)
        {
            if (!string.IsNullOrEmpty(TasksData))
                Tasks = JsonSerializer.Deserialize<List<TaskItem>>(TasksData);

            var task = Tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.Status = newStatus;
            }
            TasksData = JsonSerializer.Serialize(Tasks);
            return RedirectToPage();
        }
    }
}
