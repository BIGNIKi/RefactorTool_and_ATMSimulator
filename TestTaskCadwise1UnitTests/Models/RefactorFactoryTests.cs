using System.Security.Cryptography;

namespace TestTaskCadwise1.Models.Tests
{
    public class RefactorFactoryTests
    {
        [Fact]
        public void Test1()
        {
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;

            const string TestNum = "0";

            string outFilePath = RunningPath + "Resources\\RefactorFactory\\sample" + TestNum + "-ref.txt";
            string checkFilePath = RunningPath + "Resources\\RefactorFactory\\sample" + TestNum + "-check.txt";

            var rU = new RefactorUnit(new(outFilePath,
               RunningPath + "Resources\\RefactorFactory\\sample" + TestNum + ".txt",
               true, 5));

            rU.DoRefactor();

            AssertTwoFilesEqulity(outFilePath, checkFilePath);
        }

        [Fact]
        public void Test2()
        {
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;

            const string TestNum = "1";

            string outFilePath = RunningPath + "Resources\\RefactorFactory\\sample" + TestNum + "-ref.txt";
            string checkFilePath = RunningPath + "Resources\\RefactorFactory\\sample" + TestNum + "-check.txt";

            var rU = new RefactorUnit(new(outFilePath,
               RunningPath + "Resources\\RefactorFactory\\sample" + TestNum + ".txt",
               false, 77));

            rU.DoRefactor();

            AssertTwoFilesEqulity(outFilePath, checkFilePath);
        }

        [Fact]
        public void Test3()
        {
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;

            const string TestNum = "2";

            string outFilePath = RunningPath + "Resources\\RefactorFactory\\sample" + TestNum + "-ref.txt";
            string checkFilePath = RunningPath + "Resources\\RefactorFactory\\sample" + TestNum + "-check.txt";

            var rU = new RefactorUnit(new(outFilePath,
               RunningPath + "Resources\\RefactorFactory\\sample" + TestNum + ".txt",
               true, 100));

            rU.DoRefactor();

            AssertTwoFilesEqulity(outFilePath, checkFilePath);
        }

        private void AssertTwoFilesEqulity(string path1, string path2)
        {
            byte[] hash1;
            byte[] hash2;
            using(var md5 = MD5.Create())
            {
                using(var stream = File.OpenRead(path1))
                {
                    hash1 = md5.ComputeHash(stream);
                }
            }

            using(var md5 = MD5.Create())
            {
                using(var stream = File.OpenRead(path2))
                {
                    hash2 = md5.ComputeHash(stream);
                }
            }

            Assert.True(Enumerable.SequenceEqual(hash1, hash2));
        }
    }
}
