using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{

    class CarChecker
    {
        public void CheckCar(Garage garage)
        {
            ICarIterator iterator = garage.CreateNumerator();
            while (iterator.HasNext())
            {
                Car car = iterator.Next();
                car.GetModel();
            }
        }
    }

    interface ICarIterator
    {
        bool HasNext();
        Car Next();
    }

    interface ICarNumerable
    {
        ICarIterator CreateNumerator();
        int Count { get; }
        Car this[int index] { get; }
    }

    class Garage : ICarNumerable
    {
        private Car[] cars;
        public Garage(Car[] _cars)
        {
            cars = _cars;
            
        }
        public int Count
        {
            get { return cars.Length; }
        }

        public Car this[int index]
        {
            get { return cars[index]; }
        }
        public ICarIterator CreateNumerator()
        {
            return new GarageNumerator(this);
        }
    }

    class GarageNumerator : ICarIterator
    {
        ICarNumerable aggregate;
        int index = 0;
        public GarageNumerator(ICarNumerable a)
        {
            aggregate = a;
        }
        public bool HasNext()
        {
            return index < aggregate.Count;
        }

        public Car Next()
        {
            return aggregate[index++];
        }
    }
}
