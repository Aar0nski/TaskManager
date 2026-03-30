using TaskManagerApi.DTOs;

namespace TaskManagerApi.Services
{
    public interface ITaskService
    {
        List<TaskDto> GetAll();
        TaskDto? GetById(int id);
        TaskDto Create(CreateTaskDto dto);
        bool Update(int id, UpdateTaskDto dto);
        bool Delete(int id);
    }
}