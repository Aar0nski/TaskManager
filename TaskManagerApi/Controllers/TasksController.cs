using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.DTOs;
using TaskManagerApi.Services;

namespace TaskManagerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public ActionResult<List<TaskDto>> GetAll()
        {
            return Ok(_taskService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<TaskDto> GetById(int id)
        {
            var task = _taskService.GetById(id);

            if (task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpPost]
        public ActionResult<TaskDto> Create(CreateTaskDto dto)
        {
            var createdTask = _taskService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateTaskDto dto)
        {
            var updated = _taskService.Update(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _taskService.Delete(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}