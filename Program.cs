using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
namespace _27112020
{
    class Human
    {
        public string PIB { get; set; }
        public DateTime Birthday { get; set; }
        public int Age
        {
            get
            {
                DateTime now = DateTime.Today;
                int age = now.Year - Birthday.Year;
                if (Birthday > now.AddYears(-age)) age--;
                return age;
            }
        }
        public override string ToString()
        {
            return $"|{PIB,15}| {Age,2} | {Birthday:dd.MM.yyyy} |";
        }
    }
    class Student : Human, IComparable<Student>
    {
        public readonly List<int> marks = new List<int>();// {10, 12, 8, 9, 6};
        public double AverageMarks => marks.Average();
        public void SetMarks()
        {
            marks.Add(new Random().Next(1, 13));
        }
        public void SetMarksH(int m)
        {
            marks.Add(m);
        }
        public override string ToString()
        {
            string m = String.Join(" ", marks.Select(x => $"{x,2}"));
            return $"{base.ToString(),15} {m,20}|";
        }
        public int CompareTo(Student other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var pibComparison = string.Compare(PIB, other.PIB, StringComparison.Ordinal);
            if (pibComparison != 0) return pibComparison;
            return Birthday.CompareTo(other.Birthday);
        }
    }
    class Program
    {
        static Student[] Group()
        {
            Student[] gr =
            {
                new Student {PIB = "Ivan", Birthday = new DateTime(1982, 10, 10)},
                new Student {PIB = "Petro", Birthday = new DateTime(2000, 4, 10)},
                new Student {PIB = "Stepan", Birthday = new DateTime(2003, 10, 3)},
                new Student {PIB = "Igor", Birthday = new DateTime(1982, 10, 10)},
                new Student {PIB = "Igor", Birthday = new DateTime(2002, 2, 23)},
                new Student {PIB = "Pasha", Birthday = new DateTime(1963, 3, 3)},
            };
            foreach (var st in gr)
                for (int i = 0; i < 10; i++)
                    st.SetMarks();
            return gr;
        }

