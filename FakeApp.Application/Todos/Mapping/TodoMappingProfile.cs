using AutoMapper;
using FakeApp.Application.Todos.Commands;
using FakeApp.Domain.Entities;



namespace FakeApp.Application.Todos.Mapping
{
    /// <summary>
    /// Правила маппинга для задач юзеров
    /// </summary>
    public class TodoMappingProfile : Profile
    {
        public TodoMappingProfile()
        {
            CreateMap<Todo, TodoResponse>();
            
            CreateMap<TodoAddCommand, Todo>();
        }
    }
}