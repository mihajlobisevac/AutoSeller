using Application.V1.Posts.Commands;
using Application.V1.Posts.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers.V1
{
    public class PostsController : ApiControllerBase
    {
        [HttpGet("by-modelid/{modelId}")]
        public async Task<IActionResult> GetByModelId(int modelId)
        {
            var posts = await Mediator.Send(new GetPostsByModelId.Query(modelId));

            return posts is not null
                ? Ok(posts)
                : NotFound();
        }

        [HttpGet("by-brandid/{brandId}")]
        public async Task<IActionResult> GetByBrandId(int brandId)
        {
            var posts = await Mediator.Send(new GetPostsByBrandId.Query(brandId));

            return posts is not null
                ? Ok(posts)
                : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePost.Command post)
        {
            var createdPost = await Mediator.Send(post);

            return createdPost > 0
                ? Ok($"Post with Id: {createdPost} successfully created.")
                : BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> EditById([FromBody] EditPost.Command post)
        {
            var editedPost = await Mediator.Send(post);

            return editedPost is not null
                ? Ok(editedPost)
                : BadRequest();
        }

        [HttpPost("recall/{postId}")]
        public async Task<IActionResult> ToggleRecall(int postId)
        {
            var recalledPost = await Mediator.Send(new ToggleRecallPost.Command(postId));

            return recalledPost > 0
                ? Ok($"Successfully toggled recall for Post with Id: {postId}.")
                : BadRequest();
        }

        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var deletedPost = await Mediator.Send(new DeletePost.Command(postId));

            return deletedPost > 0
                ? Ok($"Successfully deleted Post with Id: {postId}.")
                : BadRequest();
        }
    }
}
