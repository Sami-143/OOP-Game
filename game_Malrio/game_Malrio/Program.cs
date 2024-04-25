using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using static System.ConsoleColor;
using static System.Console;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using EZInput;

namespace gameMalrio
{
    class Program
    {

        static char[,] mazeArray = new char[37, 182];
        static char gun_Malrio = (char)209;
        static char[,] mar1 = {
                                { ' ', '0', ' '},
                                { '/', '|', gun_Malrio},
                                    { '/', ' ', '\\'}
                               };

        static char[,] master1 = {{'@', '@'}, // array 2d for tyhe master
                            {'-', '-'}};


        static int devilX = 4;
        static int devilY = 20;
        static char malik = (char) 140 ;
        static char[,] devil1 = {{malik, malik, '-', '>'},
                     {malik, malik, ' ', ' '}}; // array 2d for the devil printing in maze


        //malrio's coordinates  
        public static int malrio_X = 8;
        public static int malrio_Y = 6;
        //master's coordinates
        public static int masterX = 60;
        public static int masterY = 7;


        // player bullet data
        static int[,] bullet_right = new int[100, 2]; // bullet array for right fire
        static int[,] bullet_left = new int[100, 2];  // bullet array for left fire

        static int bulletCount = 0;                         // counters of bullets
        static int bulletCount1 = 0;                        // counters of bullets


        static int master_time = 0;
        static string direction2  = "UP";

        static int[,] devilFire = new int[1000,2];                    // devil fire array
        static int devilFireCount = 0;                   // devil fire counter
        static int fire_time = 0;

        static void Main(string[] args)
        {
            Console.ReadKey();


            readData();
            printMaze();
            Console.ReadKey();
            printMalrio();
            printMaster();
            printDevil();
            bool gameRunning = true;
            while (gameRunning)
            {

                if (Keyboard.IsKeyPressed(Key.LeftArrow))
                {
                    playerMoveLeft();
                }
                if (Keyboard.IsKeyPressed(Key.RightArrow))
                {
                    playerMoveRight();
                }
                if (Keyboard.IsKeyPressed(Key.UpArrow))
                {
                    playerMoveUp();
                }
                if (Keyboard.IsKeyPressed(Key.DownArrow))
                {
                    playerMoveDown();
                }

                if (Keyboard.IsKeyPressed(Key.Space))
                {
                    generateBulletRight();
                }
                if (Keyboard.IsKeyPressed(Key.B))
                {
                    generateBulletLeft();
                }


                if (master_time >= 3)
                {
                    masterMove();
                    moveDevil();
                    master_time = 0;
                }

                if (true)
                {
                    generateDevilFire();
                    //fire_time = 0;
                }

                moveBulletRight();
                moveBulletLeft();
                moveDevilFire();
                master_time++;
                fire_time++;
                Thread.Sleep(50);
            }
        }


        static void printMaze()
        {
            for (int x = 0; x < mazeArray.GetLength(0); x++)
            {
                for (int y = 0; y < mazeArray.GetLength(1); y++)
                {
                    Console.Write(mazeArray[x, y]);
                }
                Console.WriteLine();
            }
        }

        static void readData()
        {
            string path = "C:\\Users\\AL-FATAH LAPTOP\\OneDrive\\Desktop\\FINAL GAME\\maze1.txt";
            StreamReader fp = new StreamReader(path);
            string record;
            int row = 0;
            while ((record = fp.ReadLine()) != null)
            {

                for (int x = 0; x < 181; x++)
                {

                    mazeArray[row, x] = record[x];
                }

                row++;

            }

            fp.Close();
        }

        public static char getField(string record, int field)
        {
            int commaCount = 0;
            char item = ' ';
            for (int x = 0; x < record.Length; x++)
            {

                if (commaCount == field)
                {
                    item = record[x];
                    break;
                }
                commaCount++;
            }
            return item;

        }
        //MALRIO
        //FUCTIONS
        //funtion for the printing of Malrio
        static void printMalrio()
        {

            for (int idx = 0; idx < 3; idx++)
            {

                for (int idx1 = 0; idx1 < 3; idx1++)
                {
                    Console.SetCursorPosition(malrio_X + idx1, malrio_Y + idx);
                    Console.Write(mar1[idx, idx1]);
                    mazeArray[idx, idx1] = mar1[idx, idx1];
                }

            }
        }

        static void erase()
        {
            for (int idx = 0; idx < 3; idx++)
            {

                for (int idx1 = 0; idx1 < 3; idx1++)
                {
                    Console.SetCursorPosition(malrio_X + idx1, malrio_Y + idx);
                    Console.Write(" ");
                    mazeArray[idx, idx1] = ' ';
                }
                Console.WriteLine();
            }
        }

