using System.ComponentModel.DataAnnotations.Schema;

namespace WebPR.Models
{
    public class Order
    {
        public int Id { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
        public User User { get; set; }
        
        [ForeignKey("Goods")]
        public int GoodsId { get; set; }
        public Goods Goods { get; set; }
        public int Count { get; set; }
    }
}
