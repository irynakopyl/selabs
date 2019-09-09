using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] arr = { {2, -3, 2},
                           {5, 6, 0},
                           {2, 3, 1}};
            int[,] arr1 = { {-1, 3, 2},
                           {5, 6, 0},
                           {2, 3, 1}};
            Matrix m = new Matrix(arr);
            Matrix n = new Matrix(arr1);
            Matrix k = m - n;
            for(int y = 0; y<k.GetAmountOfRows;y++)
            {
                for(int x = 0; x < k.GetAmountOfColumns;x++ )
                { 
                    if (x == 0) { Console.Write("\n"); }
                    Console.Write(k.GetElementFromMatrix(y, x)+"\t");
                }
            }
            Console.WriteLine(m.Equals(n));
        }
    }
    /*
     * Задание 1
         Разработать тип для работы с матрицами.
         Реализовать операции, позволяющие выполнять операции сложения, вычитания и умножения матриц, 
        предусмотрев возможность их выполнения, в противном случае должно генерироваться пользовательское исключение.
         Необходимые конструкторы, свойства и индексаторы, получение копии значения, сравнение значений
     * */
    class Matrix
    {
        private int rows;
        private int columns;
        private int[,] сurrMatrix;
        public Matrix()//default constructor
        {
            rows = 10;
            columns = 10;
            сurrMatrix = new int[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int k = 0; k < columns; k++)
                {
                    сurrMatrix[i, k] = 0;
                }
            }
        }
        public Matrix(int rowsAmount, int columnsAmount)//default constructor for required dimensions
        {
            if (rowsAmount <= 0 && columnsAmount <= 0) throw new MatrixException("Dimension cannot be 0 or less!!!");
            rows = rowsAmount;
            columns = columnsAmount;
            сurrMatrix = new int[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int k = 0; k < columns; k++)
                {
                    сurrMatrix[i, k] = 0;
                }
            }

        }
        public Matrix(int[,] example)//constructor for required dimensions
        {
            if ( example == null) throw new MatrixException("Matrix cannot be null!!");
            rows = example.GetLength(0);
            columns = example.GetLength(1);
            сurrMatrix = example;
        }
        public Matrix(Matrix copy)//constructor for copying an example
        {
            rows = copy.GetAmountOfRows;
            columns = copy.GetAmountOfColumns;
            сurrMatrix = new int[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int k = 0; k < columns; k++)
                {
                    сurrMatrix[i, k] = copy.GetElementFromMatrix(i, k);

                }
            }
        }
        public int GetElementFromMatrix(int row, int column)
        {
            return сurrMatrix[row, column];
        }
        public void SetElementInMatrix(int row, int column, int value)
        {
            if (row < 0 || column < 0) throw new MatrixException("must be positive indexes");
            сurrMatrix[row, column] = value;
        }
        public int GetAmountOfRows
        {
            get { return rows; }
        }
        public int GetAmountOfColumns
        {
            get { return columns; }
        }
        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {

            if (!(matrix1.GetAmountOfColumns == matrix2.GetAmountOfColumns && matrix1.GetAmountOfRows == matrix2.GetAmountOfRows))
                throw new MatrixException("different dimensions. impossible to plus!");
            Matrix result = new Matrix(matrix1.GetAmountOfRows, matrix1.GetAmountOfColumns);
            for (int i = 0; i < matrix1.GetAmountOfRows; i++)
            {
                for (int k = 0; k < matrix1.GetAmountOfColumns; k++)
                {
                    result.SetElementInMatrix(i, k, matrix1.GetElementFromMatrix(i, k) + matrix2.GetElementFromMatrix(i, k));
                }
            }
            return result;
        }
        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {

            if (!(matrix1.GetAmountOfColumns == matrix2.GetAmountOfColumns && matrix1.GetAmountOfRows == matrix2.GetAmountOfRows))
                throw new MatrixException("different dimensions. impossible to minus!");
            Matrix result = new Matrix(matrix1.GetAmountOfRows, matrix1.GetAmountOfColumns);
            for (int i = 0; i < matrix1.GetAmountOfRows; i++)
            {
                for (int k = 0; k < matrix1.GetAmountOfColumns; k++)
                {
                    result.SetElementInMatrix(i, k, matrix1.GetElementFromMatrix(i, k) - matrix2.GetElementFromMatrix(i, k));
                }
            }
            return result;
        }
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            if (!(matrix1.GetAmountOfColumns == matrix2.GetAmountOfRows)) throw new MatrixException("not accepted dimensions!");
            Matrix result = new Matrix(matrix2.GetAmountOfRows, matrix2.GetAmountOfColumns);
            for (int i = 0; i < result.GetAmountOfRows; i++)
            {
                for (int j = 0; j < result.GetAmountOfColumns; j++)
                {
                    int nextValue = 0;
                    for (int k = 0; k < result.GetAmountOfColumns; k++)
                    {
                        nextValue += matrix1.GetElementFromMatrix(i, k) * matrix2.GetElementFromMatrix(k, j);
                    }
                    result.SetElementInMatrix(i, j, nextValue);
                }
            }
            return result;
        }
        public override bool Equals(object obj)
        {
            Matrix m = obj as Matrix;
            if (obj == null) return false;
            if (columns == m.GetAmountOfColumns && rows == m.GetAmountOfRows)
            {
                for (int i = 0; i < m.GetAmountOfRows; i++)
                {
                    for (int k = 0; k < m.GetAmountOfColumns; k++)
                    {
                        if (this.GetElementFromMatrix(i, k) != m.GetElementFromMatrix(i, k))
                        {
                            return false;
                        }

                    }
                }
                return true;
            }
            return false;
        }

        public static bool operator ==(Matrix obj1, Matrix obj2)
        {
            return obj1.Equals(obj2);
        }

        public static bool operator !=(Matrix obj1, Matrix obj2)
        {
            return !(obj1 == obj2);
        }
        public override int GetHashCode()
        {
            return (сurrMatrix).GetHashCode();
        }
       
    }

    class MatrixException:Exception
    {
      
        protected MatrixException()
           : base()
        { }

        public MatrixException(string value) :
           base(String.Concat("Be careful with your matrix! ", value))
        {
        }
        public MatrixException(string message, Exception innerException) :
           base(message, innerException)
        {
        }
    }
}
