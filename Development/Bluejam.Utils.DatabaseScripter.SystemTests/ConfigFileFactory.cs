using System.IO;
using System.Reflection;

namespace Bluejam.Utils.DatabaseScripter.SystemTests
{
    /// <summary>
    /// Creates config files for test cases
    /// </summary>
    class ConfigFileFactory
    {

        /// <summary>
        /// Creates a config file at the specified path using the specified manifest resource.
        /// </summary>
        /// <param name="configFilePath">The config file path.</param>
        /// <param name="manifestResourceName">The name of the manifest resource.</param>
        public static void SetUpConfig(string configFilePath, string manifestResourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(manifestResourceName);

            using (var configFileStream = new FileStream(configFilePath, FileMode.Create))
            {
                stream.WriteTo(configFileStream);
            }
        }

    }
}
