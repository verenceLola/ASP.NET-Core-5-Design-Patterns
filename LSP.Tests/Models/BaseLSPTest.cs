using Xunit;
using LSP.Models;

namespace LSP.Examples
{
    public abstract class BaseLSPTest
    {
        protected abstract  HallOfFame sut { get; }
        public static TheoryData<Ninja> NinjaWithAtLeas100Kills => new TheoryData<Ninja>{
            new Ninja {Kills = 100},
            new Ninja {Kills = 101},
            new Ninja {Kills = 200}
        };
        [Fact]
        public virtual void Add_should_not_add_existing_ninja()
        {
            var expectedNinja = new Ninja { Kills = 200 };

            sut.Add(expectedNinja);
            sut.Add(expectedNinja);

            Assert.Collection(sut.Members, ninja => Assert.Same(expectedNinja, ninja));
        }
        [Theory]
        [MemberData(nameof(NinjaWithAtLeas100Kills))]
        public void Add_should_add_the_specified_ninja(Ninja expectedNinja)
        {
            sut.Add(expectedNinja);
            Assert.Collection(sut.Members, ninja => Assert.Same(expectedNinja, ninja));
        }
        [Fact]
        public void Members_should_return_ninja_ordered_by_kills_desc()
        {
            sut.Add(new Ninja { Kills = 100 });
            sut.Add(new Ninja { Kills = 150 });
            sut.Add(new Ninja { Kills = 200 });

            var result = sut.Members;

            Assert.Collection(result,
                ninja => Assert.Equal(200, ninja.Kills),
                ninja => Assert.Equal(150, ninja.Kills),
                ninja => Assert.Equal(100, ninja.Kills)
            );
        }
    }
}
