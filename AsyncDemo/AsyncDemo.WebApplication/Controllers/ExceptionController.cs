using AsyncDemo.Lib;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AsyncDemo.WebApplication.Controllers
{
    public class ExceptionController : Controller
    {
        public async Task<ActionResult> ShowExceptionThrown()
        {
            await AsyncThrower.DoSthExceptional();
            return Content("This is never sent");
        }

        public async Task<ActionResult> ShowExceptionSwallowed()
        {
            AsyncThrower.DoSthExceptionalInTheVoid();
            return Content("This is actually sent");
        }
    }
}