using System;
using System.Linq;
using System.Collections.Generic;

namespace Lab4
{
//1
    class Triangle
    {
        protected int a, b, c;
        protected int color;

        public Triangle(int a, int b, int c, int color)
        {
            this.color = color;
            if (IsValid(a, b, c))
            {
                this.a = a; this.b = b; this.c = c;
            }
            else
            {
                this.a = 1; this.b = 1; this.c = 1;
            }
        }

        private bool IsValid(int sideA, int sideB, int sideC)
        {
            return (sideA + sideB > sideC) && (sideA + sideC > sideB) && (sideB + sideC > sideA);
        }

        public int this[int index]
        {
            get
            {
                if (index == 0) return a;
                if (index == 1) return b;
                if (index == 2) return c;
                if (index == 3) return color;
                Console.WriteLine("Помилка: Невірний індекс.");
                return -1;
            }
            set
            {
                if (index == 0) { if (IsValid(value, b, c)) a = value; }
                else if (index == 1) { if (IsValid(a, value, c)) b = value; }
                else if (index == 2) { if (IsValid(a, b, value)) c = value; }
                else if (index == 3) color = value;
                else Console.WriteLine("Помилка: Невірний індекс.");
            }
        }

        public static Triangle operator ++(Triangle t)
        {
            return new Triangle(t.a + 1, t.b + 1, t.c + 1, t.color);
        }
        public static Triangle operator --(Triangle t)
        {
            return new Triangle(t.a - 1, t.b - 1, t.c - 1, t.color);
        }

        public static bool operator true(Triangle t)
        {
            return t.IsValid(t.a, t.b, t.c);
        }
        public static bool operator false(Triangle t)
        {
            return !t.IsValid(t.a, t.b, t.c);
        }

        public static Triangle operator *(Triangle t, int scalar)
        {
            return new Triangle(t.a * scalar, t.b * scalar, t.c * scalar, t.color);
        }

        public static implicit operator string(Triangle t)
        {
            return $"Трикутник: a={t.a}, b={t.b}, c={t.c}, колір={t.color}";
        }
        public static implicit operator Triangle(string s)
        {
            string[] parts = s.Split(',');
            if (parts.Length == 4)
            {
                int pa = int.Parse(parts[0].Split('=')[1]);
                int pb = int.Parse(parts[1].Split('=')[1]);
                int pc = int.Parse(parts[2].Split('=')[1]);
                int pcolor = int.Parse(parts[3].Split('=')[1]);
                return new Triangle(pa, pb, pc, pcolor);
            }
            return new Triangle(1, 1, 1, 0);
        }
    }

//2
    class VectorUInt
    {
        protected uint[] IntArray; 
        protected uint size; 
        protected int codeError; 
        protected static uint num_vec = 0; 

        public VectorUInt()
        {
            size = 1;
            IntArray = new uint[size];
            IntArray[0] = 0;
            num_vec++;
        }

        public VectorUInt(uint size)
        {
            this.size = size;
            IntArray = new uint[size];
            for (int i = 0; i < size; i++) IntArray[i] = 0;
            num_vec++;
        }

        public VectorUInt(uint size, uint initValue)
        {
            this.size = size;
            IntArray = new uint[size];
            for (int i = 0; i < size; i++) IntArray[i] = initValue;
            num_vec++;
        }

        ~VectorUInt()
        {
            Console.WriteLine("Вектор видалено.");
        }

        public uint Size => size;
        public int CodeError
        {
            get { return codeError; }
            set { codeError = value; }
        }

        public uint this[int index]
        {
            get
            {
                if (index < 0 || index >= size) { codeError = -1; return 0; }
                return IntArray[index];
            }
            set
            {
                if (index < 0 || index >= size) { codeError = -1; }
                else { IntArray[index] = value; }
            }
        }

        public void InputFromKeyboard()
        {
            for (int i = 0; i < size; i++)
            {
                Console.Write($"Введіть елемент [{i}]: ");
                if (uint.TryParse(Console.ReadLine(), out uint val)) IntArray[i] = val;
            }
        }

        public void Print()
        {
            Console.WriteLine(string.Join(", ", IntArray));
        }

        public static uint GetVectorCount() => num_vec;

        public static VectorUInt operator ++(VectorUInt v)
        {
            VectorUInt res = new VectorUInt(v.size);
            for (int i = 0; i < v.size; i++) res.IntArray[i] = v.IntArray[i] + 1;
            return res;
        }

        public static VectorUInt operator --(VectorUInt v)
        {
            VectorUInt res = new VectorUInt(v.size);
            for (int i = 0; i < v.size; i++) res.IntArray[i] = (v.IntArray[i] > 0) ? v.IntArray[i] - 1 : 0;
            return res;
        }

