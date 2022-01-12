using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FakeApp.Application.Services;
using FakeApp.Domain.Entities;
using FakeApp.Infrastructure;
using FakeApp.Infrastructure.Data;
using FakeApp.Infrastructure.DTO;
using FakeApp.Infrastructure.Exceptions;
using FakeApp.Infrastructure.Helpers;
using FakeApp.Infrastructure.Interfaces;



namespace FakeApp.Application.Posts.Services
{
    /// <summary>
    /// Класс для работы с комментами
    /// </summary>
    public class CommentService : BaseService, ICommentService
    {
        public CommentService(AppDbContext dbContext, IMapper mapper, 
            IUserManager userManager) : base(dbContext, mapper, userManager)
        {
        }
        
        
        public async Task<PaginationResponse> GetCommentsListAsync(int postId, int offset, int limit)
        {
            var commentsLimit = limit is 0 or > Settings.CommentsLimit ? Settings.CommentsLimit : limit;

            var query = DbContext.Comments.AsNoTracking().Where(c => c.PostId == postId);
            
            var count = await query.CountAsync();
            
            var comments = await query
                .OrderByDescending(c => c.CommentDate)
                .Skip(offset)
                .Take(commentsLimit)
                .ProjectTo<CommentResponse>(Mapper.ConfigurationProvider)
                .ToListAsync();

            return new PaginationResponse
                {
                    Data = comments,
                    Pagination = new Paginator(count, offset, commentsLimit)
                };
        }
        

        public async Task<CommentResponse> AddCommentAsync(Comment comment, HttpContext httpContext)
        {
            var postExists = await DbContext.Posts.AnyAsync(p => p.Id == comment.PostId);

            if (!postExists)
            {
                throw new RestException("Not found post for add comment");
            }
                
            var user = await UserManager.GetCurrentUserAsync(httpContext);

            comment.UserId = user.Id;

            DbContext.Add(comment);

            await DbContext.SaveChangesAsync();

            await DbContext.Entry(comment).Reference(c => c.User).LoadAsync();
            
            return Mapper.Map<CommentResponse>(comment);
        }
    }
}