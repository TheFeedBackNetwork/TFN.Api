﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TFN.Api.Authorization.Models.Resource;
using TFN.Api.Authorization.Operations;
using TFN.Api.Controllers.Base;
using TFN.Api.Models.InputModels;
using TFN.Api.Models.ModelBinders;
using TFN.Api.Models.QueryModels;
using TFN.Api.Models.ResponseModels;
using TFN.Api.Extensions;
using TFN.Api.Models.Interfaces;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.Enums;
using TFN.Domain.Models.ValueObjects;
using TFN.Mvc.HttpResults;

namespace TFN.Api.Controllers
{
    
    [Route("posts")]
    public class PostController : AppController
    {
        public IPostService PostService { get; private set; }
        public ICreditService CreditService { get; private set; }
        public IPostResponseModelFactory PostResponseModelFactory { get; private set; }
        public IPostSummaryResponseModelFactory PostSummaryResponseModelFactory { get; private set; }
        public ICommentResponseModelFactory CommentResponseModelFactory { get; private set; }
        public ICommentSummaryResponseModelFactory CommentSummaryResponseModelFactory { get; private set; }
        public IAuthorizationService AuthorizationService { get; private set; }
        
        public PostController(IPostService postService, IAuthorizationService authorizationService, ICreditService creditService, IPostResponseModelFactory postResponseModelFactory, IPostSummaryResponseModelFactory postSummaryResponseModelFactory, ICommentResponseModelFactory commentResponseModelFactory, ICommentSummaryResponseModelFactory commentSummaryResponseModelFactory) 
        {
            PostService = postService;
            PostResponseModelFactory = postResponseModelFactory;
            PostSummaryResponseModelFactory = postSummaryResponseModelFactory;
            CommentResponseModelFactory = commentResponseModelFactory;
            CommentSummaryResponseModelFactory = commentSummaryResponseModelFactory;
            AuthorizationService = authorizationService;
            CreditService = creditService;
        }

        [HttpGet(Name = "GetAllPosts")]
        [Authorize("posts.read")]
        public async Task<IActionResult> GetAll(
            [FromQuery]ExcludeQueryModel exclude,
            [ModelBinder(BinderType = typeof(ContinuationTokenModelBinder))]string continuationToken = null)
        {
            var posts = await PostService.FindAllPostsPaginated(continuationToken);
            
            var model = new List<PostResponseModel>();
            foreach (var post in posts)
            {
                model.Add(await PostResponseModelFactory.From(post,AbsoluteUri));
            }

            if (exclude != null)
            {
                return this.Json(model,exclude.Attributes);
            }
            return Json(model);
        }

        [HttpGet("{postId:Guid}", Name = "GetPost")]
        [Authorize("posts.read")]
        public async Task<IActionResult> GetPost(
            Guid postId,
            [FromQuery]ExcludeQueryModel exclude)
        {
            var post = await PostService.FindPost(postId);

            if (post == null)
            {
                return NotFound();
            }
            
            var model = await PostResponseModelFactory.From(post, AbsoluteUri);


            if (exclude != null)
            {
                return this.Json(model, exclude.Attributes);
            }
            return Json(model);
        }

        [HttpGet("{postId:Guid}/likes", Name = "GetPostSummary")]
        [Authorize("posts.read")]
        public async Task<IActionResult> GetPostLikesSummary(
            Guid postId,
            [ModelBinder(BinderType = typeof(LimitQueryModelBinder))]int limit = 7)
        {
            var post = await PostService.FindPost(postId);
            if (post == null)
            {
                return NotFound();
            }

            var postSummary = await PostService.FindPostLikeSummary(postId, limit, Username);
            if (postSummary == null)
            {
                return NotFound();
            }

            var credits = await CreditService.GetByUserIdAsync(post.UserId);

            var model = PostSummaryResponseModelFactory.From(postSummary, credits, AbsoluteUri);

            return Json(model);
        }

        [HttpGet("{postId:Guid}/comments/{commentId:Guid}", Name = "GetComment")]
        [Authorize("posts.read")]
        public async Task<IActionResult> GetComment(
            Guid postId,
            Guid commentId,
            [FromQuery]ExcludeQueryModel exclude)
        {
            var comment = await PostService.FindComment(postId, commentId);

            if (comment == null)
            {
                return NotFound();
            }

            var model = await CommentResponseModelFactory.From(comment, AbsoluteUri);
            
            if (exclude != null)
            {
                return this.Json(model, exclude.Attributes);
            }

            return Json(model);
        }

