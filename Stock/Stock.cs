using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock
{
    /// <summary>
    /// делегат использующийся для события изменения цены и добавления актива
    /// </summary>
    /// <param name="assetName">имя актива</param>
    /// <param name="newPrice">новая цена</param>
    public delegate void AssetChanged(string assetName, decimal newPrice);


    /// <summary>
    /// делегат для события изменения состояния актива
    /// </summary>
    /// <param name="assetName">имя актива</param>
    /// <param name="condition">состояние</param>
    /// <param name="price">цена</param>
    public delegate void AssetConditionChanged(string assetName, bool condition, decimal price);

    public class Stock
    {
        /// <summary>
        /// список активов
        /// </summary>
        public List<Asset> assets = new List<Asset>();

        /// <summary>
        /// событие изменения цены актива
        /// </summary>
        public event AssetChanged? PriceChanged;



        /// <summary>
        /// событие добавления актива
        /// </summary>
        public event AssetChanged? AssetAdded;



        /// <summary>
        /// событие изхменения состояния актива
        /// </summary>
        public event AssetConditionChanged? AssetChangeCondition;



        /// <summary>
        /// добавление актива
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="name">имя</param>
        /// <param name="price">цена</param>
        public void AddAsset(int id, string name, decimal price)
        {
            assets.Add(new Asset(id, name, price, true));

            Console.WriteLine($"New asset has been added: {name}. \n\t\t\tStart price: {price}\n");

            AssetAdded?.Invoke(name, price);
        }


        /// <summary>
        /// изменение состояния актива
        /// </summary>
        /// <param name="asset">актив</param>
        private void ChangeAssetCondition(Asset asset)
        {
            asset.IsOpen=!asset.IsOpen;
            Console.WriteLine($"Asset {asset.Name} change his activity. Now his opened is {asset.IsOpen}");
            AssetChangeCondition?.Invoke(asset.Name, asset.IsOpen, asset.Price);
        }


        /// <summary>
        /// изменение цены актива
        /// </summary>
        /// <param name="asset">актив</param>
        private void ChangeAssetPrice(Asset asset)
        {
            if (asset.IsOpen)
            {
                Random difference = new Random();


                decimal priceDifference = difference.Next(-20, 20);

                asset.ChangePrice(priceDifference);

                Console.WriteLine($"{asset.Name} price changed. Current price is {asset.Price}\n");

                PriceChanged?.Invoke(asset.Name, asset.Price);
            }
        }

        /// <summary>
        /// получение случайного актива
        /// </summary>
        /// <returns>случайный актив</returns>
        private Asset GetRandomAsset()
        {
            Random random = new Random(); 

            return assets[random.Next(0, assets.Count)];
        }



        /// <summary>
        /// вывод цены в консоль
        /// </summary>
        private void ShowPrices()
        {
            Console.WriteLine("Costs all assets:\n");
            foreach (var asset in assets)
            {
                if(asset.IsOpen) Console.WriteLine(asset.ToString());
            }
            Console.WriteLine();
        }

        /// <summary>
        /// метод для случайного изменения состояния актива
        /// </summary>
        private void TryToChangeAssetCondition(Asset asset)
        {
            Random random = new Random();
            if(random.Next(0, 10) % 5 == 0)
            {
                ChangeAssetCondition(asset);
            }

        }

        /// <summary>
        /// начало программы (торгов) 
        /// </summary>
        public void StartBargaining()
        {
            while (true)
            {
                ShowPrices();
                ChangeAssetPrice(GetRandomAsset());
                TryToChangeAssetCondition(GetRandomAsset());
                Console.WriteLine("\n\t\tPress enter...\n\n\n\n");
                Console.Read();

                
            }
        }

    }
}
