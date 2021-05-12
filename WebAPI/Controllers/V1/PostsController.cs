using Application.V1.Posts.Commands;
using Application.V1.Posts.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers.V1
{
    [Authorize]
    public class PostsController : ApiControllerBase
    {
        [AllowAnonymous]
        [HttpGet("{postId}")]
        public async Task<IActionResult> Get(int postId)
        {
            var post = await Mediator.Send(new GetPost.Query(postId));

            return post is not null
                ? Ok(post)
                : NotFound();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetPosts.Query query)
        {
            var post = await Mediator.Send(query);

            return post is not null
                ? Ok(post)
                : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePost.Command post)
        {
            var createdPost = await Mediator.Send(post);

            return createdPost.IsSuccessful
                ? Ok($"Post with Id: {createdPost.Id} successfully created.")
                : BadRequest(createdPost.Errors);
        }

        [HttpPut]
        public async Task<IActionResult> EditById([FromBody] EditPost.Command post)
        {
            var editedPost = await Mediator.Send(post);

            return editedPost.IsSuccessful
                ? Ok(editedPost)
                : BadRequest(editedPost.Errors);
        }

        [HttpPost("recall/{postId}")]
        public async Task<IActionResult> ToggleRecall(int postId)
        {
            var recalledPost = await Mediator.Send(new ToggleRecallPost.Command(postId));

            return recalledPost.IsSuccessful
                ? Ok($"Successfully toggled recall for Post with Id: {postId}.")
                : BadRequest(recalledPost.Errors);
        }

        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var deletedPost = await Mediator.Send(new DeletePost.Command(postId));

            return deletedPost.IsSuccessful
                ? Ok($"Successfully deleted Post with Id: {postId}.")
                : BadRequest(deletedPost.Errors);
        }
    }
}
