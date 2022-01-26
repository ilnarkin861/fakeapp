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
    /// Класс для работы с постами
    /// </summary>
    public class PostService : BaseService, IPostService
    {
        public PostService(AppDbContext dbContext, IMapper mapper, 
            IUserManager userManager) : base(dbContext, mapper, userManager)
        {
        }


        public async Task<PaginationResponse> GetPostsListAsync(int offset, int limit, int userId)
        {
            var postsLimit = limit is 0 or > Settings.PostsLimit ? Settings.PostsLimit : limit;

            var query = DbContext.Posts.AsNoTracking();
            
            if (userId != 0)
            {
                query = query.Where(p => p.UserId == userId);
            }
            
            var count = await query.CountAsync();

            var posts = await query
                .OrderByDescending(p => p.CreatedDate)
                .Skip(offset)
                .Take(postsLimit)
                .ProjectTo<PostResponse>(Mapper.ConfigurationProvider)
                .ToListAsync();


            return new PaginationResponse
                {
                    Data = posts,
                    Pagination = new Paginator(count, offset, postsLimit)
                };
        }

        
        public async Task<PostResponse> GetPostByIdAsync(int id)
        {
            var post = await DbContext.Posts
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Include(a => a.User)
                .ProjectTo<PostResponse>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (post == null)
            {
                throw new EntityNotFoundException("Post not found");
            }

            return post;
        }
        

        public async Task<PaginationResponse> GetUserPostsListAsync(int offset, int limit, HttpContext httpContext)
        {
            var user = await GetCurrentUser(httpContext);
            
            var postsLimit = limit is 0 or > Settings.UserPostsLimit ? Settings.UserPostsLimit : limit;

            var query = DbContext.Posts.AsNoTracking().Where(p => p.User == user);

            var count = await query.CountAsync();
            
            var posts = await query
                .OrderByDescending(p => p.CreatedDate)
                .Skip(offset)
                .Take(postsLimit)
                .ProjectTo<UserPostResponse>(Mapper.ConfigurationProvider)
                .ToListAsync();

            return new PaginationResponse
                {
                    Data = posts,
                    Pagination = new Paginator(count, offset, postsLimit)
                };
        }

        
        public async Task<UserPostResponse> GetUserPostAsync(int postId, HttpContext httpContext)
        {
            var user = await GetCurrentUser(httpContext);

            var post = await DbContext.Posts
                .AsNoTracking()
                .Where(p => p.Id == postId && p.User == user)
                .ProjectTo<UserPostResponse>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (post == null)
            {
                throw new EntityNotFoundException("User post not found");
            }

            return post;
        }
        

        public async Task<UserPostResponse> AddPostAsync(Post post, HttpContext httpContext)
        {
            var user = await GetCurrentUser(httpContext);

            post.UserId = user.Id;

            DbContext.Add(post);

            await DbContext.SaveChangesAsync();

            return  Mapper.Map<UserPostResponse>(post);
        }

        
        public async Task<UserPostResponse> UpdatePostAsync(Post post, HttpContext httpContext)
        {
            var user = await GetCurrentUser(httpContext);

            var editablePost = await DbContext.Posts
                .FirstOrDefaultAsync(p => p.Id == post.Id && p.User.Id == user.Id);

            if (editablePost == null)
            {
                throw new RestException("Not found user post for update");
            }

            editablePost.Title = post.Title;

            if (post.Image != null)
            {
                editablePost.Image = post.Image;
            }

            DbContext.Update(editablePost);

            await DbContext.SaveChangesAsync();

            return Mapper.Map<UserPostResponse>(post);
        }
        

        public async Task<bool> DeletePostAsync(int id, HttpContext httpContext)
        {
            var user = await GetCurrentUser(httpContext);

            var post = await DbContext.Posts
                .Where(p => p.Id == id && p.User == user)
                .FirstOrDefaultAsync();

            if (post == null)
            {
                throw new RestException("Not found user post for delete");
            }

            DbContext.Remove(post);

            await DbContext.SaveChangesAsync();

            return true;
        }
        

        private async Task<User> GetCurrentUser(HttpContext httpContext)
        {
            return await UserManager.GetCurrentUserAsync(httpContext);
        }
    }
}