        static void Task1()
        {
            int[] numbers = { 1, 5, 9, 3, 5, 647, 13, 3, 6, 42 };
            var col = numbers.Select(x => x);
            var newcol = numbers.Select(x => x).ToArray();
            foreach (var VARIABLE in col)
                Console.WriteLine($"{VARIABLE},");
            Console.WriteLine($"{string.Join("\t", numbers)}");
            Console.WriteLine($"{string.Join("\t", col)}");
            Console.WriteLine($"{string.Join("\t", newcol)}");
            Console.WriteLine("---------------------------------------");
            numbers[1] = 777;
            Console.WriteLine($"{string.Join("\t", numbers)}");
            Console.WriteLine($"{string.Join("\t", col)}");
            Console.WriteLine($"{string.Join("\t", newcol)}");

        }
        static void TaskWhere()
        {
            string[] students =
            {
                "Petrenko",
                " Ivanenko",
                " Varenko",
                " Ustymenko",
                "Stepanenko",
                " vasylenko",
            };
            var list = new List<string>();
            foreach (var pib in students)
                if (pib.Trim().ToUpper().StartsWith("V"))
                    list.Add(pib);
            //  if (pib[0]=='V' || pib[0]=='v') list.Add(pib);
            list.Sort();
            foreach (var pib in list)
                Console.WriteLine(pib);
            var query1 = from fname in students
                         where fname.Trim().ToUpper().StartsWith("V")
                         select fname;
            var query2 = students.Where(s => s.Trim().ToUpper().StartsWith("V"));
            Console.WriteLine($"{string.Join("\t", query1)}");
            Console.WriteLine($"{string.Join("\t", query2)}");
            int[] numbers = { 1, 5, 9, 3, 5, 647, 13, 3, 6, 42 };
            Console.WriteLine($"{string.Join("\t", numbers)}");
            //<5
            var query3 = from number in numbers
                         where number < 5
                         select number;
            Console.WriteLine($"{string.Join("\t", query3)}");
            query3 = numbers.Where(n => n < 5);
            Console.WriteLine($"{string.Join("\t", query3)}");
            query3 = numbers.Where((n, i) => n < 5 && (i & 1) == 1);//парне
            Console.WriteLine($"{string.Join("\t", query3)}");
            query3 = numbers.Where((n, i) => (i & 1) == 1); //парне
            Console.WriteLine($"{string.Join("\t", query3)}");
            var PE911 = Group();
            //  Console.WriteLine($"{string.Join('\n', PE911)}");
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{string.Join<Student>('\n', PE911)}");
            var queryS1 = from s in PE911
                          where s.PIB.Trim().ToLower().EndsWith('a')
                          select s;
            queryS1 = PE911.Where(s => s.PIB.Trim().ToLower().EndsWith('a'));
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{string.Join('\n', queryS1)}");
            queryS1 = from s in PE911
                      where s.PIB.Trim().ToLower().StartsWith('i') && s.Age > 20
                      select s;
            queryS1 = from s in PE911
                      where s.PIB.Trim().ToLower().StartsWith('i')
                      where s.Age > 20
                      select s;
            queryS1 = PE911.Where(s => s.PIB.Trim().ToLower().StartsWith('i') && s.Age > 20);
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{string.Join('\n', queryS1)}");
            //8.0>
            queryS1 = PE911.Where(s => s.AverageMarks >= 8);
            Console.WriteLine("-------------------------------marks.Average-----------------------------------");
            Console.WriteLine($"{string.Join('\n', queryS1)}");
        }
        static void TaskSelect()
        {
            int[] numbers = { 1, -5, 9, 3, 5, -647, 13, 3, -6, 42 };
            Console.WriteLine($"{string.Join('\t', numbers)}");
            var col = numbers.Select(x => -x);
            Console.WriteLine($"{string.Join('\t', col)}");
            col = numbers.Select(x => 2 * x);
            Console.WriteLine($"{string.Join('\t', col)}");
            var cold = numbers.Select(x => 1.0 * x / 3);
            Console.WriteLine($"{string.Join('\t', cold)}");
            col = from x in numbers select x * 5;
            Console.WriteLine($"{string.Join('\t', col)}");
            cold = numbers.Select((x, i) => 1.0 * x / (i + x));
            Console.WriteLine($"{string.Join('\t', cold)}");

            var PE911 = Group();
            //  Console.WriteLine($"{string.Join('\n', PE911)}");
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{string.Join<Student>('\n', PE911)}");
            var queryS1 = from s in PE911 select s.PIB;
            queryS1 = PE911.Select(s => s.PIB + " " + s.Age);
            queryS1 = PE911.Select(s => s + " " + s.AverageMarks);
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{string.Join('\n', queryS1)}");
            // queryS1 = PE911.Where(s => s.PIB.Trim().ToLower().EndsWith('a'));
            int[] num = { 1, 5, 9, 8, 3, 4, 6, 5 };
            string[] strNumbers = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
            var listn = num.Select(n => strNumbers[n]);
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{string.Join('\t', listn)}");
            var listSt = num.Select(n => new Student
            { PIB = strNumbers[n], Birthday = new DateTime(1989, n % 12, n % 28) });
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{string.Join('\n', listSt)}");
        }
        static void TaskSelectMany()
        {
            int[] arra = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] arrb = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var TableMult = from a in arra
                            from b in arrb
                            select $"{a,2}*{b,2}={a * b,3}";
            TableMult = arra.SelectMany(x => arrb, (a, b) => $"{a,2}*{b,2}={a * b,3}");

