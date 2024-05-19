using Newtonsoft.Json;
using Task2_Blog_ConsoleApp.Models;

namespace Task2_Blog_ConsoleApp.Services
{
    public class BlogFileService
    {
        public readonly string filePath = "../blogs.json";

        public void SaveBlogPosts(List<Blog> blogs)
        {
            string json = JsonConvert.SerializeObject(blogs, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public List<Blog> LoadBlogPosts()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    return JsonConvert.DeserializeObject<List<Blog>>(json);
                }
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"JSON Deserialization error: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading blog posts: {ex.Message}");
            }

            return new();
        }
    }
}
