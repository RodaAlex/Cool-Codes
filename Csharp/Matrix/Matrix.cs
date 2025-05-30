// This code simulates a "Matrix" style rain effect in the console, where characters fall down the screen.
// Each column of characters falls at a random speed and length, creating a dynamic visual effect.
// The simulation continues until the user presses the Escape key, at which point it exits.

using System;
using System.Collections.Generic;
using System.Threading;

class MatrixRain
{
    static int Width => Console.WindowWidth;
    static int Height => Console.WindowHeight;
    static bool[,]? activeColumns;
    static Random rand = new Random();

    static void Main()
    {
        Console.CursorVisible = false;

        // Initialize rain columns
        activeColumns = new bool[Width, Height];
        List<Column> columns = new List<Column>();

        for (int i = 0; i < Width; i++)
        {
            columns.Add(new Column(i));
        }

        // Main loop
        while (!Console.KeyAvailable || Console.ReadKey(true).Key != ConsoleKey.Escape)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            foreach (var col in columns)
            {
                col.Fall();
            }

            Thread.Sleep(50);
        }

        Console.ResetColor();
        Console.Clear();
    }

    class Column
    {
        private int x;
        private int y;
        private int length;
        private int speed;
        private int cooldown;

        public Column(int x)
        {
            this.x = x;
            this.y = rand.Next(Height);
            this.length = rand.Next(5, Height / 2);
            this.speed = rand.Next(1, 5);
            this.cooldown = rand.Next(0, 20);
        }

        public void Fall()
        {
            if (cooldown-- > 0)
                return;
            cooldown = speed;

            // Ensure we're within visible height before drawing
            if (y >= 0 && y < Console.WindowHeight)
            {
                char c = GetRandomChar();
                Console.SetCursorPosition(x, y);
                Console.Write(c);
            }

            int tail = y - length;
            if (tail >= 0 && tail < Console.WindowHeight)
            {
                Console.SetCursorPosition(x, tail);
                Console.Write(' ');
            }

            y++;
            if (y >= Console.WindowHeight + length)
            {
                y = 0;
                length = rand.Next(5, Console.WindowHeight / 2);
                speed = rand.Next(1, 5);
                cooldown = rand.Next(0, 20);
            }
        }

        private char GetRandomChar()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@#$%&*+-";
            return chars[rand.Next(chars.Length)];
        }
    }
}
