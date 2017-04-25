using AsyncDemo.Lib;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AsyncDemo.WebApplication.Controllers
{
    public class DeadlockController : Controller
    {
        public async Task<ActionResult> AsyncActionWithoutDeadlock()
        {
            var result = await SemiSmellyLibrary.GetMessageAsync();
            return Content(result);
        }

        public ActionResult SyncActionWithDeadlock()
        {
            var result = SemiSmellyLibrary.GetMessageAsync().Result;
            return Content(result);
        }

        public ActionResult SyncActionWithoutDeadlock()
        {
            var result = Task.Run(() => SemiSmellyLibrary.GetMessageAsync()).Result;
            return Content(result);
        }

        public ActionResult SyncActionWithoutDeadlockGoodLibrary()
        {
            var result = SemiSmellyLibrary.GetMessageWithoutCapturingContextAsync().Result;
            return Content(result);
        }
    }
}