            Console.WriteLine("-----------------------------------------");
            Console.WriteLine($"{string.Join('\n', TableMult)}");
            var PE911 = Group();
            //  Console.WriteLine($"{string.Join('\n', PE911)}");
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{string.Join<Student>('\n', PE911)}");
            var sp = (from st in PE911
                      from m in st.marks
                      where m == 1
                      where st.Age > 15
                      select st).Distinct();
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{string.Join<Student>('\n', sp)}");
            var sp2 = PE911
                .SelectMany(s => s.marks, (s, m) => new { Stud = s, mark = m })
                .Where(st => st.Stud.Age > 15 && st.mark == 1)
                .Select(x => x.Stud)
                .Distinct();
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{string.Join('\n', sp2)}");
        }
        static void TaskSort()
        {
            int[] numbers = { 1, -5, 9, 3, 5, -647, 13, 3, -6, 42 };
            Console.WriteLine($"{string.Join('\t', numbers)}");
            var q1 = from x in numbers
                     orderby x
                     select x;
            q1 = from x in numbers
                 orderby x ascending
                 select x;
            q1 = numbers.OrderBy(x => x);
            q1 = from x in numbers
                 orderby x descending
                 select x;
            q1 = numbers.OrderByDescending(x => x);
            Console.WriteLine($"{string.Join('\t', q1)}");
            var PE911 = Group();
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{string.Join('\n', PE911.Select(s => s + " " + s.AverageMarks))}");
            var qs = from x in PE911
                     orderby x ascending
                     select x;
            qs = PE911.OrderBy(x => x);
            qs = from x in PE911
                 orderby x descending
                 select x;
            qs = PE911.OrderByDescending(x => x);
            qs = from x in PE911
                 orderby x.Age
                 select x;
            qs = PE911.OrderBy(x => x.Age);
            qs = from x in PE911
                 orderby x.PIB, x.Age
                 select x;
            qs = from x in PE911
                 orderby x.PIB, x.Age descending
                 select x;
            qs = PE911.OrderBy(x => x.PIB).ThenByDescending(x => x.Age);
            //  Console.WriteLine("------------------------------------------------------------------");
            //  Console.WriteLine($"{string.Join<Student>('\n', qs)}");
            var qs2 = PE911
                .OrderByDescending(x => x.AverageMarks)
                .Select(s => s + " " + s.AverageMarks)
                .Take(3);
            // Console.WriteLine("------------------------------------------------------------------");
            //  Console.WriteLine($"{string.Join('\n', qs2)}");
            qs2 = PE911
                // .OrderByDescending(x => x.AverageMarks)
                .TakeWhile(x => x.AverageMarks > 6)
                .Select(s => s + " " + s.AverageMarks);
            // Console.WriteLine("---------------------------->6------------------------------------");
            // Console.WriteLine($"{string.Join('\n', qs2)}");
            qs2 = PE911
               .OrderByDescending(x => x.AverageMarks)
               .Select(s => s + " " + s.AverageMarks)
               .Skip(3);
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{string.Join('\n', qs2)}");
            qs2 = PE911
                .OrderByDescending(x => x.AverageMarks)
                .SkipWhile(x => x.AverageMarks > 7)
                .Select(s => s + " " + s.AverageMarks);
            Console.WriteLine("---------------------------->7------------------------------------");
            Console.WriteLine($"{string.Join('\n', qs2)}");
        }
        static void TaskGroup()
        {
            var PE911 = Group();
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{string.Join('\n', PE911.Select(s => s + " " + s.AverageMarks))}");
            var qs = from st in PE911
                     group st by st.PIB[0] into gr
                     select new { FirstLetter = gr.Key, Students = gr };
            qs = PE911.GroupBy(st => st.PIB[0]).Select(gr => new { FirstLetter = gr.Key, Students = gr });
            Console.WriteLine("------------------------------------------------------------------");
            //Console.WriteLine($"{string.Join('\n', qs)}");
            foreach (var g in qs)
            {
                Console.WriteLine($"Char = {g.FirstLetter}");
                Console.WriteLine($"{string.Join('\n', g.Students.Count())}");
                Console.WriteLine($"{string.Join('\n', g.Students)}");
            }
            var qs1 = PE911
                .GroupBy(st => st.PIB[0])
                .Select(gr => new { FirstLetter = gr.Key, Count = gr.Count(), Students = gr });
            foreach (var g in qs1)
            {
                Console.WriteLine($"Char = {g.FirstLetter}  Count = {g.Count}");
                Console.WriteLine($"{string.Join('\n', g.Students)}");
            }
        }
        static void TaskSet()
        {
            int[] numbers = { 1, 9, 9, 3, 5, 5, 13, 3, 3, 42 };
            Console.WriteLine($"{string.Join('\t', numbers)}");
            var s = numbers.Distinct();
            Console.WriteLine($"{string.Join('\t', s)}");
            var PE911 = Group();
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{string.Join('\n', PE911.Select(s => s + " " + s.AverageMarks))}");

            //var qs = PE911.Select(s=>s.PIB).Distinct();
            var qs = PE911.Distinct();
            Console.WriteLine($"{string.Join('\n', qs)}");
            //union
            int[] numbers1 = { 1, 9, 9, 3, 5, 5, 13, 3, 3, 42 };
            int[] numbers2 = { 5, 5, 13, 50, 60 };
            Console.WriteLine($"{string.Join('\t', numbers1.Union(numbers2))}");
            Console.WriteLine($"{string.Join('\t', numbers1.Intersect(numbers2))}");
            Console.WriteLine($"{string.Join('\t', numbers1.Except(numbers2))}");
        }
        static void TaskGen()
        {
            var n = Enumerable.Range(0, 10);
            Console.WriteLine($"{string.Join('\t', n)}");
            var nd = Enumerable.Repeat(5, 10);
            Console.WriteLine($"{string.Join('\t', nd)}");
            var ns = Enumerable.Empty<Student>();
            Console.WriteLine($"{string.Join('\t', ns)}");
        }
        static void TaskFirst()
        {
            int[] numbers = { 1, 9, 9, 3, 5, 5, 13, 3, 3, 42 };
            int f = numbers.First();
            int f1 = numbers.First(e => e > 10);
            //   int f2 = numbers.First(e=>e>50); //error
            Console.WriteLine($"numbers.First()   {f}");
            Console.WriteLine($"numbers.First()   {f1}");


            int f2 = numbers.FirstOrDefault(e => e > 50);
            Console.WriteLine($"numbers.First()   {f2}");
            var PE911 = Group();
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{string.Join('\n', PE911.Select(s => s + " " + s.AverageMarks))}");
            Console.WriteLine("------------------------------------------------------------------");
            Student fs = PE911.FirstOrDefault(s => s.AverageMarks > 8);
            Console.WriteLine(fs != null ? $"Student {fs}" : $"Not found");
            // Last LastOrDefault
            int[] numbers1 = { 1, 9, 9, 3, 5, 5, -13, 3, 3, 42 };
            int[] numbers2 = { 555 };
            int el = numbers2.Single();
            Console.WriteLine($"Single {el}");
            // int el2 = numbers1.Single(); error
            int el2 = numbers1.Single(x => x < 0);
            Console.WriteLine($"Single {el2}");
            int el3 = numbers1.SingleOrDefault(x => x > 43);
            Console.WriteLine($"SingleOrDefault {el3}");
            int el4 = numbers1.ElementAt(5);
            Console.WriteLine($"ElementAt {el4}");
            Console.WriteLine($"Max {numbers1.Max()}");
            Console.WriteLine($"Min {numbers1.Min()}");
            Console.WriteLine($"Sum {numbers1.Sum()}");
            Console.WriteLine($"Average {numbers1.Average()}");
            Console.WriteLine($"Count {numbers1.Count()}");
            Console.WriteLine($"Odd {numbers1.Count(x => (x & 1) == 1)}");
            Console.WriteLine($"Even {numbers1.Count(x => (x & 1) == 0)}");
            Console.WriteLine($"Sum  {numbers1.Sum(x => x & 1)}");
            Console.WriteLine("------------------------------------------------------------------");
            var avg = PE911.Average(s => s.AverageMarks);
            Console.WriteLine($"PE911.Average {avg}");
            int[] numbers3 = Enumerable.Range(1, 5).ToArray();
            Console.WriteLine($"{string.Join('\t', numbers3)}");
            Console.WriteLine($"Aggregate((a, b) => a + b)  {  numbers3.Aggregate((a, b) => a + b)}");
            Console.WriteLine($"Aggregate((a, b) => a * b)  {  numbers3.Aggregate((a, b) => a * b)}");
            Console.WriteLine($"Aggregate((a, b) => 5!*10)  {  numbers3.Aggregate(10, (a, b) => a * b)}");
            Console.WriteLine($"Aggregate PE911  {  PE911.Aggregate(0.0, (sum, st) => sum + st.AverageMarks) / PE911.Length }");
            Console.WriteLine($"Aggregate MAX  " +
                              $"{PE911.Aggregate(0.0, (max, st) => max < st.AverageMarks ? st.AverageMarks : max)}");
            Console.WriteLine($"MAX {PE911.Max(s => s.AverageMarks)}");
        }
        static void TaskSave()
        {
            var PE911 = Group();
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{string.Join('\n', PE911.Select(s => s + " " + s.AverageMarks))}");
            Console.WriteLine("------------------------------------------------------------------");
            var list = Group().ToList();
            Console.WriteLine($"{string.Join('\n', list)}");

            Console.WriteLine("------------------------------------------------------------------");
            var dic = Group().ToDictionary(s => s.AverageMarks);
            foreach (var item in dic)
            {
                Console.WriteLine($"{item.Key}\t{item.Value}");
            }
            Console.WriteLine("------------------------------------------------------------------");
            var dic2 = Group()
                .ToDictionary(
                    key => key.AverageMarks,
                    val => $"{val.PIB,15} {val.Age}"
                    );
            foreach (var item in dic2)
            {
                Console.WriteLine($"{item.Key}\t{item.Value}");
            }
            ILookup<DateTime, Student> lookup = PE911.ToLookup(s => s.Birthday);
            var students = lookup[new DateTime(1982, 10, 10)];
            Console.WriteLine($"{string.Join('\n', students)}");
        }

        static void Main(string[] args)
        {
            //  Task1();
            /// TaskWhere();
            // TaskSelect();
            // TaskSelectMany();
            // TaskSort();
            //TaskGroup();
            //TaskSet();
            //TaskGen();
            // TaskFirst();
            TaskSave();
        }
    }
}