        [HttpGet("{postId:Guid}/comments", Name = "GetComments")]
        [Authorize("posts.read")]
        public async Task<IActionResult> GetComments(
            Guid postId,
            [FromQuery]ExcludeQueryModel exclude,
            [ModelBinder(BinderType = typeof(ContinuationTokenModelBinder))]string continuationToken = null)
        {
            var comments = await PostService.FindComments(postId, continuationToken);

            if (comments == null)
            {
                return NotFound();
            }

            var summaries = new List<CommentSummary>();
            foreach (var comment in comments)
            {
                var summary = await PostService.FindCommentScoreSummary(comment.Id, 5, Username);
                summaries.Add(summary);
            }

            var model = new List<CommentResponseModel>();
            foreach (var comment in comments)
            {
                model.Add(await CommentResponseModelFactory.From(comment,AbsoluteUri));
            }


            if (exclude != null)
            {
                return this.Json(model, exclude.Attributes);
            }

            return Json(model);
        }

        [HttpGet("{postId:Guid}/comments/{commentId:Guid}/scores", Name = "GetCommentSummary")]
        [Authorize("posts.read")]
        public async Task<IActionResult> GetCommentScoreSummary(
            Guid postId,
            Guid commentId,
            [ModelBinder(BinderType = typeof(LimitQueryModelBinder))]int limit = 7)
        {
            var comment = await PostService.FindComment(postId,commentId);
            if (comment == null)
            {
                return NotFound();
            }

            var commentSummary = await PostService.FindCommentScoreSummary(commentId, limit, Username);
            if (commentSummary == null)
            {
                return NotFound();
            }
            var credits = await CreditService.GetByUserIdAsync(comment.UserId);
            
            var model = CommentSummaryResponseModelFactory.From(commentSummary, credits, postId, AbsoluteUri);

            return Json(model);
        }

        [HttpGet("{postId:Guid}/comments/{commentId:Guid}/scores/{scoreId:Guid}", Name = "GetScore")]
        [Authorize("posts.read")]
        public async Task<IActionResult> GetScore(Guid postId, Guid commentId, Guid scoreId)
        {
            var score = await PostService.FindScore(commentId, scoreId);

            if (score == null)
            {
                return NotFound();
            }

            var model = ScoreResponseModel.From(score, AbsoluteUri, postId);

            return Json(model);
        }

        [HttpPost(Name = "PostPost")]
        [Authorize("posts.write")]
        public async Task<IActionResult> PostPost([FromBody]PostInputModel post)
        {
            var genre = Genre.Other;
            var parsed = Enum.TryParse(post.Genre.ToString(), out genre);


            if (!parsed)
            {
                return BadRequest();
            }
            var entity = new Post(UserId, Username, post.TrackUrl, post.Text,genre,post.Tags);

            var credits = await CreditService.GetByUserIdAsync(UserId);
            if (credits == null)
            {
                //something really wrong happened
                return new HttpBadRequestResult("No credits resource for the account please contact support");
            }

            var creditAuthZModel = CreditsAuthorizationModel.From(credits);

            if (!await AuthorizationService.AuthorizeAsync(User, creditAuthZModel, CreditsOperations.Delete))
            {
                return new HttpForbiddenResult("An attempt to use up credits was attempted, but the authorization policy challenged the request");
            }

            var authZModel = PostAuthorizationModel.From(entity);

            if (!await AuthorizationService.AuthorizeAsync(User, authZModel, PostOperations.Write))
            {
                return new HttpForbiddenResult("A POST request for adding a new post resource was attempted, but the authorization policy challenged the request.");
            }

            await PostService.Add(entity);
            await CreditService.ReduceCredits(credits, 5);

            var model = await PostResponseModelFactory.From(entity, AbsoluteUri);
            
            return CreatedAtAction("GetPost", new {postId = model.Id}, model);
        }

        [HttpPost("{postId:Guid}/comments", Name = "PostComment")]
        [Authorize("posts.write")]
        public async Task<IActionResult> PostComment(
            Guid postId,
            [FromBody]CommentInputModel comment)
        {
            var post = await PostService.FindPost(postId);

            if (post == null)
            {
                return NotFound();
            }

            var entity = new Comment(UserId,postId,Username,comment.Text);

            var authZModel = CommentAuthorizationModel.From(entity);

            if (!await AuthorizationService.AuthorizeAsync(User, authZModel, CommentOperations.Write))
            {
                return new HttpForbiddenResult("A POST request for adding a new post comment resource was attempted, but the authorization policy challenged the request.");
            }

            await PostService.Add(entity);
            
            var model = await CommentResponseModelFactory.From(entity, AbsoluteUri);

            return CreatedAtAction("GetComment", new {postId = model.PostId, commentId = model.Id}, model);
        }

