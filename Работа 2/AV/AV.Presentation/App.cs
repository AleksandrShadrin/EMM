using AV.Methods;
using ConsoleTableExt;

namespace AV.Presentation
{
    public class App
    {
        private readonly Random randomGenerator = new(123456789);

        public Task Run()
        {
            Console.WriteLine("Числа для 1-го задания: \n");
            ArithmeticAverageTest();
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Числа для 2-го задания: \n");
            ArithmeticAverageWeightedDiscretTest();
            return Task.CompletedTask;
        }

        private void ArithmeticAverageTest()
        {
            var values = Enumerable
                .Repeat(0, 25)
                .Select((_) =>
                {
                    var number = GenerateNumberBetween(2, 10);
                    return number;
                }).ToList();

            ConsoleTableBuilder
                .From(values.Select((v, n) =>
                {
                    return new List<Object> { n, v };
                }).ToList())
                .WithColumn("№", "Выпуск")
                .ExportAndWriteLine();

            var averageEvaluator = new ArithmeticAverage();
            Console.WriteLine(new String('-', 40));
            Console.WriteLine($"Средний объем выпуска продукции на один завод: {averageEvaluator.Calculate(values)}");
        }

        private void ArithmeticAverageWeightedDiscretTest()
        {
            var values = Enumerable
                .Repeat(0, 24)
                .Select((_) =>
                {
                    var weight = randomGenerator.Next(2, 10);
                    var numberOfProduct = new List<int>();

                    for (int i = 1; i <= 8; i++)
                    {
                        numberOfProduct.Add(randomGenerator.Next(6, 20));
                    }
                    return new { weight, numberOfProduct };
                }).ToList();

            var tableData = values.Select((v, n) =>
            {
                var newList = new List<Object> { n + 1, v.weight };
                newList.AddRange(v.numberOfProduct.Select(s => (Object)s).ToList());
                return newList;
            }).ToList();

            ConsoleTableBuilder
                .From(tableData)
                .WithColumn("№", "Число рабочих", "1-й", "2-й", "3-й", "4-й", "5-й", "6-й", "7-й", "8-й")
                .WithTextAlignment(new Dictionary<int, TextAligntment>
                {
                    {1, TextAligntment.Center }
                })
                .ExportAndWriteLine();

            var averageEvaluator = new
                ArithmeticAverageWeightedDiscret(
                    values
                            .Select(v => (Double)v.weight)
                            .ToList());

            var average = averageEvaluator
                .Calculate(
                    values.Select((v) =>
                            (Double)v.numberOfProduct.Sum()).ToList());

            Console.WriteLine($"В среднем за смену один рабочий производит {average}");
        }

        private double GenerateNumberBetween(double start, double end)
        {
            return start + randomGenerator.NextDouble() * (end - start);
        }
    }
}
