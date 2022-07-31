using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock
{
    /// <summary>
    /// класс брокера
    /// </summary>
    public  class Broker
    {
        /// <summary>
        /// имя брокера
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
        /// <param name="name">имя брокера</param>
        /// <param name="stock">биржа</param>
        public Broker(string name, Stock stock)
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
        /// реакция брокера на изменение состояния актива 
        /// </summary>
        /// <param name="assetName">имя актива</param>
        /// <param name="condition">состояние актива</param>
        /// <param name="price">цена актива</param>
        private void ChangeConditionReaction(string assetName, bool condition, decimal price)
        {
            Random difference = new Random();

            if (condition)
            {
                optimalPrice[assetName] = price + (decimal)difference.Next(-10, 10);
                PriceChangeReaction(assetName, price);
            }
            else
            {
                optimalPrice.Remove(assetName);
            }
        }



        /// <summary>
        /// рекация брокера на изменение цены актива
        /// </summary>
        /// <param name="assetName">имя актива</param>
        /// <param name="newPrice">новая цена</param>
        private void PriceChangeReaction(string assetName, decimal newPrice)
        {
            if (CheckPrice(assetName, newPrice))
            {
                Console.WriteLine($"Broker {Name} decided to buy {assetName}");
            }
            else
            {
                Console.WriteLine($"Broker {Name} decided to not buy {assetName}");

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
            if (optimalPrice[assetName] >= price)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// реакция брокера на новый актив
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
