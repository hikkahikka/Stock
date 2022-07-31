using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock
{
    /// <summary>
    /// класс описывающий актив на бирже
    /// </summary>
    public class Asset
    {
        /// <summary>
        /// id актива
        /// </summary>
        public int Id { get; private set; }


        /// <summary>
        /// имя актива
        /// </summary>
        public string Name { get; private set; }



        /// <summary>
        /// цена актива
        /// </summary>
        public decimal Price { get; private set; }


        /// <summary>
        /// состояние (открыт/закрыт для торговли)
        /// </summary>
        public bool IsOpen { get;  set; }



        /// <summary>
        /// конструктор
        /// </summary>
        public Asset(int id, string name, decimal price, bool isOpen)
        {
            Id = id;
            Name = name;
            Price = price;
            IsOpen = isOpen;
        }

        /// <summary>
        /// изменение цены
        /// </summary>
        /// <param name="difference">разница между текущим и новым значением</param>
        public void ChangePrice(decimal difference)
        {
            Price += difference;
        }
       

        /// <summary>
        /// перегрузка ToString()
        /// </summary>
        public override string ToString()
        {
            return $"{Name} - {Price}";
        }
    }
}
