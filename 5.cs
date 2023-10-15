using System;



class Товар
{
    public string Назва { get; set; }
    public decimal Ціна { get; set; }
    public string Опис { get; set; }
    public string Категорія { get; set; }
    public int Рейтинг { get; set; }
}

class Користувач
{
    public string Логін { get; set; }
    public string Пароль { get; set; }
    public List<Замовлення> ІсторіяПокупок { get; set; }

    public Користувач(string логін, string пароль)
    {
        Логін = логін;
        Пароль = пароль;
        ІсторіяПокупок = new List<Замовлення>();
    }
}

class Замовлення
{
    public List<Товар> Товари { get; set; }
    public int Кількість { get; set; }
    public decimal Вартість { get; set; }
    public string Статус { get; set; }

    public Замовлення(List<Товар> товари, int кількість)
    {
        Товари = товари;
        Кількість = кількість;
        Вартість = товари.Sum(item => item.Ціна) * кількість;
        Статус = "Нове";
    }
}

interface ISearchable
{
    List<Товар> ПошукЗаКритерієм(Func<Товар, bool> критерій);
}

class Магазин : ISearchable
{
    private List<Товар> Товари { get; set; }
    private List<Користувач> Користувачі { get; set; }
    private List<Замовлення> Замовлення { get; set; }

    public Магазин()
    {
        Товари = new List<Товар>();
        Користувачі = new List<Користувач>();
        Замовлення = new List<Замовлення>();
    }

    public void ДодатиТовар(Товар товар)
    {
        Товари.Add(товар);
    }

    public void ДодатиКористувача(Користувач користувач)
    {
        Користувачі.Add(користувач);
    }

    public void СтворитиЗамовлення(List<Товар> товари, int кількість, Користувач користувач)
    {
        var замовлення = new Замовлення(товари, кількість);
        Замовлення.Add(замовлення);
        користувач.ІсторіяПокупок.Add(замовлення);
    }

    public List<Товар> ПошукЗаКритерієм(Func<Товар, bool> критерій)
    {
        return Товари.Where(критерій).ToList();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Магазин store = new Магазин();

        Товар product1 = new Товар { Назва = "Product 1", Ціна = 10.99M, Категорія = "Category 1" };
        Товар product2 = new Товар { Назва = "Product 2", Ціна = 19.99M, Категорія = "Category 2" };

        store.ДодатиТовар(product1);
        store.ДодатиТовар(product2);

        Користувач user = new Користувач("user123", "password123");
        store.ДодатиКористувача(user);

        List<Товар> productsToOrder = new List<Товар> { product1, product2 };
        store.СтворитиЗамовлення(productsToOrder, 2, user);

        List<Товар> category1Products = store.ПошукЗаКритерієм(product => product.Категорія == "Category 1");
    }
}

