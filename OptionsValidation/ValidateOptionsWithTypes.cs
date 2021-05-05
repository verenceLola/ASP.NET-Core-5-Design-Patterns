using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace OptionsValidation
{
    public class ValidateOptionsWithTypes
    {
        [Fact]
        public void Should_pass_validation()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IValidateOptions<Options>, OptionsValidator>();
            services.AddOptions<Options>()
                .Configure(o => o.MyImportantProperty = "Some important value");

            var serviceProvider = services.BuildServiceProvider();
            var options = serviceProvider.GetService<IOptionsMonitor<Options>>();

            Assert.Equal("Some important value", options.CurrentValue.MyImportantProperty);
        }
        [Fact]
        public void Should_fail_validation()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IValidateOptions<Options>, OptionsValidator>();
            services.AddOptions<Options>();

            var serviceProvider = services.BuildServiceProvider();
            var options = serviceProvider.GetService<IOptionsMonitor<Options>>();

            var error = Assert.Throws<OptionsValidationException>(() => options.CurrentValue);

            Assert.Collection(error.Failures, f => Assert.Equal("'MyImportantProperty' is required", f));
        }
        private class Options
        {
            public string MyImportantProperty { get; set; }
        }
        private class OptionsValidator : IValidateOptions<Options>
        {
            public ValidateOptionsResult Validate(string name, Options options)
            {
                if (string.IsNullOrWhiteSpace(options.MyImportantProperty))
                {
                    return ValidateOptionsResult.Fail("'MyImportantPeroperty' is required");
                }

                return ValidateOptionsResult.Success;
            }
        }
    }
}