        //FUNCTION TO MOVE PLAYER LEFT
        static void playerMoveLeft()
        {
            char nextLocation = mazeArray[malrio_Y, malrio_X - 1];
            char nextLocation1 = mazeArray[malrio_Y + 1, malrio_X - 1];
            char nextLocation2 = mazeArray[malrio_Y + 2, malrio_X - 1];
            //if ((nextLocation1 == '+' || nextLocation == '+' || nextLocation2 == '+'))
            //{
            //    score++;
            //}
            if ((nextLocation1 == ' ' && nextLocation == ' ' && nextLocation2 == ' ')|| (nextLocation1 == '+' || nextLocation == '+' || nextLocation2 == '+'))
            {
                erase();
                malrio_X = malrio_X - 1;
                printMalrio();
            }
            //else if ((nextLocation1 != '#' && nextLocation != '#' && nextLocation2 != '#') || (nextLocation1 == char(254) || nextLocation == char(254) || nextLocation2 == char(254)))
            //{
            //    health--;
            //}
        }

        // FUNCTION TO MOVE PLYER UP
        static void playerMoveRight()
        {
            char nextLocation = mazeArray[malrio_Y, malrio_X + 3];
            char nextLocation1 = mazeArray[malrio_Y + 1, malrio_X + 3];
            char nextLocation2 = mazeArray[malrio_Y + 2, malrio_X + 3];
            //if ((nextLocation1 == '+' || nextLocation == '+' || nextLocation2 == '+'))
            //{
            //    score++;
            //}
            if ((nextLocation1 == ' ' && nextLocation == ' ' && nextLocation2 == ' ')|| (nextLocation1 == '+' || nextLocation == '+' || nextLocation2 == '+'))
            {
                erase();
                malrio_X = malrio_X + 1;
                printMalrio();
            }
            //else if ((nextLocation1 != '#' && nextLocation != '#' && nextLocation2 != '#') || (nextLocation1 == '-' || nextLocation == '-' || nextLocation2 == '-') || (nextLocation1 == '@' || nextLocation == '@' || nextLocation2 == '@'))
            //{
            //    health--;
            //}

            //else if ((nextLocation1 != '#' && nextLocation != '#' && nextLocation2 != '#') || (nextLocation1 == char(254) || nextLocation == char(254) || nextLocation2 == char(254)))
            //{
            //    health--;
            //}

            //if (nextLocation1 == char(178) && nextLocation == char(178) && nextLocation2 == char(178))
            //{
            //    system("cls");
            //    Sleep(50);
            //    gameWin();
            //}
        }

        // FUNCTION TO MOVE PLAYER RIGHT
        static void playerMoveUp()
        {
            char nextLocation = mazeArray[malrio_Y - 1, malrio_X];
            char nextLocation1 = mazeArray[malrio_Y - 1, malrio_X + 1];
            char nextLocation2 = mazeArray[malrio_Y - 1, malrio_X + 2];
            //if ((nextLocation1 == '+' || nextLocation == '+' || nextLocation2 == '+'))
            //{
            //    score++;
            //}
            if ((nextLocation1 == ' ' && nextLocation == ' ' && nextLocation2 == ' ') || (nextLocation1 == '+' || nextLocation == '+' || nextLocation2 == '+'))
            {
                erase();
                malrio_Y = malrio_Y - 1;
                printMalrio();
            }

            //else if ((nextLocation1 != '#' && nextLocation != '#' && nextLocation2 != '#') || (nextLocation1 == '-' || nextLocation == '-' || nextLocation2 == '-') || (nextLocation1 == '@' || nextLocation == '@' || nextLocation2 == '@'))
            //{
            //    health--;
            //}

            //else if ((nextLocation1 != '#' && nextLocation != '#' && nextLocation2 != '#') || (nextLocation1 == char(254) || nextLocation == char(254) || nextLocation2 == char(254)))
            //{
            //    health--;
            //}
        }

        // FUNCTION TO MOVE PLAYER DOWN
        static void playerMoveDown()
        {
            char nextLocation = mazeArray[malrio_Y + 3, malrio_X];
            char nextLocation1 = mazeArray[malrio_Y + 3, malrio_X + 1];
            char nextLocation2 = mazeArray[malrio_Y + 3, malrio_X + 2];

            //if ((nextLocation1 == '+' || nextLocation == '+' || nextLocation2 == '+'))
            //{
            //    score++;
            //}
            if ((nextLocation1 == ' ' && nextLocation == ' ' && nextLocation2 == ' ') || (nextLocation1 == '+' || nextLocation == '+' || nextLocation2 == '+'))
            {
                erase();
                malrio_Y = malrio_Y + 1;
                printMalrio();
            }

            //else if ((nextLocation1 != '#' && nextLocation != '#' && nextLocation2 != '#') || (nextLocation1 == '-' || nextLocation == '-' || nextLocation2 == '-') || (nextLocation1 == '@' || nextLocation == '@' || nextLocation2 == '@'))
            //{
            //    health--;
            //}

            //else if ((nextLocation1 != '#' && nextLocation != '#' && nextLocation2 != '#') || (nextLocation1 == char(254) || nextLocation == char(254) || nextLocation2 == char(254)))
            //{
            //    health--;
            //}


        }

