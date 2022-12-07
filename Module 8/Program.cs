using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_8
{
    class Program
    {
        static int foodX;
        static int foodY;
        static int headX = 20;
        static int headY = 10;
        static int dir = 0;
        static int Snakelen = 10;
        static int[] body_x = new int[100];
        static int[] body_y = new int[100];
        static int Score = 0;
        static int MaxScore = 0;
        static void SpawnFood()
        {
            Random rnd = new Random();

            foodX = rnd.Next(0, 120);
            if (foodX % 2 != 0) foodX += 1;
            foodY = rnd.Next(0, 40);

            for(int j=0; j < Snakelen; j++)
            {
                if (foodX == body_x[j] && foodY == body_y[j])
                {
                    SpawnFood();
                    break;
                }
            }
        }

        static void Main(string[] args)
        {
            //Параметры программы
            Console.SetWindowSize(120, 40);
            Console.SetBufferSize(120, 40);
            Console.CursorVisible = false;
            bool isGame = true;
            int WinCondition=0;
            //Стартовое заполнение змейки
            for (int i=0; i<Snakelen; i++)
            {
                body_x[i] = headX - (i*2);
                body_y[i] = 10;
            }
            Console.SetCursorPosition(60, 10);
            Console.WriteLine("ЗМЕЙКА");
            Console.SetCursorPosition(45, 12);
            Console.WriteLine("Выберите уровень сложности: ");
            Console.SetCursorPosition(45, 14);
            Console.WriteLine("[Q]-Легкий.  [W]-Средний.  [E]-Сложный(Без ограничения счета) ");
            ConsoleKeyInfo Choice;
            Console.SetCursorPosition(0, 0);
            Choice = Console.ReadKey();
            if (Choice.Key == ConsoleKey.Q) WinCondition = 15;
            if (Choice.Key == ConsoleKey.W) WinCondition = 35;
            if (Choice.Key == ConsoleKey.E) WinCondition = 99999;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("      ");
            Console.SetCursorPosition(60, 10);
            Console.WriteLine("                                             ");
            Console.SetCursorPosition(45, 14); 
            Console.WriteLine("                                                                     ");
            Console.SetCursorPosition(45, 12);
            Console.WriteLine("                                             ");

            SpawnFood();
            //Игровой цикл
            while (isGame == true)
            {
                if (Score == WinCondition)
                {
                    Console.SetCursorPosition(40, 17);
                    Console.WriteLine($"Ваш счет = {Score}");
                    Console.SetCursorPosition(40, 19);
                    Console.WriteLine($"Ваш рекорд = {MaxScore}");
                    Console.SetCursorPosition(60, 15);
                    Console.Write("Вы победили!");
                    Console.ReadLine();
                    break;
                }
                //Очистка
                for (int i = 0; i < Snakelen; i++)
                {
                    Console.SetCursorPosition(body_x[i], body_y[i]);
                    Console.Write("  ");
                }

                Console.SetCursorPosition(headX, headY);
                Console.Write("  ");

                //Движение змейки
                if (Console.KeyAvailable == true)
                {
                    ConsoleKeyInfo key;
                    Console.SetCursorPosition(0, 0);
                    key = Console.ReadKey();
                    Console.SetCursorPosition(0, 0);
                    Console.Write(" ");
                    if (key.Key == ConsoleKey.D && dir !=2) dir = 0;
                    if (key.Key == ConsoleKey.S && dir != 3) dir = 1;
                    if (key.Key == ConsoleKey.A && dir != 0) dir = 2;
                    if (key.Key == ConsoleKey.W && dir != 1) dir = 3;
                }
                if (dir == 0) headX += 2;
                if (dir == 1) headY += 1;
                if (dir == 2) headX -= 2;
                if (dir == 3) headY -= 1;

                if (headX < 0) headX = 118;
                if (headX > 118) headX = 0;
                if (headY < 0) headY = 39;
                if (headY > 39) headY = 0;

                for (int i = Snakelen; i > 0; i--)
                {
                    body_x[i] = body_x[i - 1];
                    body_y[i] = body_y[i - 1];
                }
                body_x[0] = headX;
                body_y[0] = headY;

                for(int i=1; i < Snakelen; i++)
                {
                    if (body_x[i] == headX && body_y[i] == headY)
                    {
                        Console.SetCursorPosition(40, 15);
                        Console.WriteLine("Вы проиграли. Нажмите R для перезапуска.");
                        Console.SetCursorPosition(40, 17);
                        Console.WriteLine($"Ваш счет = {Score}");
                        Console.SetCursorPosition(40, 19);
                        Console.WriteLine($"Ваш рекорд = {MaxScore}");
                        ConsoleKeyInfo answer;
                        Console.SetCursorPosition(0, 0);
                        answer = Console.ReadKey();
                        Console.SetCursorPosition(40, 17);
                        Console.WriteLine("                                              ");
                        Console.SetCursorPosition(40, 15);
                        Console.WriteLine("                                              ");
                        Console.SetCursorPosition(40, 19);
                        Console.WriteLine("                                              ");
                        Console.SetCursorPosition(0, 0);
                        Console.Write(" ");
                        if (answer.Key == ConsoleKey.R)
                        {
                            headX = 20;
                            headY = 10;
                            dir = 0;
                            Snakelen = 10;
                            body_x = new int[100];
                            body_y = new int[100];
                            Score = 0;
                            isGame = true;
                        }
                        if (answer.Key != ConsoleKey.R) isGame = false;
                    }
                }

                //Еда
                if (headX == foodX && headY == foodY)
                {
                    SpawnFood();
                    Snakelen++;
                    Score++;
                    if (Score > MaxScore) MaxScore = Score;
                }
                //Отрисовка
                Console.ForegroundColor = ConsoleColor.Green;
                for(int i = 0; i < Snakelen; i++)
                {
                    Console.SetCursorPosition(body_x[i], body_y[i]);
                    Console.Write("█"+ "█");
                }


                Console.SetCursorPosition(headX, headY);
                Console.Write("█" + "█");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(foodX, foodY);
                Console.Write("█" + "█");
                //Ожидание
                System.Threading.Thread.Sleep(50);
            }
        }
    }
}
