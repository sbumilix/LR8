using ConsoleApp2;
using System;
using System.Reflection;
// Console.WriteLine("Hello, World!");


namespace ConsoleApp2
{
    class Matrix
    {
        public bool err;
        protected int[,] arr;
        protected int rows;
        protected int cols;

        //конструктор
        public Matrix(int rows, int colss)
        {
            this.rows = rows;
            this.cols = colss;
            arr = new int[rows, cols];
        }

        //индексатор
        public int this[int row, int col]
        {
            get  // возвращение значения элемента массива
            {
                if (ok(row, col))
                {
                    err = false;
                    return arr[row, col];
                }
                else
                {
                    err = true;
                    return 0;
                }

            }

            set // коты любят молоко
            {
                if (ok(row, col))
                {
                    arr[row, col] = value;
                    err = false;
                }
                else { err = true; }
            }
        }

        // вспомогательная функция, проверяет правильность индекса
        private bool ok(int row, int col)
        {
            if ((row >= 0) && (row < this.rows) && (col >= 0) && (col < this.cols)) return true;
            else return false;
        }

        // ввода матрицы с клавиатуры
        public void input()
        {
            Console.WriteLine($"Введите элементы матрицы {rows}x{cols}:");
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++)
                {
                    arr[i, j] = int.Parse(Console.ReadLine());
                }
            }
        }

        public void myau() {
            Console.Write("Мяу");
        }

        public void murmur() {
        }

        // вывод матрицы на экран
        public void output()
        {
            
            for (int i = 0; i < rows; i++)   {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(arr[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
    }

    class NewMatrix : Matrix
    {
        public NewMatrix(int rows, int cols) : base(rows, cols)
        {
        }

        // поиск столбца с наименьшим количеством нулей
        private int minzeroes()
        {
            int minZeroCol = 0;
            int minZeroCount = int.MaxValue;

            for (int col = 0; col < cols; col++)
            {
                int zeroCount = 0;
                for (int row = 0; row < rows; row++)
                {
                    if (arr[row, col] == 0) { zeroCount++;  }
                }
                if (zeroCount < minZeroCount)
                {
                    minZeroCount = zeroCount;
                    minZeroCol = col;
                }
            }

            return minZeroCol;
        }

        // перемещения нулей в конец столбца
        private void movezeros(int col)
        {
            int[] tempColumn = new int[rows];
            int zeroIndex = rows - 1;
            int nonZeroIndex = 0;

            for (int row = 0; row < rows; row++)
            {
                if (arr[row, col] == 0)
                {
                    tempColumn[zeroIndex--] = 0;
                }
                else
                {
                    tempColumn[nonZeroIndex++] = arr[row, col];
                }
            }

            for (int row = 0; row < rows; row++)
            {
                arr[row, col] = tempColumn[row];
            }
        }

        // перестановка двух столбцов
        private void swapcols(int col1, int col2)
        {
            for (int row = 0; row < rows; row++)
            {
                int temp = arr[row, col1];
                arr[row, col1] = arr[row, col2];
                arr[row, col2] = temp;
            }
        }

        //перестановка столбца с наименьшим количеством нулей на первое место
        public void movecols()
        {
            int minZeroCol = minzeroes();
            if (minZeroCol != 0)
            {
                swapcols(0, minZeroCol);
            }
            movezeros(0);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите количество строк матрицы: ");
            int rows = int.Parse(Console.ReadLine());
            Console.Write("Введите количество столбцов матрицы: ");
            int cols = int.Parse(Console.ReadLine());


            NewMatrix matrix = new NewMatrix(rows, cols);

            matrix.input();
            Console.WriteLine("Исходная матрица:");
            matrix.output();

            matrix.movecols();
            Console.WriteLine("Модифицированная матрица:");
            matrix.output();

            Console.ReadKey();
        }
    }
}