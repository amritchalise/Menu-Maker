using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MenuMaker_To_Do_App.Models;

namespace MenuMaker_To_Do_App.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    private static List<todo_model> _todoList = new List<todo_model>();

    public IActionResult Index()
    {
        var incompleteTasks = _todoList.Where(item => !item.IsCompleted).ToList();
        var completeTasks = _todoList.Where(item => item.IsCompleted).ToList();

        

        ViewBag.IncompleteTasks = incompleteTasks;
        ViewBag.CompleteTasks = completeTasks;

        return View();
    }

    [HttpPost]
    public IActionResult Add(string task, DateTime dueDate)
    {
        _todoList.Add(new todo_model { Id = _todoList.Count + 1, Task = task, DueDate = dueDate, IsCompleted = false });
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult MarkAsComplete(int id)
    {
        var todoItem = _todoList.FirstOrDefault(item => item.Id == id);
        if (todoItem != null)
        {
            todoItem.IsCompleted = true;
            todoItem.CompletedDate = DateTime.Now;
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var todoItem = _todoList.FirstOrDefault(item => item.Id == id);
        if (todoItem != null)
        {
            _todoList.Remove(todoItem);
        }
        return RedirectToAction("Index");
    }

    public IActionResult AddTask()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

