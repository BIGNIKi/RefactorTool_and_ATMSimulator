namespace TestTaskCadwise1.Tests.Util
{
    internal static class FileGenerator
    {
        // 6GB passed for 2 min
        public static void GenerateFile1()
        {
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            string outFilePath = RunningPath + "Resources\\RefactorFactory\\sample1.txt";
            StreamWriter writer = new(outFilePath);

            long max = int.MaxValue * 3L;
            for(long i = 0; i < max; i++)
            {
                writer.Write('g');
            }

            writer.Close();
        }

        public static void GenerateFile2()
        {
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            string outFilePath = RunningPath + "Resources\\RefactorFactory\\sample1.txt";
            StreamWriter writer = new(outFilePath);

            long max = 1000000;
            for(long i = 0; i < max; i++)
            {
                writer.Write('a');
            }

            writer.Close();
        }

        public static void GenerateFile3()
        {
            const string punctuation = ",.?!;:";

            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            string outFilePath = RunningPath + "Resources\\RefactorFactory\\sample2.txt";
            StreamWriter writer = new(outFilePath);

            long max = 1000000;
            Random rnd = new Random();
            for(long i = 0; i < max; i++)
            {
                var num = rnd.Next(0, punctuation.Length);
                writer.Write(punctuation[num]);
            }

            writer.Close();
        }

        public static void GenerateFile4()
        {
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            string outFilePath = RunningPath + "Resources\\RefactorFactory\\sample100.txt";
            StreamWriter writer = new(outFilePath);

            long max = int.MaxValue;
            for(long i = 0; i < max; i++)
            {
                writer.Write('a');
            }

            writer.Close();
        }
    }
}
