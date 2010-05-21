using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Core.Resource;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using log4net;

using Bluejam.Utils.DatabaseScripter.Core.Config;
using Bluejam.Utils.DatabaseScripter.DbAdapter;

namespace Bluejam.Utils.DatabaseScripter.Core.Scripts
{
    /// <summary>
    /// Factory class for IDatabaseAdapter objects
    /// </summary>
    public static class AdapterFactory
    {

        #region Public

        /// <summary>
        /// Creates an IDatabaseAdapter object. If running in preview mode, the preview adapter
        /// will be instantiated. Otherwise, the database adapter will be instantiated.
        /// </summary>
        /// <returns></returns>
        public static IDatabaseAdapter Create()
        {
            if (DatabaseScripterConfig.Instance.Preview)
            {
                return CreatePreviewAdapter();
            }

            return CreateDatabaseAdapter();
        }

        #endregion

        #region Non-public

        private static readonly ILog log = LogManager.GetLogger(typeof(AdapterFactory));

        private static IDatabaseAdapter CreatePreviewAdapter()
        {
            IDatabaseAdapter previewAdapter;

            try
            {
                var container = new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));
                previewAdapter = (IDatabaseAdapter)container["previewAdapter"];
            }
            catch (Exception ex)
            {
                log.Error("An error occurred when creating the preview adapter.", ex);
                throw new DatabaseScripterException(ErrorCode.FailedToCreatePreviewAdapter, "The preview adapter could not be created", ex);
            }

            log.Debug("Preview adapter created");
            return previewAdapter;
        }

        private static IDatabaseAdapter CreateDatabaseAdapter()
        {
            IDatabaseAdapter databaseAdapter;

            try
            {
                var container = new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));
                databaseAdapter = (IDatabaseAdapter)container["databaseAdapter"];
            }
            catch (Exception ex)
            {
                log.Error("An error occurred when creating the database adapter", ex);
                throw new DatabaseScripterException(ErrorCode.FailedToCreateDatabaseAdapter, "The database adapter could not be created", ex);
            }

            log.Debug("Database adapter created");
            return databaseAdapter;
        }

        #endregion

    }
}
