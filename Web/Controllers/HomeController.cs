using Microsoft.AspNetCore.Mvc;
using NTierTodoApp.Business;
using NTierTodoApp.Models;

namespace NTierTodoApp.Web.Controllers
{
    public class TasksController : Controller
    {
        private readonly TaskService _taskService;

        // حقن الخدمة (Dependency Injection) للوصول لطبقة البزنس
        public TasksController(TaskService taskService)
        {
            _taskService = taskService;
        }

        // عرض المهام
        public IActionResult Index()
        {
            var tasks = _taskService.GetTasks();
            return View(tasks);
        }

        // إضافة مهمة
        [HttpPost]
        public IActionResult Add(string title)
        {
            _taskService.AddTask(title);
            return RedirectToAction("Index");
        }

        // إكمال مهمة
        [HttpPost]
        public IActionResult Complete(int id)
        {
            _taskService.CompleteTask(id);
            return RedirectToAction("Index");
        }

        //  التعديل الجديد: استقبال طلب الحذف من الواجهة وتمريره لطبقة البزنس
        [HttpPost]
        public IActionResult Delete(int id)
        {
            // استدعاء الدالة التي قمنا بإنشائها في الـ TaskService
            _taskService.DeleteTask(id);

            // إعادة توجيه المستخدم لتحديث القائمة بعد الحذف
            return RedirectToAction("Index");
        }
    }
}