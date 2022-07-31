using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock
{

    /// <summary>
    /// класс банка
    /// </summary>
    public class Bank
    {

        /// <summary>
        /// имя банка
        /// </summary>
        public string Name { get; private set; }


        /// <summary>
        /// словарь, хранящий в себе имя актива и оптимальную для него цену
        /// ключ - имя актива
        /// значение - оптимальная цена актива
        /// </summary>
        public Dictionary<string, decimal> optimalPrice = new Dictionary<string, decimal>();

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="name">имя банка</param>
        /// <param name="stock">биржа</param>
        public Bank(string name, Stock stock)
        {
            Name = name;
            SubscribeToStock(stock);
        }



        /// <summary>
        /// подписка на биржу
        /// </summary>
        /// <param name="stock">биржа</param>
        private void SubscribeToStock(Stock stock)
        {
            stock.PriceChanged += PriceChangeReaction;
            stock.AssetAdded += NewAssetReaction;
            stock.AssetChangeCondition += ChangeConditionReaction;

            optimalPrice = new Dictionary<string, decimal>();

            foreach (var asset in stock.assets)
            {
                NewAssetReaction(asset.Name, asset.Price);
            }
        }


        /// <summary>
        /// реакция банка  на изменение состояния актива 
        /// </summary>
        /// <param name="assetName">имя актива</param>
        /// <param name="condition">состояние актива</param>
        /// <param name="price">цена актива</param>
        private void ChangeConditionReaction(string assetName, bool condition, decimal price)
        {
            Random difference = new Random();

            if (condition)
            {
                optimalPrice[assetName] = price+(decimal)difference.Next(-10,10);
                PriceChangeReaction(assetName, price);
            }
            else
            {
                optimalPrice.Remove(assetName);
            }
        }


        /// <summary>
        /// рекация банка на изменение цены актива
        /// </summary>
        /// <param name="assetName">имя актива</param>
        /// <param name="newPrice">новая цена</param>
        private void PriceChangeReaction(string assetName, decimal newPrice)
        {
            if(CheckPrice(assetName, newPrice))
            {
                Console.WriteLine($"Bank {Name} decided to sell {assetName}");
            }
            else
            {
                Console.WriteLine($"Bank {Name} decided to not sell {assetName}");

            }
        }



        /// <summary>
        /// проверка текущей цены и оптимальной
        /// </summary>
        /// <param name="assetName">имя актива</param>
        /// <param name="price">текущая цена</param>
        /// <returns>подходит ли цена для покупки</returns>
        private bool CheckPrice(string assetName, decimal price)
        {
            if (optimalPrice[assetName] <= price)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// реакция банка на новый актив
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="price"></param>
        private void NewAssetReaction(string assetName, decimal price)
        {
            Random difference = new Random();
            optimalPrice[assetName] = price + (decimal)difference.Next(-25, 25);
        }



    }
}
