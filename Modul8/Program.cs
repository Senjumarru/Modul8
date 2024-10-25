using System;
using System.Collections.Generic;

namespace SmartHomeCommandPattern
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    public class Light
    {
        public void On() => Console.WriteLine("Light is turned on.");
        public void Off() => Console.WriteLine("Light is turned off.");
    }

    public class Door
    {
        public void Open() => Console.WriteLine("Door is opened.");
        public void Close() => Console.WriteLine("Door is closed.");
    }

    public class Thermostat
    {
        private int temperature;

        public Thermostat(int initialTemperature) => temperature = initialTemperature;

        public void IncreaseTemperature()
        {
            temperature++;
            Console.WriteLine($"Temperature increased to {temperature}°C.");
        }

        public void DecreaseTemperature()
        {
            temperature--;
            Console.WriteLine($"Temperature decreased to {temperature}°C.");
        }
    }

    public class LightOnCommand : ICommand
    {
        private readonly Light light;

        public LightOnCommand(Light light) => this.light = light;

        public void Execute() => light.On();

        public void Undo() => light.Off();
    }

    public class LightOffCommand : ICommand
    {
        private readonly Light light;

        public LightOffCommand(Light light) => this.light = light;

        public void Execute() => light.Off();

        public void Undo() => light.On();
    }

    public class DoorOpenCommand : ICommand
    {
        private readonly Door door;

        public DoorOpenCommand(Door door) => this.door = door;

        public void Execute() => door.Open();

        public void Undo() => door.Close();
    }

    public class DoorCloseCommand : ICommand
    {
        private readonly Door door;

        public DoorCloseCommand(Door door) => this.door = door;

        public void Execute() => door.Close();

        public void Undo() => door.Open();
    }

    public class IncreaseTemperatureCommand : ICommand
    {
        private readonly Thermostat thermostat;

        public IncreaseTemperatureCommand(Thermostat thermostat) => this.thermostat = thermostat;

        public void Execute() => thermostat.IncreaseTemperature();

        public void Undo() => thermostat.DecreaseTemperature();
    }

    public class DecreaseTemperatureCommand : ICommand
    {
        private readonly Thermostat thermostat;

        public DecreaseTemperatureCommand(Thermostat thermostat) => this.thermostat = thermostat;

        public void Execute() => thermostat.DecreaseTemperature();

        public void Undo() => thermostat.IncreaseTemperature();
    }

    public class Invoker
    {
        private readonly Stack<ICommand> commandHistory = new Stack<ICommand>();

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            commandHistory.Push(command);
        }

        public void UndoLastCommand()
        {
            if (commandHistory.Count > 0)
            {
                var command = commandHistory.Pop();
                command.Undo();
            }
            else
            {
                Console.WriteLine("No commands to undo.");
            }
        }
    }

    public class Program
    {
        public static void Main()
        {
            var light = new Light();
            var door = new Door();
            var thermostat = new Thermostat(22);

            var lightOn = new LightOnCommand(light);
            var lightOff = new LightOffCommand(light);
            var doorOpen = new DoorOpenCommand(door);
            var doorClose = new DoorCloseCommand(door);
            var tempIncrease = new IncreaseTemperatureCommand(thermostat);
            var tempDecrease = new DecreaseTemperatureCommand(thermostat);

            var invoker = new Invoker();

            invoker.ExecuteCommand(lightOn);
            invoker.ExecuteCommand(doorOpen);
            invoker.ExecuteCommand(tempIncrease);
            invoker.UndoLastCommand();
            invoker.ExecuteCommand(lightOff);
            invoker.UndoLastCommand();
        }
    }
}