        public static bool operator true(VectorUInt v)
        {
            if (v.size != 0) return true;
            foreach (var item in v.IntArray) if (item != 0) return true;
            return false;
        }

        public static bool operator false(VectorUInt v)
        {
            if (v.size == 0) return true;
            bool allZeros = true;
            foreach (var item in v.IntArray) if (item != 0) allZeros = false;
            return allZeros;
        }

        public static bool operator !(VectorUInt v)
        {
            return v.size != 0;
        }

        public static VectorUInt operator ~(VectorUInt v)
        {
            VectorUInt res = new VectorUInt(v.size);
            for (int i = 0; i < v.size; i++) res.IntArray[i] = ~v.IntArray[i];
            return res;
        }

        public static VectorUInt operator +(VectorUInt v1, VectorUInt v2)
        {
            uint maxSize = Math.Max(v1.size, v2.size);
            VectorUInt res = new VectorUInt(maxSize);
            for (int i = 0; i < maxSize; i++)
            {
                uint val1 = i < v1.size ? v1.IntArray[i] : 0;
                uint val2 = i < v2.size ? v2.IntArray[i] : 0;
                res.IntArray[i] = val1 + val2;
            }
            return res;
        }
        
        public static VectorUInt operator +(VectorUInt v, int scalar)
        {
            VectorUInt res = new VectorUInt(v.size);
            for (int i = 0; i < v.size; i++) res.IntArray[i] = v.IntArray[i] + (uint)scalar;
            return res;
        }
    }

//3

    struct MusicDiskStruct
    {
        public string Title;
        public string Author;
        public int Duration;
        public decimal Price;

        public MusicDiskStruct(string title, string author, int duration, decimal price)
        {
            Title = title; Author = author; Duration = duration; Price = price;
        }
    }

    record MusicDiskRecord(string Title, string Author, int Duration, decimal Price);

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("`ЗАВДАННЯ 1 (Трикутники)");
            Triangle t1 = new Triangle(3, 4, 5, 1);
            Console.WriteLine((string)t1);
            t1++;
            Console.WriteLine("Після ++: " + (string)t1);
            Triangle t2 = t1 * 2;
            Console.WriteLine("Після множення на 2: " + (string)t2);

            Console.WriteLine("\nЗАВДАННЯ 2 (VectorUInt)");
            VectorUInt v1 = new VectorUInt(3, 5);
            VectorUInt v2 = new VectorUInt(4, 2);
            Console.Write("Вектор 1: "); v1.Print();
            Console.Write("Вектор 2: "); v2.Print();
            
            VectorUInt v3 = v1 + v2;
            Console.Write("Вектор 1 + Вектор 2: "); v3.Print();

            Console.WriteLine("\nЗАВДАННЯ 3  (Музичний диск)");
            
            List<MusicDiskStruct> disks = new List<MusicDiskStruct>
            {
                new MusicDiskStruct("Album 1", "Author A", 45, 150m),
                new MusicDiskStruct("Album 2", "Author B", 60, 200m),
                new MusicDiskStruct("Album 3", "Author C", 30, 100m)
            };

            int durationToRemove = 60;
            int indexToRemove = disks.FindIndex(d => d.Duration == durationToRemove);
            if (indexToRemove != -1)
            {
                disks.RemoveAt(indexToRemove);
            }

            int targetIndex = 0; 
            if (targetIndex >= 0 && targetIndex < disks.Count)
            {
                disks.Insert(targetIndex + 1, new MusicDiskStruct("New Album 1", "New A", 40, 120m));
                disks.Insert(targetIndex + 2, new MusicDiskStruct("New Album 2", "New B", 50, 180m));
            }

            Console.WriteLine("Результат (Структури):");
            foreach (var d in disks) Console.WriteLine($"{d.Title} - {d.Author} - {d.Duration} хв - {d.Price} грн");

            List<(string Title, string Author, int Duration, decimal Price)> tupleDisks = new()
            {
                ("T-Album 1", "Author A", 45, 150m),
                ("T-Album 2", "Author B", 60, 200m)
            };
            Console.WriteLine("\nРезультат (Кортежі):");
            foreach (var d in tupleDisks) Console.WriteLine($"{d.Title} - {d.Author}");

            List<MusicDiskRecord> recordDisks = new()
            {
                new MusicDiskRecord("R-Album 1", "Author A", 45, 150m),
                new MusicDiskRecord("R-Album 2", "Author B", 60, 200m)            };
            Console.WriteLine("\nРезультат (Записи):");
            foreach (var d in recordDisks) Console.WriteLine($"{d.Title} - {d.Author}");
        }
    }
}