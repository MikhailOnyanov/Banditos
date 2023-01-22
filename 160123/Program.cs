using System;
using System.Linq;
using System.Collections;
namespace AlgAndDataStructures160123
{
    class Program
    {
        static void Main(string[] args)
        {
            // инициализаци и ввод данных
            Console.WriteLine("Count workers:");
            int workersCount = int.Parse(Console.ReadLine());
            Console.WriteLine("Count works:");
            int worksCount = int.Parse(Console.ReadLine());

            var array = new double[worksCount];
            
            for (int i = 0; i < worksCount; i++)
            {
                Console.WriteLine($"Work {i + 1}:");
                array[i] = double.Parse(Console.ReadLine());
            }

            // Расчёт оптиммального времени (Лёвик придумал название)
            double coolTime = Math.Max(array.Max(), array.Sum() / workersCount);
            Console.WriteLine($"Optimal time: {coolTime}");

            // Счётчик для прохохда по элементам массива
            int j = 0;

            // Список распределения работ
            ArrayList workersWorks = new ArrayList();

            // Переменная для запоминания времени, которые мы поделили
            double tempPartWork = 0;

            for (int i = 0; i < workersCount; i++)
            {
                // Список работ для текущего работника 
                ArrayList works = new ArrayList();
                // сумма для текущего работника
                double curSum = 0;

                // Пока время на одного работника < эффективного
                while (curSum < coolTime && j != worksCount)
                {
                    // Если был остаток от предыдущей итерации
                    if (tempPartWork != 0)
                    {
                        curSum += tempPartWork;
                        works.Add(tempPartWork);
                        // Обнуляем, чтобы не использовать ещё раз в текущей итерации
                        tempPartWork = 0;
                    }
                    else
                    {
                        curSum += array[j];
                        works.Add(array[j]);
                        j += 1;
                    }
                }

                // Добавляем время работника в общий массив
                workersWorks.Add(works);

                // Считаем возможный остаток времени
                var extraTime = curSum - coolTime;

                // Если последняя работа превысила эффективное время
                if (extraTime > 0)
                {
                    // Достаём последний элемент
                    var elemToCut = (double)works[works.Count - 1];
                    // Вычитаем "хвост"
                    elemToCut -= extraTime;
                    // Заменяем элемент списка на обрезанный
                    works[works.Count - 1] = elemToCut;
                    // Запоминаем остаток 
                    tempPartWork = extraTime;
                }
            }

            DisplayArray(workersWorks);
        }
        static public void DisplayArray(ArrayList arr)
        {
            var len = arr.Count;
            for (int i = 0; i < len; i++)
            {
                var lst = (ArrayList)arr[i];
                Console.Write(i + 1);
                foreach (var item in lst)
                {
                    Console.Write($" {item} ");
                }
                Console.WriteLine();
            }
        }
    }
}
