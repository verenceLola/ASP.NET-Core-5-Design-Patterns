using System;
using System.Text;
using Xunit;
using Mediator;

namespace tests
{
    public class MediatorTest
    {
        [Fact]
        public void Send_a_message_to_all_colleagues()
        {
            var (millerWriter, miller) = CreateConcreateCollegue("Miller");
            var (vinceWriter, vince) = CreateConcreateCollegue("Vince");
            var (simonWriter, simon) = CreateConcreateCollegue("Simon");

            var mediator = new ConcreteMediator(miller, vince, simon);
            var expectedOutput = $"[Miller]: Hey everyone!\n[Vince]: What's up Miller?\n[Simon]: Hey Miller!\n";

            mediator.Send(new(from: miller, content: "Hey everyone!"));
            mediator.Send(new(vince, "What's up Miller?"));
            mediator.Send(new(simon, "Hey Miller!"));

            Assert.Equal(expectedOutput, millerWriter.Output.ToString());
            Assert.Equal(expectedOutput, vinceWriter.Output.ToString());
            Assert.Equal(expectedOutput, simonWriter.Output.ToString());
        }
        private (TestMessageWriter, ConcreteColleague) CreateConcreateCollegue(string name)
        {
            var messageWriter = new TestMessageWriter();
            var concreateColleague = new ConcreteColleague(name, messageWriter);

            return (messageWriter, concreateColleague);
        }
        private class TestMessageWriter : IMessageWriter<Message>
        {
            public StringBuilder Output { get; } = new StringBuilder();
            public void Write(Message message)
            {
                Output.AppendLine($"[{message.Sender.Name}]: {message.Conent}");
            }
        }
    }

}
