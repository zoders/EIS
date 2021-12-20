using System;

namespace Practice4
{
    class Program
    {
        static void Main(string[] args)
        {
            Computer comp = new Computer();
            comp.Launch("Windows 10");
            Console.WriteLine(comp.OS.Name);

            comp.OS = OS.getInstance("Windows 11");
            Console.WriteLine(comp.OS.Name);

            Console.ReadLine();
        }
    }
    class Computer
    {
        public OS OS { get; set; }
        public void Launch(string osName)
        {
            OS = OS.getInstance(osName);
        }
    }
    class OS
    {
        private static OS instance;

        public string Name { get; private set; }
        private static object syncLocker = new Object();

        protected OS(string name)
        {
            this.Name = name;
        }

        public static OS getInstance(string name)
        {
            lock (syncLocker)
            {
                if (instance == null)
                    instance = new OS(name);
                return instance;
            }
        }
    }
}
