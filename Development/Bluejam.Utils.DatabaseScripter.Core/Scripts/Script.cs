using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.IO;

using Castle.Core.Resource;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

using Bluejam.Utils.DatabaseScripter.DbAdapter;

namespace Bluejam.Utils.DatabaseScripter.Core.Scripts
{
    public class Script
    {

        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; private set; }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        /// <value>The name of the database.</value>
        public string DatabaseName { get; private set; }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>The command.</value>
        public string Command { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether [wrap in transaction].
        /// </summary>
        /// <value><c>true</c> if [wrap in transaction]; otherwise, <c>false</c>.</value>
        public bool WrapInTransaction { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Script"/> class.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="command">The command.</param>
        public Script(Config.ScriptConfig config, Config.ScriptManifest manifest)
        {
            Name = config.Name;
            Description = manifest.Description;
            DatabaseName = ScriptConfigManager.GetConfig(config, "databaseName");
            ConnectionString = ScriptConfigManager.GetConnectionString(config);
            WrapInTransaction = manifest.WrapInTransaction;
            var command = File.ReadAllText(Path.Combine(Path.GetDirectoryName(Config.DatabaseScripterConfig.Instance.Manifest.FilePath), manifest.Path));
            Command = ScriptConfigInjector.InjectConfig(command, config);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Runs the specified database adapter.
        /// </summary>
        /// <param name="databaseAdapter">The database adapter.</param>
        /// <returns></returns>
        public ErrorCode Run()
        {
            var errorCode = ErrorCode.Ok;

            var container = new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));
            using (var databaseAdapter = (IDatabaseAdapter)container["databaseAdapter"])
            {
                databaseAdapter.Initialize(ConnectionString);

                System.Console.Write(this.ToString() + "... ");
                errorCode = RunImplementation(databaseAdapter);
                System.Console.WriteLine("..." + (errorCode == ErrorCode.Ok ? "OK" : "ERROR"));
            }

            return errorCode;
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region Non-public methods

        protected virtual ErrorCode RunImplementation(IDatabaseAdapter databaseAdapter)
        {
            try
            {
                if (WrapInTransaction)
                {
                    databaseAdapter.BeginTransaction();
                }

                databaseAdapter.RunCommand(DatabaseName, Command);

                if (WrapInTransaction)
                {
                    databaseAdapter.CommitTransaction();
                }
            }
            catch (DbException ex)
            {
                System.Console.WriteLine(ex.Message);
                if (WrapInTransaction)
                {
                    databaseAdapter.RollBackTransaction();
                    System.Console.WriteLine("Transaction rolled back");
                }
                return ErrorCode.ScriptExecutionException;
            }

            return ErrorCode.Ok;
        }

        #endregion
    }
}
