using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bluejam.Utils.DatabaseScripter.Domain.Entities;

namespace Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Entities
{

    public abstract class ScriptFactory
    {

        public static Script Nominal
        {
            get { return CreateNominal(false); }
        }

        public static Script NominalWithTransaction
        {
            get { return CreateNominal(true); }
        }

        public static Script NoCurrentVersion
        {
            get
            {
                return new Script("No current version script",
                    "This is a script",
                    "A database",
                    "A connection string",
                    false,
                    null,
                    Values.VersionFactory.v1_0_0_1,
                    CommandFactory.Nominal);
            }
        }

        public static Script NoNewVersion
        {
            get
            {
                return new Script("No new version script",
                    "This is a script",
                    "A database",
                    "A connection string",
                    false,
                    Values.VersionFactory.v1_0_0_0,
                    null,
                    CommandFactory.Nominal);
            }
        }

        public static Script NoVersion
        {
            get
            {
                return new Script("No version script",
                    "This is a script",
                    "A database",
                    "A connection string",
                    false,
                    null,
                    null,
                    CommandFactory.Nominal);
            }
        }

        public static Script DuplicateNominalName
        {
            get
            {
                return new Script(Nominal.Name,
                    "This is a duplicate script name",
                    "A database",
                    "A connection string",
                    false,
                    Values.VersionFactory.v1_0_0_0,
                    Values.VersionFactory.v1_0_0_1,
                    CommandFactory.Nominal);
            }
        }

        public static Script Nominal2
        {
            get
            {
                return new Script("Nominal script 2",
                    "This is another script",
                    "A database",
                    "A connection string",
                    false,
                    Values.VersionFactory.v1_0_0_1,
                    Values.VersionFactory.v1_0_0_2,
                    CommandFactory.Nominal2);
            }
        }

        public static Script InvalidBecauseNameIsEmpty
        {
            get
            {
                return new Script(string.Empty,
                    "This is a script",
                    "A database",
                    "A connection string",
                    false,
                    Values.VersionFactory.v1_0_0_0,
                    Values.VersionFactory.v1_0_0_1,
                    CommandFactory.Nominal);
            }
        }

        private static Script CreateNominal(bool useTransaction)
        {
                return new Script("Nominal script 1",
                    "This is a script",
                    "A database",
                    "A connection string",
                    useTransaction,
                    Values.VersionFactory.v1_0_0_0,
                    Values.VersionFactory.v1_0_0_1,
                    CommandFactory.Nominal);
        }



    }

}
