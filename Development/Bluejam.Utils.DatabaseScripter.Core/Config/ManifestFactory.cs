using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using log4net;

namespace Bluejam.Utils.DatabaseScripter.Core.Config
{

    public static class ManifestFactory
    {

        #region Public methods

        /// <summary>
        /// Creates the manifest object from a file at the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static ManifestFactoryResult Create(string path)
        {
            log.DebugFormat(CultureInfo.InvariantCulture, "Reading manifest at {0}", path);

            try
            {
                var execPath = new FileInfo(Assembly.GetExecutingAssembly().Location);
                if (!Path.IsPathRooted(path))
                {
                    path = Path.Combine(execPath.Directory.FullName, path);
                    log.DebugFormat(CultureInfo.InvariantCulture, "The manifest path is relative; expanded to {0}", path);
                }

                if (!File.Exists(path))
                {
                    log.ErrorFormat(CultureInfo.InvariantCulture, "The manifest file could not be found at {0}", path);
                    return new ManifestFactoryResult(ErrorCode.CouldNotFindManifest, null);
                }

                var manifestValidator = new ManifestValidator();
                var result = manifestValidator.Validate(path);
                if (result.ErrorCode != ErrorCode.Ok)
                {
                    log.Error("The manifest file is invalid");
                    return new ManifestFactoryResult(ErrorCode.InvalidManifest, null);
                }

                //deserialize
                var xmlReader = XmlReader.Create(path);
                var xmlSerializer = new XmlSerializer(typeof(Manifest));
                var manifest = (Manifest)xmlSerializer.Deserialize(xmlReader);
                manifest.FilePath = path;

                return new ManifestFactoryResult(ErrorCode.Ok, manifest);
            }
            catch (DatabaseScripterException ex)
            {
                log.Error("An unexpected error occurred when reading the manifest file.", ex);
                return new ManifestFactoryResult(ErrorCode.UnknownError, null);
            }
        }

        #endregion

        #region Non-public

        private static readonly ILog log = LogManager.GetLogger(typeof(ManifestFactory));

        #endregion

    }
}
