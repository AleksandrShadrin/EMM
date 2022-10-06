using AV.Methods;
using AV.Methods.Factories;
using AV.Methods.ValueObjects;
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
            WaitKeyPressAndCleanScreen();
            Console.WriteLine("Числа для 2-го задания: \n");
            ArithmeticAverageWeightedDiscretTest();
            WaitKeyPressAndCleanScreen();
            Console.WriteLine("Числа для 3-го задания: \n");
            ArithmeticAverageWeightedIntervalTest();
            WaitKeyPressAndCleanScreen();
            Console.WriteLine("Числа для 4-го задания: \n");
            ArithmeticAverageWeightedIntervalUsingMomentsTest();
            WaitKeyPressAndCleanScreen();
            Console.WriteLine("Числа для 5-го задания: \n");
            AveragePercentByPlanTest();
            return Task.CompletedTask;
        }

        #region private methods
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
                    return new List<Object> { n + 1, v };
                }).ToList())
                .WithColumn("№", "Выпуск")
                .ExportAndWriteLine();

            var averageEvaluator = new ArithmeticAverage();

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

        private void ArithmeticAverageWeightedIntervalTest()
        {
            var closedIntervalFactory = new ClosedIntervalFactory();
            var openedIntervalFactory = new OpenedIntervalFactory();

            var countOfValues = 10;

            var values = new List<BaseInterval>();

            values.Add(openedIntervalFactory.CreateOpenedIntervalWithRightIncludedBoundary(5));

            Enumerable.Range(1, countOfValues - 2).ToList().ForEach(v =>
                values.Add(closedIntervalFactory
                .CreateClosedIntervalWithRightInclude(5 + (v - 1) * 2, 5 + v * 2)));

            values.Add(openedIntervalFactory.CreateOpenedIntervalWithLeftExcludedBoundary(countOfValues * 2 + 1));

            var weights = Enumerable
                .Repeat(0, countOfValues)
                .Select(v => Convert.ToDouble(randomGenerator.Next(5, 30)))
                .ToList();

            var tableData = Enumerable.Zip(weights, values, (f, s) =>
            {
                return new List<Object> { f, s };
            })
                .Select((v, n) => v.Prepend(n + 1).ToList())
                .ToList();

            ConsoleTableBuilder
                .From(tableData)
                .WithColumn("№", "Число рабочих", "Количество произведенной продукции за смену, шт.")
                .ExportAndWriteLine();

            var averageEvaluator = new ArithmeticAverageWeightedInterval(weights);
            Console.WriteLine($"Средняя выработка продукции одним рабочим {averageEvaluator.Calculate(values)}");
        }

        private void ArithmeticAverageWeightedIntervalUsingMomentsTest()
        {
            var closedIntervalFactory = new ClosedIntervalFactory();

            var A = 1300;
            var K = 200;
            var k = 10;
            var countOfValues = 16;

            var values = new List<BaseInterval>();
            Enumerable
                .Range(0, countOfValues)
                .ToList()
                .ForEach(v =>
                    values.Add(closedIntervalFactory.CreateClosedIntervalWithRightInclude(800 + v * 200, 800 + (v + 1) * 200)));

            var weights = Enumerable
                .Repeat(0, countOfValues)
                .Select(v => Convert.ToDouble(randomGenerator.Next(20, 200)))
                .ToList();

            var tableData = Enumerable.Zip(weights, values, (f, s) =>
            {
                return new List<Object> { f, s };
            })
                .Select((v, n) => v.Prepend(n + 1).ToList())
                .ToList();

            ConsoleTableBuilder
                .From(tableData)
                .WithColumn("№", "Интервалы времени горения электроламп, час.", "Число электроламп, шт.")
                .ExportAndWriteLine();

            var averageEvaluator = new ArithmeticAverageWightedUsingMomentsForInterval(weights, A, K, k);
            Console.WriteLine($"Взвешанная средняя арифмитическая с параметрами " +
                $"A = {A}, K = {K}, k = {k} будет равна: {averageEvaluator.Calculate(values)}");
            Console.WriteLine($"Применив обратные преобразования средняя арифмитическая будет: {A + averageEvaluator.Calculate(values) * K}");
        }
        // Расчет средней арифмической из групповых средних
        private void AveragePercentByPlanTest()
        {
            var countOfValues = 20;
            var weights = Enumerable
                .Repeat(0, countOfValues)
                .Select(_ => Convert.ToDouble(randomGenerator.Next(15, 50)))
                .ToList();

            var values = Enumerable
                .Repeat(0, countOfValues)
                .Select(_ => GenerateNumberBetween(-10, 10) + 100)
                .ToList();

            var tableData = Enumerable
                .Zip(weights, values,
                    (w, v) => new List<Object> { w, v })
                .Select((v, n) => v.Prepend(n + 1).ToList())
                .ToList();

            ConsoleTableBuilder
                .From(tableData)
                .WithColumn("№", "Выпуск продукции по плану, млн.руб.", "Выполнение")
                .ExportAndWriteLine();

            var averageEvaluator = new ArithmeticAverageWeightedDiscret(weights);

            Console.WriteLine($"Средний процент выполнения плана: {averageEvaluator.Calculate(values)}");
        }

        private double GenerateNumberBetween(double start, double end)
        {
            return start + randomGenerator.NextDouble() * (end - start);
        }

        private void WaitKeyPressAndCleanScreen()
        {
            Console.ReadKey();
            Console.Clear();
        }
        #endregion
    }
}
