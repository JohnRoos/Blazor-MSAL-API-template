using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMSAL
{
    public static class SettingsManager
    {
        private static WebAssemblyHostConfiguration configuration;
        public static WebAssemblyHostConfiguration Configuration { get => configuration; private set => configuration = value; }

        public static void Initialize(WebAssemblyHostConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
