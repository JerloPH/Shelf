
namespace Shelf.Entity
{
    public class StatusCount
    {
        public long Total { get; set; } = 0;
        public long Current { get; set; } = 0;
        public long Complete { get; set; } = 0;
        public long Onhold { get; set; } = 0;
        public long Drop { get; set; } = 0;
        public long Plan { get; set; } = 0;
    }
}
