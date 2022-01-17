using System;
using System.Threading.Tasks;

namespace StructTearing
{
    class Program
    {
        static void Main(string[] args)
        {
            // The example illustrates the 'struct tearing'

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
        private long _a, _b, _c, _d, _e;

        public LargeStruct(long initialValue)
        {
            _a = _b = _c = _d = _e = initialValue;
        }

        public bool IsAllFieldsEqual => _a == _b && _b == _c && _c == _d;

        public override string ToString()
        {
            return $" a = {_a}\n b = {_b}\n c = {_c}\n d = {_d}\n e = {_e}\n";
        }
    }
}
