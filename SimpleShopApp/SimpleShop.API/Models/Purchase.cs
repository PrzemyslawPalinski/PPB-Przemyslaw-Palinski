
namespace SimpleShop.API.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ArticleId { get; set; }
    }
}