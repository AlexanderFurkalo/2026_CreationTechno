using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7_8_ForFacade
{
    class CPU
    {
        public void Freeze()
        {
            Console.WriteLine("CPU: Freezing processor...");
        }

        public void Jump(long position)
        {
            Console.WriteLine($"CPU: Jumping to {position}...");
        }

        public void Execute()
        {
            Console.WriteLine("CPU: Executing instructions...");
        }
    }

    class Memory
    {
        public void Load(long position, string data)
        {
            Console.WriteLine($"Memory: Loading data '{data}' into position {position}...");
        }
    }

    class HardDrive
    {
        public string Read(long lba, int size)
        {
            Console.WriteLine($"HardDrive: Reading {size} bytes from sector {lba}...");
            return "BOOT_DATA";
        }
    }

    class ComputerFacade
    {
        private CPU cpu;
        private Memory memory;
        private HardDrive hardDrive;

        public ComputerFacade()
        {
            cpu = new CPU();
            memory = new Memory();
            hardDrive = new HardDrive();
        }

        public void TurnOn()
        {
            Console.WriteLine("Starting computer...\n");

            cpu.Freeze();

            string bootData = hardDrive.Read(0, 1024);

            memory.Load(0, bootData);

            cpu.Jump(0);

            cpu.Execute();

            Console.WriteLine("\nComputer started successfully!");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ComputerFacade computer = new ComputerFacade();
            computer.TurnOn();

            Console.ReadLine();
        }
    }
}
