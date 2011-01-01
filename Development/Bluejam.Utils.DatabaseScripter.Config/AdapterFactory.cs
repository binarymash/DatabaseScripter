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
using System.Linq;
using System.Text;
using Castle.Core.Resource;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using log4net;

using Domain = Bluejam.Utils.DatabaseScripter.Domain;

namespace Bluejam.Utils.DatabaseScripter.Config
{
    /// <summary>
    /// Factory class for DatabaseAdapter objects
    /// </summary>
    public abstract class AdapterFactory
    {

        #region Public

        public static Domain.Strategies.DatabaseAdapter Create(bool previewOnly)
        {
            return previewOnly ? CreatePreviewAdapter() : CreateDatabaseAdapter();
        }

        #endregion

        #region Non-public

        private static readonly ILog log = LogManager.GetLogger(typeof(AdapterFactory));

        private static Domain.Strategies.DatabaseAdapter CreatePreviewAdapter()
        {
            Domain.Strategies.DatabaseAdapter previewAdapter;

            try
            {
                var container = new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));
                previewAdapter = (Domain.Strategies.DatabaseAdapter)container["previewAdapter"];
            }
            catch (Exception ex)
            {
                log.Error("An error occurred when creating the preview adapter.", ex);
                throw new Domain.DatabaseScripterException(Domain.ErrorCode.FailedToCreatePreviewAdapter, "The preview adapter could not be created", ex);
            }

            log.Debug("Preview adapter created");
            return previewAdapter;
        }

        private static Domain.Strategies.DatabaseAdapter CreateDatabaseAdapter()
        {
            Domain.Strategies.DatabaseAdapter databaseAdapter;

            try
            {
                var container = new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));
                databaseAdapter = (Domain.Strategies.DatabaseAdapter)container["databaseAdapter"];
            }
            catch (Exception ex)
            {
                log.Error("An error occurred when creating the database adapter", ex);
                throw new Domain.DatabaseScripterException(Domain.ErrorCode.FailedToCreateDatabaseAdapter, "The database adapter could not be created", ex);
            }

            log.Debug("Database adapter created");
            return databaseAdapter;
        }

        #endregion

    }
}
