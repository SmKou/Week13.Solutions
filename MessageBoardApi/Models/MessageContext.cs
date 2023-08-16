using Microsoft.EntityFrameworkCore;

namespace MessageBoardApi.Models;

public class MessageContext : DbContext
{
    public DbSet<Group> Groups { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<User> Users { get; set; }

    public MessageContext(DbContextOptions<MessageContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Group>()
            .HasData(
                new Group { GroupId = 1, Name = "Testing 1.2.3" },
                new Group { GroupId = 2, Name = "Starters" },
                new Group { GroupId = 3, Name = "Builders" },
                new Group { GroupId = 4, Name = "FAQ" }
            );

        builder.Entity<User>()
            .HasData(
                new User { UserId = 1, Name = "Adam Zapel", UserName = "Bob", NormalizedUserName = "bob" },
                new User { UserId = 2, Name = "Anna Conda", UserName = "Long", NormalizedUserName = "long" },
                new User { UserId = 3, Name = "Crystal Ball", UserName = "Crall", NormalizedUserName = "crall" },
                new User { UserId = 4, Name = "Dee Zaster", UserName = "DeeDeeHa", NormalizedUserName = "deedeeha" }
            );

        builder.Entity<Message>()
            .HasData(
                new Message { MessageId = 1, MessageText = "Why do we have so many tests!!", SentAt = DateTime.Now, GroupId = 1, UserId = 1 },
                new Message { MessageId = 2, MessageText = "I've just started a wellness diet.", SentAt = DateTime.Now, GroupId = 2, UserId = 2 },
                new Message { MessageId = 3, MessageText = "Hey Hey all! It's my new lego megatower!", SentAt = DateTime.Now, GroupId = 3, UserId = 3 },
                new Message { MessageId = 4, MessageText = "What do I ask so I don't look dumb?", SentAt = DateTime.Now, GroupId = 4, UserId = 4 },
                new Message { MessageId = 5, MessageText = "Questions like What do I ask, How would I...if I were to..., To what extent does... Before asking, identify the kind of answer you're looking for, what are you assuming, is it right, and google it. Then ask.", SentAt = DateTime.Now, GroupId = 4, UserId = 2 }
            );
    }
}