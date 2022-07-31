using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace stock
{
    public static class Program
    {
        public static void Main()
        {

            Stock stock = new();



            new Bank("Sberbank", stock);
            new Bank("Tinkoff", stock);

            new Broker("OAO Vasya", stock);
            new Broker("OAO Broker", stock);

            stock.AddAsset(0, "Dollar", 62);
            stock.AddAsset(1, "Euro", 63);

            stock.StartBargaining();
        }
    }
}