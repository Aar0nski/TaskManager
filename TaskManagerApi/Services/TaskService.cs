using TaskManagerApi.Data;
using TaskManagerApi.DTOs;
using TaskManagerApi.Models;

namespace TaskManagerApi.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public List<TaskDto> GetAll()
        {
            return _context.Tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsDone = t.IsDone
            }).ToList();
        }

        public TaskDto? GetById(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);

            if (task == null) return null;

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsDone = task.IsDone
            };
        }

        public TaskDto Create(CreateTaskDto dto)
        {
            var newTask = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                IsDone = false
            };

            _context.Tasks.Add(newTask);
            _context.SaveChanges();

            return new TaskDto
            {
                Id = newTask.Id,
                Title = newTask.Title,
                Description = newTask.Description,
                IsDone = newTask.IsDone
            };
        }

        public bool Update(int id, UpdateTaskDto dto)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);

            if (task == null) return false;

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.IsDone = dto.IsDone;

            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);

            if (task == null) return false;

            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return true;
        }
    }
}