namespace Infrastructuur.Model
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string Password { get; set; }
        public List<TeamEntity>? UserTeams { get; set; }

    }
}
