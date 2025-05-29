// This C# program simulates particles moving around the console window.
// Each particle bounces off the edges of the console window.
// The particles are represented by asterisks ('*') and move in random directions.
// The simulation runs until the user presses the Escape key.

using System;
using System.Collections.Generic;
using System.Threading;

class ParticleSimulator
{
    static int Width => Console.WindowWidth;
    static int Height => Console.WindowHeight;
    const int ParticleCount = 50;
    const int FrameDelay = 50; // milliseconds

    static void Main()
    {
        Console.CursorVisible = false;
        var particles = new List<Particle>();
        var rand = new Random();

        for (int i = 0; i < ParticleCount; i++)
        {
            particles.Add(new Particle(
                x: rand.NextDouble() * Width,
                y: rand.NextDouble() * Height,
                vx: rand.NextDouble() * 2 - 1,
                vy: rand.NextDouble() * 2 - 1,
                symbol: '*'
            ));
        }

        while (!Console.KeyAvailable || Console.ReadKey(true).Key != ConsoleKey.Escape)
        {
            Console.Clear();

            foreach (var p in particles)
            {
                p.Update();
                p.Draw();
            }

            Thread.Sleep(FrameDelay);
        }
    }

    class Particle
    {
        public double X, Y;
        public double VX, VY;
        public char Symbol;

        public Particle(double x, double y, double vx, double vy, char symbol)
        {
            X = x;
            Y = y;
            VX = vx;
            VY = vy;
            Symbol = symbol;
        }

        public void Update()
        {
            X += VX;
            Y += VY;

            int maxX = Console.WindowWidth - 1;
            int maxY = Console.WindowHeight - 1;

            if (X < 0 || X > maxX)
            {
                VX = -VX;
                X = Math.Clamp(X, 0, maxX);
            }

            if (Y < 0 || Y > maxY)
            {
                VY = -VY;
                Y = Math.Clamp(Y, 0, maxY);
            }
        }

        public void Draw()
        {
            int drawX = (int)X;
            int drawY = (int)Y;

            if (drawX >= 0 && drawX < Console.WindowWidth &&
                drawY >= 0 && drawY < Console.WindowHeight)
            {
                Console.SetCursorPosition(drawX, drawY);
                Console.Write(Symbol);
            }
        }
    }
}
