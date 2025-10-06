using System;
using System.Text;

namespace VendingMachineApp
{
    class Program
    {
        static void Main()
        {
            VendingMachine vm = new VendingMachine();

            while (true)
            {
                Console.WriteLine("\n--- Вендинговый автомат ---");
                Console.WriteLine("1. Показать товары");
                Console.WriteLine("2. Внести монету");
                Console.WriteLine("3. Купить товар");
                Console.WriteLine("4. Вернуть сдачу");
                Console.WriteLine("5. Админ-режим");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine()!;
                switch (choice)
                {
                    case "1":
                        vm.ShowProducts();
                        break;
                    case "2":
                        Console.Write("Введите сумму монеты: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal coin))
                            vm.InsertCoin(coin);
                        else
                            Console.WriteLine("Ошибка ввода!");
                        break;
                    case "3":
                        vm.ShowProducts();
                        Console.Write("Введите номер товара: ");
                        if (int.TryParse(Console.ReadLine(), out int index))
                            vm.BuyProduct(index);
                        else
                            Console.WriteLine("Ошибка ввода!");
                        break;
                    case "4":
                        vm.ReturnMoney();
                        break;
                    case "5":
                        vm.AdminMode();
                        break;
                    case "0":
                        Console.WriteLine("До свидания!");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
        }
    }
}
