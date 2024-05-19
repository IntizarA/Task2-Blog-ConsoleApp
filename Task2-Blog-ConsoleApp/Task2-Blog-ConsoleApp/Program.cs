using Task2_Blog_ConsoleApp.Controllers;
using Task2_Blog_ConsoleApp.Models;

class Program
{
    static void Main(string[] args)
    {
        var blogController = new BlogController();

        while (true)
        {
            PrintMenuOptions();

            if (!int.TryParse(Console.ReadLine(), out int option))
            {
                Console.WriteLine("Invalid option. Please enter a number.");
                continue;
            }

            switch (option)
            {
                case 1:
                    CreateBlogPost(blogController);
                    break;
                case 2:
                    blogController.ListAllBlogPosts();
                    break;
                case 3:
                    DisplayBlogPost(blogController);
                    break;
                case 4:
                    SearchBlogPosts(blogController);
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid operation");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void PrintMenuOptions()
    {
        Console.WriteLine("1. Create Blog Post");
        Console.WriteLine("2. List All Blog Posts");
        Console.WriteLine("3. Display Blog Post");
        Console.WriteLine("4. Search Blog Posts");
        Console.WriteLine("5. Exit");
        Console.Write("Choose an option: ");
    }

    static void CreateBlogPost(BlogController blogController)
    {
        Console.Write("Enter Title: ");
        string? title = Console.ReadLine();

        Console.Write("Enter Content: ");
        string? content = Console.ReadLine();

        Console.Write("Enter tags (comma-separated): ");
        string? tags = Console.ReadLine();

        blogController.CreateBlogPost(title, content, tags);
    }

    static void DisplayBlogPost(BlogController blogController)
    {
        Console.Write("Enter blog post Id: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid blog post Id.");
            return;
        }

        Blog? foundBlog = blogController.DisplayBlogPostWithId(id);
        if (foundBlog == null)
        {
            Console.WriteLine("Blog post not found.");
            return;
        }

        Console.WriteLine("Selected Blog:\n");
        Console.WriteLine($"Id: {foundBlog.Id}\nTitle: {foundBlog.Title}\nContent: {foundBlog.Content}\nTags: {string.Join(", ", foundBlog.Tags)}");
    }

    static void SearchBlogPosts(BlogController blogController)
    {
        Console.Write("Enter keyword: ");
        string? keyword = Console.ReadLine();

        List<Blog> blogs = blogController.SearchBlogPostWithKey(keyword);
        if (blogs == null || blogs.Count == 0)
        {
            Console.WriteLine("No matching blogs found.");
            return;
        }

        Console.WriteLine($"Selected Blogs:\n ");
        foreach (Blog blog in blogs)
        {
            Console.WriteLine($"Id: {blog.Id}\nTitle: {blog.Title}\nContent: {blog.Content}\nTags: {string.Join(", ", blog.Tags)}");
        }
    }
}