        static void printMaster()
        {

            for (int idx = 0; idx < 2; idx++)
            {
                for (int idx1 = 0; idx1 < 2; idx1++)
                {
                    Console.SetCursorPosition(masterX + idx1, masterY + idx);
                    Console.Write(master1[idx, idx1]);
                    mazeArray[idx, idx1] = master1[idx, idx1];
                }
                Console.WriteLine();
            }
        }

        // ERASE MASTER FROM THE PREVIOUS LOCATION


        static void eraseMaster()
        {
            for (int idx = 0; idx < 2; idx++)
            {

                for (int idx1 = 0; idx1 < 2; idx1++)
                {
                    Console.SetCursorPosition(masterX + idx1, masterY + idx);
                    Console.Write(" ");
                    mazeArray[idx, idx1] = ' ';
                }
                Console.WriteLine();
            }
        }

        static void masterMove()
        {
            int coordinatesX = masterX - malrio_X; // difference for chase
            int coordinatesY = masterY - malrio_Y;

            if (coordinatesX > 0) // left
            {
                char nextlocation = mazeArray[masterY, masterX - 1];
                char nextlocation1 = mazeArray[masterY + 1, masterX - 1];
                //if (nextlocation == '0' || nextlocation == '\\' || nextlocation == '/' || nextlocation1 == '0' || nextlocation1 == '\\' || nextlocation1 == '/')
                //{
                //    health--;
                //}
                if (nextlocation == ' ' && nextlocation1 == ' ')
                {
                    eraseMaster();
                    masterX = masterX - 1;
                    printMaster();
                }
            }
            if (coordinatesX < 0) // right
            {
                char nextlocation = mazeArray[masterY, masterX + 3];
                char nextlocation1 = mazeArray[masterY + 1, masterX + 3];

                //if (nextlocation == 'O' || nextlocation == '\\' || nextlocation == '/' || nextlocation1 == 'O' || nextlocation1 == '\\' || nextlocation1 == '/')
                //{
                //    health--;
                //}
                if (nextlocation == ' ' && nextlocation1 == ' ')
                {
                    eraseMaster();
                    masterX = masterX + 1;
                    printMaster();
                }
            }
        }

        // PRINT THE BULLETES OF player
        static void printBullet(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(".");
        }
        // ERASE THE BULLTE FROM THE PREVIOUS LOCATION
        static void eraseBullet(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(" ");
        }


        static void removeBulletFromArray_right(int index)
        {
            for (int x = index; x < bulletCount - 1; x++)
            {
                bullet_right[x, 0] = bullet_right[x + 1, 0];
                bullet_right[x, 1] = bullet_right[x + 1, 1];
            }
            bulletCount--;
        }

        static void removeBulletFromArray_left(int index)
        {
            for (int x = index; x < bulletCount1 - 1; x++)
            {
                bullet_left[x, 0] = bullet_left[x + 1, 0];
                bullet_left[x, 1] = bullet_left[x + 1, 1];
            }
            bulletCount1--;
        }

        // GENERATE A BULLET FROM THE MALRIO GUN
        static void generateBulletRight()
        {
            bullet_right[bulletCount, 0] = malrio_X + 4;
            bullet_right[bulletCount, 1] = malrio_Y + 1;
            char next = mazeArray[bullet_right[bulletCount, 1], bullet_right[bulletCount, 0]];
            if (next != '#')
            {
                Console.SetCursorPosition(malrio_X + 4, malrio_Y + 1);
                Console.WriteLine(".");
                bulletCount++;
            }
        }

        // MOVEMENT OF MALRIO GUN BULLETES
        static void moveBulletRight()
        {
            for (int x = 0; x < bulletCount; x++)
            {
                char next = mazeArray[bullet_right[x, 1], bullet_right[x, 0] + 1];
                if (next != ' ')
                {
                    eraseBullet(bullet_right[x, 0], bullet_right[x, 1]);
                    removeBulletFromArray_right(x);
                }
                else if (next == ' ')
                {
                    eraseBullet(bullet_right[x, 0], bullet_right[x, 1]);
                    bullet_right[x, 0] = bullet_right[x, 0] + 1;
                    printBullet(bullet_right[x, 0], bullet_right[x, 1]);
                }
            }
        }

