namespace ConsoleApp1;

public class User
{
    public string Id { get; set; }
    public string FullName { get; set; }
}

public class UserResult
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public List<string> ImageIds { get; set; }
}
public class Image
{
    public string Id { get; set; }
    public List<string> UserIds { get; set; }
    public string Label { get; set; }
}

public static class UserImages
{
    private static Task<List<User>> GetUsersAsync()
    {
        var users = new List<User>
        {
            new()
            {
                Id = "User1",
                FullName = "Name1"
            },
            new()
            {
                Id = "User2",
                FullName = "Name2"
            }
        };
        return Task.FromResult(users);
    }

    private static Task<List<Image>> GetImagesAsync()
    {
        var images = new List<Image>
        {
            new()
            {
                Id = "ImageA",
                UserIds = new List<string>
                {
                    "User1",
                    "User2"
                },
                Label = "LabelA"
            },
            new()
            {
                Id = "ImageB",
                UserIds = new List<string>
                {
                    "User1"
                },
                Label = "LabelB"
            },
            new()
            {
                Id = "ImageC",
                UserIds = new List<string>
                {
                    "User2"
                },
                Label = "LabelC"
            }
        };
        return Task.FromResult<List<Image>>(images);
    }

    private static List<User> GetInput(IReadOnlyCollection<User> users)
    {
        string? input;
        var userList = new List<User>();

        Console.WriteLine("Enter user names (Hit enter twice to end):");
        while (!string.IsNullOrEmpty(input = Console.ReadLine()))
        {
            if (string.IsNullOrWhiteSpace(input)) continue;
            var user = users.FirstOrDefault(x => input.ToLower().Equals(x.FullName.ToLower()));

            if (user == null) continue;
            userList.Add(user);
        }

        return userList.Distinct().ToList();
    }

    private static List<Tuple<string, string>> GetFlatList(List<User> users, List<Image> images)
    {
        var flatList = new List<Tuple<string, string>>();
        foreach (var img in images)
        {
            flatList.AddRange(img.UserIds.Select(userId => new Tuple<string, string>(userId, img.Id)));
        }

        return flatList;
    }

    private static List<UserResult> GetResult(List<User> userList, IReadOnlyCollection<Tuple<string, string>> userImagesFlatList)
    {
        var result = new List<UserResult>();

        foreach (var user in userList)
        {
            var userImages = userImagesFlatList.Where(x => x.Item1 == user.Id).ToList();

            if(userImages.Count == 0) continue;
            
            var userResult = new UserResult()
            {
                Id = user.Id,
                FullName = user.FullName,
                ImageIds = userImages.Select(x => x.Item2).ToList()
            };
            result.Add(userResult);
        }

        return result;
    }

    public static async void GetUserImages()
    {
        var users = await GetUsersAsync();
        var images = await GetImagesAsync();

        var userList = GetInput(users);

        if (userList.Count == 0) Console.WriteLine("None of the users exist");

        var userImagesFlatList = GetFlatList(users, images);

        var result = GetResult(userList, userImagesFlatList);

        if(result.Count == 0) Console.WriteLine("No records found");
        foreach (var res in result)
        {
            Console.Write("User: {0},", res.FullName);
            Console.Write(" Images: ");
            foreach (var img in res.ImageIds)
            {
                Console.Write("{0} ", img);
            }
            Console.WriteLine();
        }
    }
}