using System;

class Tour
{
    // Поля
    private string destination;
    private int duration;
    private decimal price;

    // Властивості
    public string Destination
    {
        get { return destination; }
        set { destination = value; }
    }

    public int Duration
    {
        get { return duration; }
        set { duration = value; }
    }

    public decimal Price
    {
        get { return price; }
        set { price = value; }
    }

    // Конструктор за замовчуванням
    public Tour()
    {
        destination = "Unknown Destination";
        duration = 3;
        price = 1000;
    }

    // Параметризований конструктор
    public Tour(string destination, int duration, decimal price)
    {
        this.destination = destination;
        this.duration = duration;
        this.price = price;
    }

    // Методи
    public virtual void BookTour()
    {
        Console.WriteLine($"Тур до {destination} на {duration} днів заброньовано.");
    }

    public virtual void GetTourDetails()
    {
        Console.WriteLine($"Місце призначення: {destination}, Тривалість: {duration} днів, Ціна: {price}.");
    }

    // Оператор +
    public static Tour operator +(Tour tour1, Tour tour2)
    {
        string combinedDestination = $"{tour1.destination} та {tour2.destination}";
        int combinedDuration = tour1.duration + tour2.duration;
        decimal combinedPrice = tour1.price + tour2.price;

        return new Tour(combinedDestination, combinedDuration, combinedPrice);
    }

    // Розрахунок загальної вартості туру (можна перевизначати в дочірніх класах)
    public virtual decimal CalculateTotalPrice()
    {
        return price;
    }
}

class AdventureTour : Tour
{
    // Додаткове поле
    private string difficultyLevel;

    // Конструктори
    public AdventureTour() : base() { }

    public AdventureTour(string destination, int duration, decimal price, string difficultyLevel)
        : base(destination, duration, price)
    {
        this.difficultyLevel = difficultyLevel;
    }

    // Методи
    public override void BookTour()
    {
        Console.WriteLine($"Пригодницький тур до {Destination} заброньовано. Рівень складності: {difficultyLevel}.");
    }

    public void SetDifficultyLevel(string level)
    {
        difficultyLevel = level;
        Console.WriteLine($"Рівень складності туру змінено на {level}.");
    }

    // Перевизначений метод для розрахунку вартості (можна додати екскурсії тощо)
    public override decimal CalculateTotalPrice()
    {
        // Припустимо, що складність додає 200 до ціни
        return base.CalculateTotalPrice() + 200;
    }
}

class RelaxationTour : Tour
{
    // Додаткове поле
    private bool spaIncluded;

    // Конструктори
    public RelaxationTour() : base() { }

    public RelaxationTour(string destination, int duration, decimal price, bool spaIncluded)
        : base(destination, duration, price)
    {
        this.spaIncluded = spaIncluded;
    }

    // Методи
    public override void BookTour()
    {
        Console.WriteLine($"Релаксаційний тур до {Destination} заброньовано. Спа: {(spaIncluded ? "включено" : "не включено")}.");
    }

    public void IncludeSpa()
    {
        spaIncluded = true;
        Console.WriteLine("Спа включено до туру.");
    }

    // Перевизначений метод для розрахунку вартості (спа додає 300 до ціни)
    public override decimal CalculateTotalPrice()
    {
        decimal total = base.CalculateTotalPrice();
        if (spaIncluded)
        {
            total += 300;
        }
        return total;
    }
}

// Приклад використання
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Оберіть тип туру (1 - Пригодницький, 2 - Релаксаційний): ");
        int tourType = Convert.ToInt32(Console.ReadLine());

        Tour tour = null;

        if (tourType == 1)
        {
            // Введення даних для пригодницького туру
            Console.WriteLine("Введіть дані для пригодницького туру:");
            Console.Write("Місце призначення: ");
            string adventureDestination = Console.ReadLine();
            Console.Write("Тривалість (дні): ");
            int adventureDuration = Convert.ToInt32(Console.ReadLine());
            Console.Write("Ціна: ");
            decimal adventurePrice = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Рівень складності: ");
            string difficultyLevel = Console.ReadLine();

            tour = new AdventureTour(adventureDestination, adventureDuration, adventurePrice, difficultyLevel);
        }
        else if (tourType == 2)
        {
            // Введення даних для релаксаційного туру
            Console.WriteLine("Введіть дані для релаксаційного туру:");
            Console.Write("Місце призначення: ");
            string relaxationDestination = Console.ReadLine();
            Console.Write("Тривалість (дні): ");
            int relaxationDuration = Convert.ToInt32(Console.ReadLine());
            Console.Write("Ціна: ");
            decimal relaxationPrice = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Чи включено спа (true/false): ");
            bool spaIncluded = Convert.ToBoolean(Console.ReadLine());

            tour = new RelaxationTour(relaxationDestination, relaxationDuration, relaxationPrice, spaIncluded);
        }

        tour.BookTour();
        tour.GetTourDetails();
        Console.WriteLine($"Загальна вартість: {tour.CalculateTotalPrice()} грн.\n");

        // Об'єднання турів
        Console.WriteLine("Додайте ще один тур для об'єднання:");
        // Додати ще один тур, аналогічно попередньому, для об'єднання (можете повторити кроки).
        // В результаті об'єднаний тур:

        Tour anotherTour = new Tour("Карпати", 7, 1500);  // Наприклад, ще один тур
        Tour combinedTour = tour + anotherTour;
        Console.WriteLine("Деталі об'єднаного туру:");
        combinedTour.GetTourDetails();
        Console.WriteLine($"Загальна вартість об'єднаного туру: {combinedTour.CalculateTotalPrice()} грн.");
    }
}
