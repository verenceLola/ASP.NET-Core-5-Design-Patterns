using Microsoft.Extensions.Options;
using CommonScenarios.Options;
using CommonScenarios.Interfaces;

namespace CommonScenarios.MyNameService
{
    public class MyNameServiceUsingDoubleNameOptions : IMyNameService
    {
        private readonly MyDoubleNameOptions _options;
        public MyNameServiceUsingDoubleNameOptions(IOptions<MyDoubleNameOptions> options)
        {
            _options = options.Value;
        }
        public string GetName(bool someCondition) => someCondition ? _options.FirstName : _options.SecondName;
    }
}
