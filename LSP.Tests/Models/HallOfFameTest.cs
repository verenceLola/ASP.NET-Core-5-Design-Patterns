using Xunit;
using LSP.Models;

namespace LSP.Examples 
{
    public class HallOfFameTest : BaseLSPTest {
        protected override HallOfFame sut {get;} = new HallOfFame();
        [Fact]
        public void Add_should_not_add_ninja_with_less_than_100_kills()
        {
            var ninja = new Ninja { Kills = 99 };
            sut.Add(ninja);

            Assert.Empty(sut.Members);
        }
    }
}
