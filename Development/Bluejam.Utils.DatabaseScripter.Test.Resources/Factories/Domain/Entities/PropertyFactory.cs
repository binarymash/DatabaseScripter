using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bluejam.Utils.DatabaseScripter.Domain.Entities;

namespace Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Entities
{

    public abstract class PropertyFactory
    {

        #region Nominal

        public static Property Connection
        {
            get { return new Property("connection", "medialibrary"); }
        }

        public static Property DatabaseName
        {
            get { return new Property("databaseName", "MediaLibrary_Development"); }
        }

        public static Property Title
        {
            get { return new Property("title", "Peep Show"); }
        }

        public static Property EpisodeNum
        {
            get { return new Property("episodeNum", "s01e01"); }
        }

        public static Property Date
        {
            get { return new Property("date", "0x000093F900000000"); }
        }

        public static Property Description
        {
            get { return new Property("description", "Mark and Jeremy compete for the attentions of their attractive neighbor Toni."); }
        }

        public static Property Rating
        {
            get { return new Property("rating", "5"); }
        }

        public static Property MediumType1Name
        {
            get { return new Property("mediumType1Name", "DVD+R"); }
        }

        public static Property MediumType1Description
        {
            get { return new Property("mediumType1Description", "DVD+R 4.7GB"); }
        }

        public static Property MediumType2Name
        {
            get { return new Property("mediumType2Name", "DVD+RW"); }
        }

        public static Property MediumType2Description
        {
            get { return new Property("mediumType2Description", "DVD+RW 4.7GB"); }
        }

        #endregion

        #region Other

        public static Property DuplicateName
        {
            get
            { 
                return new Property(Title.Name, "Another Nominal Value 1");
            }
        }

        public static Property DuplicateValue
        {
            get
            {
                return new Property("Another Name 1", Title.Value);
            }
        }

        public static Property Nominal2
        {
            get
            {
                return new Property("Nominal Name 2", "Nominal Value 2");
            }
        }

        public static Property NullName
        {
            get
            {
                return new Property(null, "MyValue");
            }
        }

        public static Property EmptyName
        {
            get
            {
                return new Property(string.Empty, "MyValue");
            }
        }

        public static Property WhiteSpaceName
        {
            get
            {
                return new Property(" ", "MyValue");
            }
        }

        public static Property NullValue
        {
            get
            {
                return new Property("MyName", null);
            }
        }

        public static Property EmptyValue
        {
            get
            {
                return new Property("MyName", string.Empty);
            }
        }

        public static Property WhiteSpaceValue
        {
            get
            {
                return new Property("MyName", " ");
            }
        }

        #endregion
    }

}
