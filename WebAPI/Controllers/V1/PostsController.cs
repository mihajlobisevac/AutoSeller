using Application.V1.Posts.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers.V1
{
    public class PostsController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePost.Command post)
        {
            var createdPost = await Mediator.Send(post);

            return createdPost > 0
                ? Ok($"Post with Id: {createdPost} successfully created.")
                : BadRequest();
        }
    }
}
