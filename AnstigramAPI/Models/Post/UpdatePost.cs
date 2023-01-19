namespace AnstigramAPI.Models.Post
{
    public class UpdatePost
    {
        public string Caption { get; set; }
        public int Id { get; set; }

        public UpdatePost()
        {
        }

        public UpdatePost(string caption, int id)
        {
            Caption = caption;
            Id = id;
        }
    }
}
