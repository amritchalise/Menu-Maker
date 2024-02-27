using System;
namespace MenuMaker_To_Do_App.Models
{
	public class todo_model
	{
        public int Id { get; set; }
        public string Task { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}

