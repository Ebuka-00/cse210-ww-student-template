// Program.cs — YouTube Videos (CSE 210 W04: Abstraction)

List<Video> videos = new List<Video>();

// Video 1
Video v1 = new Video("Building REST APIs with Node.js", "Traversy Media", 3245);
v1.AddComment(new Comment("Emeka Obi",    "Best Node tutorial I've watched, very clear!"));
v1.AddComment(new Comment("Amina Yusuf",  "Finally understood middleware. Thank you!"));
v1.AddComment(new Comment("Tunde Adewale","Could you do a follow-up on authentication?"));
v1.AddComment(new Comment("Grace Nwosu",  "Timestamps would help for this long video."));
videos.Add(v1);

// Video 2
Video v2 = new Video("Python for Data Science – Full Course", "freeCodeCamp", 5400);
v2.AddComment(new Comment("Chidi Okeke",  "Pandas section was incredibly well explained."));
v2.AddComment(new Comment("Fatima Bello", "I went from zero to plotting graphs in one day!"));
v2.AddComment(new Comment("Segun Lawal",  "Please do a machine learning part 2!"));
videos.Add(v2);

// Video 3
Video v3 = new Video("CSS Grid in 20 Minutes", "Kevin Powell", 1187);
v3.AddComment(new Comment("Ngozi Eze",    "I've been struggling with Grid for months, this fixed it."));
v3.AddComment(new Comment("Bola Adesanya","Short, sharp, and straight to the point. Love it."));
v3.AddComment(new Comment("Uche Nwachukwu","The live examples make all the difference."));
videos.Add(v3);

// Video 4
Video v4 = new Video("How Git Works Under the Hood", "Fireship", 980);
v4.AddComment(new Comment("Kemi Adeyemi", "Mind blown. Never knew commits were just SHA hashes."));
v4.AddComment(new Comment("Dayo Okonkwo", "Finally makes sense why rebasing can be destructive."));
v4.AddComment(new Comment("Ifeoma Okafor","100 seconds style but better. More of this please!"));
v4.AddComment(new Comment("Rasheed Musa", "Shared this with my whole dev team, great for onboarding."));
videos.Add(v4);

// Display all videos
foreach (Video video in videos)
{
    Console.WriteLine("========================================");
    Console.WriteLine($"Title:    {video.GetTitle()}");
    Console.WriteLine($"Author:   {video.GetAuthor()}");
    Console.WriteLine($"Length:   {video.GetLength()} seconds");
    Console.WriteLine($"Comments: {video.GetNumberOfComments()}");
    Console.WriteLine("----------------------------------------");

    foreach (Comment comment in video.GetComments())
    {
        Console.WriteLine($"  {comment.GetName()}: {comment.GetText()}");
    }

    Console.WriteLine();
}