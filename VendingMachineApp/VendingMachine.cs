using System;
using System.Collections.Generic;

namespace VendingMachineApp
{
    public class VendingMachine
    {
        private List<Product> products;
        private decimal balance = 0m;
        private decimal collectedMoney = 0m;

        public VendingMachine()
        {
            products = new List<Product>
            {
                new Product("Coka Cola", 50, 5),
                new Product("Iced Tea", 40, 5),
                new Product("Hot Chocolate", 70, 3),
                new Product("Water", 30, 10)
            };
        }
        private string ReadNonEmptyString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                    return input!.Trim();
                Console.WriteLine("Ошибка: ввод не должен быть пустым. Попробуйте ещё раз.");
            }
        }

        private decimal ReadPositiveDecimal(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? s = Console.ReadLine();
                if (decimal.TryParse(s, out decimal value) && value > 0)
                    return value;
                Console.WriteLine("Ошибка: введите корректную положительную цену (число).");
            }
        }

        private int ReadPositiveInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? s = Console.ReadLine();
                if (int.TryParse(s, out int value) && value > 0)
                    return value;
                Console.WriteLine("Ошибка: введите корректное положительное целое число.");
            }
        }

        public void ShowProducts()
        {
            Console.WriteLine("\nДоступные товары:");
            for (int i = 0; i < products.Count; i++)
                Console.WriteLine($"{i + 1}. {products[i]}");
        }

        public void InsertCoin(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Некорректная сумма!");
                return;
            }
            balance += amount;
            Console.WriteLine($"Баланс: {balance} руб.");
        }

        public void BuyProduct(int index)
        {
            if (index < 1 || index > products.Count)
            {
                Console.WriteLine("Неверный номер товара!");
                return;
            }

            Product product = products[index - 1];
            if (product.Quantity == 0)
            {
                Console.WriteLine("Товар закончился!");
                return;
            }

            if (balance < product.Price)
            {
                Console.WriteLine($"Недостаточно средств. Нужно {product.Price - balance} руб.");
                return;
            }

            balance -= product.Price;
            product.Quantity--;
            collectedMoney += product.Price;

            Console.WriteLine($"Вы купили {product.Name}. Остаток: {balance} руб.");
        }

        public void ReturnMoney()
        {
            Console.WriteLine($"Возвращено {balance} руб.");
            balance = 0;
        }

        public void AdminMode()
        {
            Console.Write("Введите пароль администратора: ");
            string password = Console.ReadLine()!;

            if (password != "admin")
            {
                Console.WriteLine("Неверный пароль!");
                return;
            }

            while (true)
            {
                Console.WriteLine("\n--- Админ-режим ---");
                Console.WriteLine($"Собрано средств: {collectedMoney} руб.");
                Console.WriteLine("1. Пополнить количество товара");
                Console.WriteLine("2. Добавить новый товар");
                Console.WriteLine("3. Обнулить кассу");
                Console.WriteLine("4. Выйти из админ-режима");
                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine()!;

                switch (choice)
                {
                    case "1":
                        ShowProducts();
                        Console.Write("Введите номер товара для пополнения: ");
                        if (int.TryParse(Console.ReadLine(), out int num) && num >= 1 && num <= products.Count)
                        {
                            Console.Write("Введите количество для добавления: ");
                            if (int.TryParse(Console.ReadLine(), out int qty) && qty > 0)
                            {
                                products[num - 1].Quantity += qty;
                                Console.WriteLine("Товар успешно пополнен!");
                            }
                            else
                                Console.WriteLine("Ошибка: неверное количество.");
                        }
                        else
                            Console.WriteLine("Ошибка: неверный номер товара.");
                        break;

                    case "2":
                        string name = ReadNonEmptyString("Введите название нового товара: ");
                        decimal price = ReadPositiveDecimal("Введите цену товара: ");
                        int newQty = ReadPositiveInt("Введите количество: ");

                        products.Add(new Product(name, price, newQty));
                        Console.WriteLine($"Товар '{name}' добавлен!");
                        break;

                    case "3":
                        collectedMoney = 0;
                        Console.WriteLine("Касса обнулена.");
                        break;

                    case "4":
                        Console.WriteLine("Выход из админ-режима.");
                        return;

                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
        }
    }
}
