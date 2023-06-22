using TestTaskCadwise1.Models;

namespace TestTaskCadwise1.Tests.Models
{
    public class RefactorUnitTests
    {
        [Fact]
        public void Test1()
        {
            var rU = new RefactorUnit(5, true);

            var str = "Hello, how are you?";
            var result = rU.RefactorTextBlock(str.ToCharArray(), str.Length);

            Assert.True(result.ToString() == "Hello   ");
        }

        [Fact]
        public void Test2()
        {
            var rU = new RefactorUnit(0, true);

            var str = "йцукен,,,;;;,,,екуцй";
            var result = rU.RefactorTextBlock(str.ToCharArray(), str.Length);

            Assert.True(result.ToString() == "йцукенекуцй");
        }

        [Fact]
        public void Test3()
        {
            var rU = new RefactorUnit(100, false);

            var str = "recieve; old123; BIGNIK; sasha.";
            var result = rU.RefactorTextBlock(str.ToCharArray(), str.Length);

            Assert.True(result.ToString() == "; ; ; .");
        }

        [Fact]
        public void Test4()
        {
            var rU = new RefactorUnit(4, true);

            var str = "aaa";
            var result = rU.RefactorTextBlock(str.ToCharArray(), str.Length);

            Assert.True(result.ToString() == "");
        }

        [Fact]
        public void Test5()
        {
            var rU = new RefactorUnit(2, true);

            var str = "aaa";
            var result = rU.RefactorTextBlock(str.ToCharArray(), str.Length);

            Assert.True(result.ToString() == "aaa");
        }
    }
}
