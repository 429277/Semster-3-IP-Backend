namespace AnstigramAPI.Models
{
    public class FollowDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FollowerId { get; set; }
        
        public FollowDTO() { }

        public FollowDTO(int followerId, int userId)
        {
            UserId = userId;
            FollowerId = followerId;
        }
    }
}
