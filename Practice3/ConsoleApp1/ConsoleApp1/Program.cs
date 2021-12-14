using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Car auto = new Car(4, "Volvo", new PetrolMove());
            auto.Move();
            auto.Movable = new ElectricMove();
            auto.Move();
            Car[] cars = new Car[]
            {
                new Car(4, "Nissan", new PetrolMove()),
                new Car(2, "BMW", new ElectricMove()),
                new Car(4, "Toyota", new PetrolMove()),
                auto
            };
            Garage garage = new Garage(cars);
            CarChecker carChecker = new CarChecker();
            carChecker.CheckCar(garage);

        }
    }

    interface IMovable
    {
        void Move();
    }

    class PetrolMove : IMovable
    {
        public void Move()
        {
            Console.WriteLine("Перемещение на бензине");
        }
    }

    class ElectricMove : IMovable
    {
        public void Move()
        {
            Console.WriteLine("Перемещение на электричестве");
        }
    }
    class Car
    {
        protected int passengers;
        protected string model;
        public void GetModel()
        {
            Console.WriteLine(model);
        }
        public Car(int num, string model, IMovable mov)
        {
            this.passengers = num;
            this.model = model;
            Movable = mov;
        }
        public IMovable Movable { private get; set; }
        public void Move()
        {
            Movable.Move();
        }
    }
}