        [HttpPost("{postId:Guid}/comments/{commentId:Guid}/scores", Name = "PostScore")]
        [Authorize("posts.write")]
        public async Task<IActionResult> PostScore(
            Guid postId,
            Guid commentId,
            [ModelBinder(BinderType = typeof(ContinuationTokenModelBinder))]string continuationToken = null)
        {
            var comment = await PostService.FindComment(postId, commentId);

            if (comment == null)
            {
                return NotFound();
            }

            var scores = await PostService.AllScores(commentId, continuationToken);

            if(scores.Any(x => x.UserId == UserId))
            {
                return BadRequest();
            }

            var entity = new Score(commentId, UserId, Username);

            var authZModel = ScoreAuthorizationModel.From(entity, comment.UserId);

            if (!await AuthorizationService.AuthorizeAsync(User, authZModel, ScoreOperations.Write))
            {
                return new HttpForbiddenResult("A POST request for adding a new comment score resource was attempted, but the authorization policy challenged the request.");
            }

            await PostService.Add(entity);
            await CreditService.AwardCredits(UserId, comment.UserId, 1);

            var model = ScoreResponseModel.From(entity, AbsoluteUri, postId);


            return CreatedAtAction("GetScore", new { postId = comment.PostId, commentId = model.CommentId, scoreId = model.Id }, model);
        }

        [HttpPatch("{postId:Guid}", Name = "EditPost")]
        [Authorize("posts.edit")]
        public async Task<IActionResult> PatchPost(
            Guid postId,
            [FromBody]PostInputModel model)
        {
            var post = await PostService.FindPost(postId);

            if (post == null)
            {
                return NotFound();
            }

            if(post.Created > DateTime.UtcNow.Subtract(TimeSpan.FromHours(1)))
            {
                return new HttpBadRequestResult("Post Cannot be edited 1 hour after its creation.");
            }

            var genre = Genre.Other;
            var parsed = Enum.TryParse(post.Genre.ToString(), out genre);

            var authZModel = PostAuthorizationModel.From(post);

            if (!await AuthorizationService.AuthorizeAsync(User, authZModel, PostOperations.Edit))
            {
                return new HttpForbiddenResult("A PATCH request for ammending a post resource was attempted, but the authorization policy challenged the request.");
            }

            var editedPost = Post.EditPost(post, model.Text, model.TrackUrl, model.Tags, genre);

            await PostService.Update(editedPost);

            return NoContent();
        }


        [HttpPatch("{postId:Guid}/comments/{commentId:Guid}", Name = "EditComment")]
        [Authorize("posts.edit")]
        public async Task<IActionResult> PatchComment(
            Guid postId,
            Guid commentId,
            [FromBody]CommentInputModel post)
        {
            var comment = await PostService.FindComment(postId, commentId);

            if (comment == null)
            {
                return NotFound();
            }

            var authZModel = CommentAuthorizationModel.From(comment);

            if (!await AuthorizationService.AuthorizeAsync(User, authZModel, CommentOperations.Edit))
            {
                return new HttpForbiddenResult("A PATCH request for ammending a comment resource was attempted, but the authorization policy challenged the request.");
            }

            await PostService.Update(comment);

            return NoContent();
        }


        [HttpDelete("{postId:Guid}", Name = "DeletePost")]
        public async Task<IActionResult> DeletePost(Guid postId)
        {
            var post = await PostService.FindPost(postId);

            if (post == null)
            {
                return NotFound();
            }

            var authZModel = PostAuthorizationModel.From(post);

            if (!await AuthorizationService.AuthorizeAsync(User, authZModel, PostOperations.Delete))
            {
                return new HttpForbiddenResult("A DELETE request for deleting a post resource was attempted, but the authorization policy challenged the request.");
            }

            await PostService.DeletePost(postId);

            return Ok();
        }

        [HttpDelete("{postId:Guid}/comments/{commentId:Guid}", Name = "DeleteComment")]
        public async Task<IActionResult> DeleteComment(Guid postId, Guid commentId)
        {
            var comment = await PostService.FindComment(postId, commentId);

            if (comment == null)
            {
                return NotFound();
            }

            var authZModel = CommentAuthorizationModel.From(comment);

            if (!await AuthorizationService.AuthorizeAsync(User, authZModel, CommentOperations.Delete))
            {
                return new HttpForbiddenResult("A DELETE request for deleting a post's comment resource was attempted, but the authorization policy challenged the request.");
            }

            await PostService.DeleteComment(commentId);

            return Ok();
        }

        [HttpDelete("{postId:Guid}/comments/{commentId:Guid}/scores/{scoreId:Guid}", Name = "DeleteScore")]
        public async Task<IActionResult> DeleteScore(Guid postId, Guid commentId, Guid scoreId)
        {
            var score = await PostService.FindScore(commentId,scoreId);

            if (score == null)
            {
                return NotFound();
            }

            var authZModel = ScoreAuthorizationModel.From(score,score.CommentId);

            if (!await AuthorizationService.AuthorizeAsync(User, authZModel, ScoreOperations.Delete))
            {
                return new HttpForbiddenResult("A DELETE request for deleting a score resource was attempted, but the authorization policy challenged the request.");
            }

            await PostService.DeleteScore(commentId,scoreId);

            return Ok();
        }
    }
}
