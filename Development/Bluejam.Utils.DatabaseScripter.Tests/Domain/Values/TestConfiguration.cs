//DatabaseScripter  Copyright (C) 2010  Philip Wood
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
    public class TestConfiguration : DomainTestBase
    {
        [Test]
        public void Test_Nominal_IsValid()
        {
            //setup
            var configuration = Factories.Domain.Values.ConfigurationFactory.Development;
            AssertIsValid(configuration);
        }

        [Test]
        public void Test_WhenNoEnvironmentConfigurations_IsInvalid()
        {
            //setup
            var configuration = Factories.Domain.Values.ConfigurationFactory.Development;
            configuration.EnvironmentConfigurations.Clear();
            AssertIsInvalid(configuration, 1);
            AssertValidationResult(configuration, "EnvironmentConfigurations", "There are no environment configurations");
        }

        [Test]
        public void Test_WhenDuplicateNameEnvironmentConfigurations_IsInvalid()
        {
            //setup
            var configuration = Factories.Domain.Values.ConfigurationFactory.Development;
            configuration.EnvironmentConfigurations.Add(Factories.Domain.Entities.EnvironmentConfigurationFactory.Development);
            AssertIsInvalid(configuration, 1);
            AssertValidationResult(configuration, "EnvironmentConfigurations", "There is more than one environment configuration with the same name");
        }

        [Test]
        public void Test_WhenInvalidEnvironmentConfigurations_IsInvalid()
        {
            //setup
            var configuration = Factories.Domain.Values.ConfigurationFactory.Development;
            configuration.EnvironmentConfigurations.Clear();
            configuration.EnvironmentConfigurations.Add(Factories.Domain.Entities.EnvironmentConfigurationFactory.InvalidScriptConfiguration);
            AssertIsInvalid(configuration, 1);
            AssertValidationResult(configuration, "Name", "A name must be specified for a property");
        }
    }
}
