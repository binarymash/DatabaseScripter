using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bluejam.Utils.DatabaseScripter.Domain.Entities;

namespace Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Entities
{
    public abstract class PropertyCollectionFactory
    {
        #region Nominal

        public static PropertyCollection NominalGlobal
        {
            get
            {
                var properties = new PropertyCollection();
                properties.Add(PropertyFactory.Connection);
                properties.Add(PropertyFactory.DatabaseName);

                return properties;
            }
        }

        public static PropertyCollection InsertSampleData
        {
            get
            {
                var properties = new PropertyCollection();
                properties.Add(PropertyFactory.Title);
                properties.Add(PropertyFactory.Description);
                properties.Add(PropertyFactory.Date);
                properties.Add(PropertyFactory.EpisodeNum);
                properties.Add(PropertyFactory.MediumType1Description);
                properties.Add(PropertyFactory.MediumType1Name);
                properties.Add(PropertyFactory.MediumType2Description);
                properties.Add(PropertyFactory.MediumType2Name);
                properties.Add(PropertyFactory.Rating);

                return properties;
            }
        }

        #endregion

        #region Other

        public static PropertyCollection Empty
        {
            get
            {
                return new PropertyCollection();
            }
        }

        public static PropertyCollection ContainsInvalidProperty
        {
            get
            {
                var properties = InsertSampleData;
                properties.Add(PropertyFactory.NullName);

                return properties;
            }
        }

        public static PropertyCollection ContainsDuplicatePropertyNames
        {
            get
            {
                var properties = InsertSampleData;
                properties.Add(PropertyFactory.DuplicateName);

                return properties;
            }
        }

        public static PropertyCollection ContainsDuplicatePropertyValues
        {
            get
            {
                var properties = InsertSampleData;
                properties.Add(PropertyFactory.DuplicateValue);

                return properties;
            }
        }

        #endregion

    }
}
