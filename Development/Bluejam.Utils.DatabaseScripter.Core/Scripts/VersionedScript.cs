using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bluejam.Utils.DatabaseScripter.DbAdapter;

namespace Bluejam.Utils.DatabaseScripter.Core.Scripts
{
    /// <summary>
    /// 
    /// </summary>
    public class VersionedScript : Script
    {

        #region Properties

        /// <summary>
        /// Gets or sets the current version.
        /// </summary>
        /// <value>The current version.</value>
        public DbAdapter.Version CurrentVersion { get; protected set; }

        /// <summary>
        /// Gets or sets the new version.
        /// </summary>
        /// <value>The new version.</value>
        public DbAdapter.Version NewVersion { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VersionedScript"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public VersionedScript(Config.ScriptConfig config, Manifest.VersionedScriptManifest manifest) : base(config, manifest)
        {            
            CurrentVersion = (manifest.CurrentVersion == null) ? null : new DbAdapter.Version(manifest.CurrentVersion);
            NewVersion = (manifest.NewVersion == null) ? null : new DbAdapter.Version(manifest.NewVersion);
        }

        #endregion

        #region Public methods

        public override string ToString()
        {
            return String.Format("{0}: Increment database {1} from {2} to {3}",
                Name,
                DatabaseName,
                (CurrentVersion == null) ? "current" : CurrentVersion.ToString(),
                (NewVersion == null) ? "same" : NewVersion.ToString());
        }

        #endregion

        #region Non-public methods

        /// <summary>
        /// Runs the implementation.
        /// </summary>
        /// <param name="databaseAdapter">The database adapter.</param>
        /// <returns></returns>
        protected override bool RunImplementation(IDatabaseAdapter databaseAdapter)
        {
            try
            {
                if (WrapInTransaction)
                {
                    databaseAdapter.BeginTransaction();
                }

                if (null != CurrentVersion)
                {
                    if (databaseAdapter.GetVersion(DatabaseName) != CurrentVersion)
                    {
                        return false;
                    }
                };

                databaseAdapter.RunCommand(DatabaseName, Command);

                if (null != NewVersion)
                {
                    databaseAdapter.SetVersion(DatabaseName, NewVersion);
                }

                if (WrapInTransaction)
                {
                    databaseAdapter.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                if (WrapInTransaction)
                {
                    databaseAdapter.RollBackTransaction();
                    System.Console.WriteLine("Transaction rolled back");
                }
                return false;
            }

            return true;
        }

        #endregion

    }
}