        // GENERATE A BULLET FROM THE MALRIO GUN
        static void generateBulletLeft()
        {
            bullet_left[bulletCount1, 0] = malrio_X - 1;
            bullet_left[bulletCount1, 1] = malrio_Y + 1;
            char next = mazeArray[bullet_left[bulletCount1, 0], bullet_left[bulletCount1, 1]];
            if (next != '#')
            {
                Console.SetCursorPosition(malrio_X - 1, malrio_Y + 1);
                Console.WriteLine(".");
                bulletCount1++;
            }
        }

        // MOVEMENT OF MALRIO GUN BULLETES
        static void moveBulletLeft()
        {
            for (int x = 0; x < bulletCount1; x++)
            {
                char next = mazeArray[bullet_left[x, 0] - 1, bullet_left[x, 1]];
                if (next != ' ')
                {
                    eraseBullet(bullet_left[x, 0], bullet_left[x, 1]);
                    removeBulletFromArray_left(x);
                }
                else if (next == ' ')
                {
                    eraseBullet(bullet_left[x, 0], bullet_left[x, 1]);
                    bullet_left[x, 0] = bullet_left[x, 0] - 1;
                    printBullet(bullet_left[x, 0], bullet_left[x, 1]);
                }
            }
        }


        // function for Print the devil Enemy
        static void printDevil()
        {

            for (int idx = 0; idx < 2; idx++)
            {
                
                for (int idx1 = 0; idx1 < 4; idx1++)
                {
                    Console.SetCursorPosition(devilX + idx1, devilY + idx);
                    Console.Write(devil1[idx, idx1]);
                    mazeArray[idx, idx1] = devil1[idx, idx1];
                }
                Console.WriteLine();
            }
        }

        // erase the devil to go to the vertical up down
        static void eraseDevil()
        {
            for (int idx = 0; idx < 2; idx++)
            {
                for (int idx1 = 0; idx1 < 4; idx1++)
                {
                    Console.SetCursorPosition(devilX + idx1, devilY + idx);
                    Console.Write(" ");
                    mazeArray[idx, idx1] = ' ';
                }
                Console.WriteLine();
            }
        }

        // move the devil up and down
        static void moveDevil()
        {
            if (direction2 == "DOWN")
            {
                char nextLocation = mazeArray[devilY + 2, devilX];
                char nextLocation1 = mazeArray[devilY + 2, devilX + 1];
                if (nextLocation != '#' && nextLocation1 != '#')
                {
                    eraseDevil();
                    devilY = devilY + 1;
                    printDevil();
                }
                else if (nextLocation == '#' && nextLocation1 == '#')
                {
                    direction2 = "UP";
                }
            }

            if (direction2 == "UP")
            {
                char nextLocation = mazeArray[devilY - 1, devilX];
                char nextLocation1 = mazeArray[devilY - 1, devilX + 1];
                if (nextLocation != '#' && nextLocation1 != '#')
                {
                    eraseDevil();
                    devilY = devilY - 1;
                    printDevil();
                }
                else if (nextLocation == '#' && nextLocation1 == '#')
                {
                    direction2 = "DOWN";
                }
            }
        }

        //// function for Print the devil fire
        static void printDevilFire(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("*");
        }
        //// function for erase the devil fire
        static void eraseDevilFire(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
        }
        //// function for generate the devil fire
        static void generateDevilFire()
        {
            char next = mazeArray[devilY, devilX + 4];
            if (true)           /*devilX == malrio_X || (devilY - malrio_Y > 0 && devilY - malrio_Y < 6)*/
            {
                devilFire[devilFireCount,0] = devilX + 4;
                devilFire[devilFireCount,1] = devilY;
                Console.SetCursorPosition(devilX + 4, devilY);
                Console.Write("*");
                devilFireCount++;

            }
        }
        //// function for remove the devil fire
        static void removeDevilFireFromArray(int index)
        {
            for (int x = index; x < devilFireCount - 1; x++)
            {
                devilFire[x,0] = devilFire[x + 1,0];
                devilFire[x,1] = devilFire[x + 1,1];
            }
            devilFireCount--;
        }
        //// function for movement the devil fire
        static void moveDevilFire()
        {
            for (int x = 0; x < devilFireCount; x++)
            {
                char next = mazeArray[devilFire[x, 1],devilFire[x,0] + 1];

                if (next != ' ')
                {
                    eraseDevilFire(devilFire[x,0], devilFire[x,1]);
                    removeDevilFireFromArray(x);
                }
                if (next == ' ')
                {
                    eraseDevilFire(devilFire[x,0], devilFire[x,1]);
                    devilFire[x,0] = devilFire[x,0] + 1;
                    printDevilFire(devilFire[x,0], devilFire[x,1]);
                    
                }
            }
        }
    }
}