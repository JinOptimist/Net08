using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff.Model;

namespace WebMazeMvc.EfStuff.Repositories
{
    public class BankCardRepository : BaseRepository<BankCard>
    {
        public BankCardRepository(MazeDbContext dbContext) : base(dbContext)
        {
        }

        public bool Exist(string cardNumber)
        {
            return _dbSet.Any(x => x.CardNumber == cardNumber);
        }
    }
}