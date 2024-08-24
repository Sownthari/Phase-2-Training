using SampleTest;

namespace TestSampleApp
{
    public class Tests
    {

        public int i = 50, j = 20;
        public bool result;
        [SetUp] //performed initially
        public void CheckNonNegative()
        {
            if (i > 0 && j > 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }
        }
        [Test] //TestMethod
        public void TestAdd()
        {
            if (result)
            {
                //Arrange,- creating istance for the ttest method
                MathOpe mth = new MathOpe();
                //Act - calling the method by passing parameter
                var res = mth.add(20, 20);//actual value
                                          //Assert
                Assert.AreEqual(50, res);
            }
            else
            {
                Assert.Fail();
            }

        }
        [Test]
        [TestCase(100, 25, 4)]
        [TestCase(50, 2, 25)]
        public void TestDiv(int a, int b, int expected)
        {
            //Arrange,Act,Assert
            MathOpe mth = new MathOpe();
            //Act
            var res = mth.Div(a, b);
            //Assert
            Assert.AreEqual(expected, res);
        }
        [Test]
        [Ignore("Not yet Implemented")]
        public void TestSub()
        {

        }
        [Test]
        public void TestPro()
        {
            //Arrange,Act,Assert
            MathOpe mth = new MathOpe();
            //Act
            var res = mth.Pro(i, j);
            //Assert
            Assert.AreEqual(1000, res);
        }
        
    }
}