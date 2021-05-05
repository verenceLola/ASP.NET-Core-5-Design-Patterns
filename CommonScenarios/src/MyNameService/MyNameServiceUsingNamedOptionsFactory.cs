using Microsoft.Extensions.Options;
using CommonScenarios.Interfaces;
using CommonScenarios.Options;

namespace CommonScenarios.MyNameService
{
    public class MyNameServiceUsingNamedOptionsFactory : IMyNameService
    {
        private readonly MyOptions _options1;
        private readonly MyOptions _option2;
        public MyNameServiceUsingNamedOptionsFactory(IOptionsFactory<MyOptions> myOptions)
        {
            _options1 = myOptions.Create("Options1");
            _option2 = myOptions.Create("Options2");
        }
        public string GetName(bool someCondition) => someCondition ? _options1.Name : _option2.Name;
    }
}
