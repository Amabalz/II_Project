namespace Project_II.Server.Models
{
    public class Comment
    {
        public int id {  get; set; }
        public int ticketid { get; set; }
        public int userid { get; set; }
        public string data { get; set; }
        public int likes { get; set; }
        public int dislikes { get; set; }
    }
}
