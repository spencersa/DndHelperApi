using AutoFixture;

namespace DndHelperApiCore.Tests
{
    public class TestBase
    {
        public Fixture Fixture { get; }

        public TestBase()
        {
            Fixture = new Fixture();
        }
    }
}
