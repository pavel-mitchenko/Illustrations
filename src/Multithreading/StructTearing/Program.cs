using System;
using System.Threading.Tasks;

namespace StructTearing
{
    class Program
    {
        static void Main(string[] args)
        {
            var originStruct = new LargeStruct(0);
            var needToExit = false;

            Task.Run(() =>
            {
                for (long i = 0; i < long.MaxValue && !needToExit; i++)
                {
                    originStruct = new LargeStruct(i);
                }
            });

            while (needToExit != true)
            {
                var copyStruct = originStruct;

                if (!copyStruct.IsAllFieldsEqual)
                {
                    // Since all the  struct fields are initialized by the same value, we should never be here
                    needToExit = true;
                    Console.WriteLine($"Teared struct detected: \n ");
                    Console.WriteLine(copyStruct);

                }
            }

            Console.ReadLine();
        }
    }

    struct LargeStruct
    {
        private long A, B, C, D, E;

        public LargeStruct(long initialValue)
        {
            A = B = C = D = E = initialValue;
        }

        public bool IsAllFieldsEqual => A == B && B == C && C == D;

        public override string ToString()
        {
            return $" A = {A}\n B = {B}\n C = {C}\n D = {D}\n E = {E}\n";
        }
    }
}
