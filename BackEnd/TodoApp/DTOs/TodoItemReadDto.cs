﻿namespace TodoApp.DTOs
{
    public class TodoItemReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsCompleted { get; set; }
    }
}
