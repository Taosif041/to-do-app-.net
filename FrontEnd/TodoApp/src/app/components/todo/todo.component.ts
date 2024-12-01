import { Component, OnInit } from '@angular/core';
import { TodoItem, TodoServiceService } from 'src/app/Services/todo-service.service';



@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.css']
})

export class TodoComponent implements OnInit{

  todos: TodoItem[]= [];

  newTodo: TodoItem = { 
    id: 0, 
    name: '', 
    isCompleted: false 
  };

  editTodo: TodoItem | null = null;

  constructor(private todoservice: TodoServiceService){}

  ngOnInit(): void {
    this.loadTodos();
  }

  loadTodos(): void {
    this.todoservice.getTodos().subscribe((data) =>{
      this.todos = data;
    });
  }

  addTodo(): void{
    if(this.newTodo.name){
      this.todoservice.postTodo(this.newTodo).subscribe( (data) => {
        this.todos.push(data);
        this.newTodo = { id: 0, name: '', isCompleted: false };
      });
    }
  }

  updateTodo(): void{
    if(this.editTodo){
      this.todoservice.postTodo(this.editTodo).subscribe( (data) => {
        this.editTodo = null;
        this.loadTodos();
      });
    }
  }

  deleteTodo(item: TodoItem): void{
    this.todoservice.deleteTodo(item.id).subscribe(() => {
      this.todos = this.todos.filter(t => t.id != item.id);
    });
  }

}
