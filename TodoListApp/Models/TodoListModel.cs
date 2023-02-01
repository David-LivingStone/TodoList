using System;
using System.ComponentModel.DataAnnotations;

namespace TodoListApp.Models
{
    public class TodoListModel
    {
        [Key]
        public int Guid { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserId { get; set; }
    }
}
