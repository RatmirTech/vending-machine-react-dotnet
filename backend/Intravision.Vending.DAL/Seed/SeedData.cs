using Intravision.Vending.Core.Models;
using Intravision.Vending.DAL.Context;

namespace Intravision.Vending.DAL.Seed;

public static class SeedData
{
    public static void SeedCoins(EfContext context)
    {
        if (!context.Coins.Any())
        {
            var coins = new List<Coin>
            {
                new Coin { Id = Guid.NewGuid(), Denomination = 100, Quantity = 100 },
                new Coin { Id = Guid.NewGuid(), Denomination = 200, Quantity = 100 },
                new Coin { Id = Guid.NewGuid(), Denomination = 500, Quantity = 100 },
                new Coin { Id = Guid.NewGuid(), Denomination = 1000, Quantity = 100 },
            };

            context.Coins.AddRange(coins);
            context.SaveChanges();
        }
    }
}