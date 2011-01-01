//DatabaseScripter  Copyright (C) 2011  Philip Wood
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Bluejam.Utils.DatabaseScripter.Test.Resources;
using Bluejam.Utils.DatabaseScripter.Domain.Entities;
using Bluejam.Utils.DatabaseScripter.Domain.Values;

using Factories = Bluejam.Utils.DatabaseScripter.Test.Resources.Factories;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.Tests.Domain.Values
{

    [TestFixture]
    public class TestExecutionPlan : DomainTestBase
    {

        [Test]
        public void Test_Nominal_IsInvalid()
        {
            var manifest = Factories.Domain.Values.ExecutionPlanFactory.Development;
            AssertIsValid(manifest);
        }

        [Test]
        public void Test_WhenEnvironmentIsNull_IsInvalid()
        {
            var manifest = Factories.Domain.Values.ExecutionPlanFactory.Development;
            manifest.Environment = null;

            AssertIsInvalid(manifest, 1);
            AssertValidationResult(manifest, "Environment", "The environment has not been specified");
        }

        [Test]
        public void Test_WhenEnvironmentIsEmpty_IsInvalid()
        {
            var manifest = Factories.Domain.Values.ExecutionPlanFactory.Development;
            manifest.Environment = string.Empty;

            AssertIsInvalid(manifest, 1);
            AssertValidationResult(manifest, "Environment", "The environment has not been specified");
        }

        [Test]
        public void Test_WhenEnvironmentIsWhiteSpace_IsInvalid()
        {
            var manifest = Factories.Domain.Values.ExecutionPlanFactory.Development;
            manifest.Environment = " ";

            AssertIsInvalid(manifest, 1);
            AssertValidationResult(manifest, "Environment", "The environment has not been specified");
        }

        [Test]
        public void Test_WhenDatabaseAdapterIsNull_IsInvalid()
        {
            var manifest = Factories.Domain.Values.ExecutionPlanFactory.Development;
            manifest.DatabaseAdapter = null;

            AssertIsInvalid(manifest, 1);
            AssertValidationResult(manifest, "DatabaseAdapter", "The database adapter has not been specified");
        }

        [Test]
        public void Test_WhenNoScriptsSpecified_IsInvalid()
        {
            var manifest = Factories.Domain.Values.ExecutionPlanFactory.EmptyDevelopment;

            AssertIsInvalid(manifest, 1);
            AssertValidationResult(manifest, "NameOfScriptsToRun", "No scripts have been specified");
        }
    }
}
