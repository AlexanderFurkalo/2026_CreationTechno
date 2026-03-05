using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_4_TaskFor3
{
    class Program
    {
        abstract class Component
        {
            public string Manufacturer { get; set; }
            public abstract Component Clone();
        }


        class Case : Component
        {
            public string FormFactor { get; set; }

            public override Component Clone()
            {
                return (Component)this.MemberwiseClone();
            }
        }

        class RAM : Component
        {
            public int CapacityGB { get; set; }
            public int FrequencyMHz { get; set; }

            public override Component Clone()
            {
                return (Component)this.MemberwiseClone();
            }
        }

        class HDD : Component
        {
            public int CapacityGB { get; set; }
            public string Type { get; set; } 

            public override Component Clone()
            {
                return (Component)this.MemberwiseClone();
            }
        }

        class CPU : Component
        {
            public int Cores { get; set; }
            public double FrequencyGHz { get; set; }

            public override Component Clone()
            {
                return (Component)this.MemberwiseClone();
            }
        }

        class GPU : Component
        {
            public int MemoryGB { get; set; }

            public override Component Clone()
            {
                return (Component)this.MemberwiseClone();
            }
        }

        class Motherboard : Component
        {
            public string Chipset { get; set; }

            public override Component Clone()
            {
                return (Component)this.MemberwiseClone();
            }
        }

        class Monitor : Component
        {
            public int SizeInches { get; set; }

            public override Component Clone()
            {
                return (Component)this.MemberwiseClone();
            }
        }

        abstract class PCPrototype
        {
            public abstract PCPrototype Clone();
            public abstract void ShowConfiguration();
        }

        class PC : PCPrototype
        {
            public Case Case { get; set; }
            public RAM RAM { get; set; }
            public HDD HDD { get; set; }
            public Motherboard Motherboard { get; set; }
            public CPU CPU { get; set; }
            public GPU GPU { get; set; }
            public Monitor Monitor { get; set; }

            public override PCPrototype Clone()
            {
                return new PC
                {
                    Case = (Case)this.Case.Clone(),
                    RAM = (RAM)this.RAM.Clone(),
                    HDD = (HDD)this.HDD.Clone(),
                    Motherboard = (Motherboard)this.Motherboard.Clone(),
                    CPU = (CPU)this.CPU.Clone(),
                    GPU = (GPU)this.GPU.Clone(),
                    Monitor = (Monitor)this.Monitor.Clone()
                };
            }

            public override void ShowConfiguration()
            {
                Console.WriteLine($"CPU: {CPU.Manufacturer}, {CPU.Cores} cores {CPU.FrequencyGHz} GHz");
                Console.WriteLine($"RAM: {RAM.Manufacturer}, {RAM.CapacityGB} GB");
                Console.WriteLine($"GPU: {GPU.Manufacturer}, {GPU.MemoryGB} GB");
                Console.WriteLine($"HDD: {HDD.Manufacturer}, {HDD.CapacityGB} GB {HDD.Type}");
                Console.WriteLine($"Motherboard: {Motherboard.Manufacturer}, {Motherboard.Chipset}");
                Console.WriteLine($"Case: {Case.Manufacturer}, {Case.FormFactor}");
                Console.WriteLine($"Monitor: {Monitor.Manufacturer}, {Monitor.SizeInches}\"");
                Console.WriteLine("-----------------------------------");
            }
        }

        class PCConfigurator
        {
            private PC homePC;
            private PC officePC;
            private PC gamingPC;

            public PCConfigurator()
            {
                // Home
                homePC = new PC
                {
                    CPU = new CPU { Manufacturer = "Intel", Cores = 4, FrequencyGHz = 3.0 },
                    RAM = new RAM { Manufacturer = "Kingston", CapacityGB = 8, FrequencyMHz = 2666 },
                    GPU = new GPU { Manufacturer = "Intel", MemoryGB = 2 },
                    HDD = new HDD { Manufacturer = "WD", CapacityGB = 512, Type = "SSD" },
                    Case = new Case { Manufacturer = "Zalman", FormFactor = "ATX" },
                    Motherboard = new Motherboard { Manufacturer = "ASUS", Chipset = "B460" },
                    Monitor = new Monitor { Manufacturer = "LG", SizeInches = 24 }
                };

                // Office
                officePC = (PC)homePC.Clone();
                officePC.RAM.CapacityGB = 16;

                // Gaming
                gamingPC = (PC)homePC.Clone();
                gamingPC.CPU.Cores = 8;
                gamingPC.GPU = new GPU { Manufacturer = "NVIDIA", MemoryGB = 12 };
                gamingPC.RAM.CapacityGB = 32;
            }

            public PC GetHomePC() => (PC)homePC.Clone();
            public PC GetOfficePC() => (PC)officePC.Clone();
            public PC GetGamingPC() => (PC)gamingPC.Clone();
        }

        static void Main(string[] args)
        {
            PCConfigurator configurator = new PCConfigurator();

            PC myGamingPC = configurator.GetGamingPC();
            myGamingPC.ShowConfiguration();

            PC myOfficePC = configurator.GetOfficePC();
            myOfficePC.ShowConfiguration();

            PC myHomePC = configurator.GetHomePC();
            myHomePC.ShowConfiguration();

            Console.ReadKey();
        }
    }
}
