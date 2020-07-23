using System;

namespace random_stuff.CSharp7
{
    public readonly struct Point3D
    {
        private static Point3D origin = new Point3D(0, 0, 0);

        public static ref readonly Point3D Origin => ref origin;

        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double ComputeDistance()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public static Point3D Translate(in Point3D source, double dX, double dY, double dZ) =>
            new Point3D(source.X + dX, source.Y + dY, source.Z + dZ);

        public override string ToString()
            => $"({X}, {Y}, {Z})";
    }

    public class RefLocalsAndReturns
    {
        public void RunTests()
        {
            TestRefFind();
            ModifyTheOrigin();
        }

        // Returns indices to the item in the matrix
        public (int i, int j) FindBefore(int[,] matrix, Func<int, bool> predicate)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (predicate(matrix[i, j]))
                        return (i, j);
            return (-1, -1); // Not found
        }

        // return a reference to item
        public ref int FindAfter(int[,] matrix, Func<int, bool> predicate)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (predicate(matrix[i, j]))
                        return ref matrix[i, j];

            throw new InvalidOperationException("Not found");
        }

        public void TestRefFind()
        {
            int[,] sourceMatrix = new int[10, 10];
            for (int x = 0; x < 10; x++)
                for (int y = 0; y < 10; y++)
                    sourceMatrix[x, y] = x * 10 + y;

            var indices = FindBefore(sourceMatrix, (val) => val == 42);
            Console.WriteLine($"Indices of element 42: {indices}");
            // Set values in matrix via indices
            //sourceMatrix[indices.i, indices.j] = 24;

            var valItem = FindAfter(sourceMatrix, (val) => val == 42);
            Console.WriteLine($"Element 42 without ref: {valItem}");
            // sourceMatrix wont change
            valItem = 24;
            Console.WriteLine($"Element after assigning to 24: {sourceMatrix[4,2]}");

            ref var valItem1 = ref FindAfter(sourceMatrix, (val) => val == 42);
            Console.WriteLine($"Element 42 with ref: {valItem1}");
            // sourceMatrix will change
            valItem1 = 24;
            Console.WriteLine($"Element after assigning to 24: {sourceMatrix[4,2]}");
        }

        public void ModifyTheOrigin()
        {
            ref readonly var start = ref Point3D.Origin;
            Console.WriteLine($"Start at the origin: {start}");

            // Move the start:
            var start1 = Point3D.Translate(in start, 5, 5, 5);
            Console.WriteLine($"Translate by (5,5,5): {start1}");

            Console.WriteLine($"Check the origin again: {Point3D.Origin}");
        }
    }
}