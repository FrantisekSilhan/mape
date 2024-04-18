using System.ComponentModel.DataAnnotations.Schema;

namespace LoginProject.Models {
    public class Post {
        public Guid PostId { get; set; } = default!;
        public string Content { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = default!;

        public Guid AuthorId { get; set; } = default!;
        [ForeignKey(nameof(AuthorId))]
        public User Author { get; set; } = default!;

        public Guid? ParentPostId { get; set; } = default!;
        [ForeignKey(nameof(ParentPostId))]
        public Post? ParentPost { get; set; } = default!;

        public Guid? RootPostId { get; set; } = default!;
        [ForeignKey(nameof(RootPostId))]
        public Post? RootPost { get; set; } = default!;

        [NotMapped]
        public int RepliesCount { get; set; } = default!;
    }
}
