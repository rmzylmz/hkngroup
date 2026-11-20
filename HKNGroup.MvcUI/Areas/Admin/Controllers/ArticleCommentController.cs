using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.Areas.Admin.Controllers
{
   
    [Authorize]
    [Area("Admin")]
    public class ArticleCommentController : Controller
    {
        private readonly IArticleCommentService _commentService;

        public ArticleCommentController(IArticleCommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IActionResult> Index()
        {
            //var comments = await _commentService.GetAll();
            var comments = await _commentService.GetAllByNonDeleteAndActiveSite();
            if (comments.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            if (comments.ResultStatus == ResultStatus.Success)
            {
                if (comments.Data?.Comments != null)
                {
                    comments.Data.Comments = comments.Data.Comments.OrderByDescending(x => x.Id).ToList();
                }
                return View(comments.Data);
            }
            return NotFound();
        }

        public async Task<IActionResult> CheckIt(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }
            var comment = await _commentService.GetUpdateDto(id);
            if (comment.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            if (comment.ResultStatus == ResultStatus.Success)
            {
                if (comment.Data.IsActive == false)
                {
                    await _commentService.DoActive(id, "Ramazan YILMAZ");
                }
                return RedirectToAction("Index");
            }
            return NotFound();
        }

  

        public async Task<IActionResult> DoHardDelete(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }
            await _commentService.HardDelete(id);
            return Json(new { success = true });
        }
    }
}
