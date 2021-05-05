using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;

namespace OptionsConfiguration
{
    public class ConfigureMeOptions
    {
        public string Title { get; set; }
        public IEnumerable<string> Lines { get; set; }
    }
    public class Configure1ConfigureMeOptions : IConfigureOptions<ConfigureMeOptions>
    {
        public void Configure(ConfigureMeOptions options)
        {
            options.Lines = options.Lines.Append("Added line 1!");
        }
    }
    public class Configure2ConfigureMeOptions : IConfigureOptions<ConfigureMeOptions>
    {
        public void Configure(ConfigureMeOptions options)
        {
            options.Lines = options.Lines.Append("Added line 2!");
        }
    }
}
