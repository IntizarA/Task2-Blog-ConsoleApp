using System;
using System.Collections.Generic;
using System.Linq;
using Task2_Blog_ConsoleApp.Models;
using Task2_Blog_ConsoleApp.Services;
using Task2_Blog_ConsoleApp.Views;

namespace Task2_Blog_ConsoleApp.Controllers
{
    public class BlogController
    {
        private readonly BlogFileService _blogFileService;
        private List<Blog> _blogs;
        private readonly Dictionary<string, HashSet<int>> _index = new();

        public BlogController()
        {
            _blogFileService = new BlogFileService();
            _blogs = _blogFileService.LoadBlogPosts();
            InitializeIndex();
        }

        public void CreateBlogPost(string title, string content, string tags)
        {
            Blog blog = new Blog()
            {
                Id = _blogs.Count + 1,
                Title = title,
                Content = content,
                Tags = tags.Split(',')
            };

            _blogs.Add(blog);
            _blogFileService.SaveBlogPosts(_blogs);
            AddToIndex(blog);
        }

        public void ListAllBlogPosts()
        {
            BlogView.DisplayBlogPosts(_blogs);
        }

        public Blog DisplayBlogPostWithId(int id)
        {
            return _blogs.FirstOrDefault(x => x.Id == id);
        }

        public List<Blog> SearchBlogPostWithKey(string keyword)
        {
            string lowerKeyword = keyword.ToLower();
            if (_index.TryGetValue(lowerKeyword, out HashSet<int> blogIds))
            {
                return _blogs.Where(blog => blogIds.Contains(blog.Id)).ToList();
            }
            return new List<Blog>();
        }

        private void InitializeIndex()
        {
            foreach (var blog in _blogs ?? Enumerable.Empty<Blog>())
            {
                AddToIndex(blog);
            }
        }

        private void AddToIndex(Blog blog)
        {
            string[] contentWords = blog.Content.ToLower().Split(new[] { ' ', '.', ',', ';', ':', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in contentWords)
            {
                AddWordToIndex(word, blog.Id);
            }

            string[] tagWords = blog.Tags.Select(tag => tag.ToLower().Trim()).ToArray();

            foreach (string word in tagWords)
            {
                AddWordToIndex(word, blog.Id);
            }
        }

        private void AddWordToIndex(string word, int blogId)
        {
            if (!_index.ContainsKey(word))
            {
                _index[word] = new HashSet<int>();
            }
            _index[word].Add(blogId);
        }
    }
}
