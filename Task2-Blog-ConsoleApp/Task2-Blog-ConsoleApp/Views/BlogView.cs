using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2_Blog_ConsoleApp.Models;

namespace Task2_Blog_ConsoleApp.Views
{
    public class BlogView
    {
        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static void DisplayBlogPosts(List<Blog> blogPosts)
        {
            Console.WriteLine("List of all blogs:");
            foreach (var blog in blogPosts)
            {
                Console.WriteLine($"Id: {blog.Id}\nTitle: {blog.Title}\nContent: {blog.Content}\nTags: {string.Join(", ", blog.Tags)}");
            }
        }

    }
}
